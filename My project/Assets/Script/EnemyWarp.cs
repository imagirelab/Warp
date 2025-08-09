using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
public class EnemyWarp : MonoBehaviour
{
    [Header("�\���ƃ��[�v�ݒ�")]
    [SerializeField] private float predictionTime = 0.5f;     // �\������
    [SerializeField] private float warpCooldown = 3.0f;       // �N�[���^�C���i���[�v�Ԋu�j
    [SerializeField] private float offsetBack = 0.5f;         // �\���ʒu�̎�O�ɂ��炷����
    [SerializeField] private float warpDelay = 0.5f;          // �\�����烏�[�v�܂ł̒x��

    [Header("�G�t�F�N�g")]
    [SerializeField] private GameObject warpEffectAfter;
    [SerializeField] private GameObject warpWarningEffect;

    [Header("�n�`����iInspector�� Ground ���C���[���Z�b�g���Ă��������j")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCastStartHeight = 10f;   // ���C�̊J�n�����i����j
    [SerializeField] private float groundCastDistance = 20f;     // ���C�̒����i�������j
    [SerializeField] private float groundSnapOffset = 0.05f;     // �n�ʂɂ߂荞�܂Ȃ����߂̔����I�t�Z�b�g
    [SerializeField] private float overlapCheckRadius = 0.12f;   // ���[�v�悪�Փ˂��ĂȂ����̃`�F�b�N���a
    [SerializeField] private int overlapFixMaxAttempts = 8;      // �Փ˂��Ă��������Ɉړ����ă��g���C�����

    [Header("�t�F�[�h")]
    [SerializeField] private float fadeDuration = 0.25f;

    private Transform player;
    private Rigidbody2D playerRb;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    // �����x�T���v�����O�p�iFixedUpdate�ōX�V�j
    private Vector2 prevSampledVelocity;
    private Vector2 lastSampledVelocity;
    private float prevSampleTime;
    private float lastSampleTime;

    private Vector2 nextWarpTarget;
    private GameObject warningInstance;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj == null)
        {
            Debug.LogError("[EnemyWarp] Player ��������܂��� (Tag=Player ���m�F)�B");
            enabled = false;
            return;
        }

        player = playerObj.transform;
        playerRb = playerObj.GetComponent<Rigidbody2D>();
        if (playerRb == null)
        {
            Debug.LogError("[EnemyWarp] Player �� Rigidbody2D ������܂���B");
            enabled = false;
            return;
        }

        // �����T���v�����Ԑݒ�
        lastSampledVelocity = playerRb.velocity;
        prevSampledVelocity = playerRb.velocity;
        lastSampleTime = Time.time;
        prevSampleTime = Time.time - Time.fixedDeltaTime;

        StartCoroutine(WarpRoutine());
    }

    void FixedUpdate()
    {
        // FixedUpdate �ő��x���T���v�����O�i�����X�e�b�v�Ɠ����j
        prevSampledVelocity = lastSampledVelocity;
        prevSampleTime = lastSampleTime;

        lastSampledVelocity = playerRb.velocity;
        lastSampleTime = Time.time;
    }

    IEnumerator WarpRoutine()
    {
        while (true)
        {
            // �\���ʒu�v�Z & �\���o��
            PredictWarpPosition();

            // �t�F�[�h�A�E�g�i���o���o�j
            yield return StartCoroutine(FadeSprite(1f, 0f, fadeDuration));

            // �\���\���҂�
            yield return new WaitForSeconds(warpDelay);

            // ���[�v���s
            ExecuteWarp();

            // �t�F�[�h�C��
            yield return StartCoroutine(FadeSprite(0f, 1f, fadeDuration));

            // �N�[���^�C��
            yield return new WaitForSeconds(warpCooldown);
        }
    }

    void PredictWarpPosition()
    {
        // ���S�ɉ����x�����߂�iFixedUpdate�ŃT���v�����O�����l���g�p�j
        float dt = lastSampleTime - prevSampleTime;
        if (dt <= Mathf.Epsilon) dt = Time.fixedDeltaTime; // ���S�[�u

        Vector2 acceleration = (lastSampledVelocity - prevSampledVelocity) / dt;

        Vector2 predicted = (Vector2)player.position
                            + playerRb.velocity * predictionTime
                            + 0.5f * acceleration * predictionTime * predictionTime;

        // ��O�ɃI�t�Z�b�g�i���x���������ꍇ�̓I�t�Z�b�g�����j
        Vector2 backOffset = playerRb.velocity.sqrMagnitude > 0.0001f ? playerRb.velocity.normalized * offsetBack : Vector2.zero;
        Vector2 candidate = predicted - backOffset;

        // �n�`�ɃX�i�b�v���邽�߂ɏ�����牺�� Raycast
        Vector2 rayStart = candidate + Vector2.up * groundCastStartHeight;
        RaycastHit2D hit = Physics2D.Raycast(rayStart, Vector2.down, groundCastDistance, groundLayer);
        if (hit.collider != null)
        {
            candidate.y = hit.point.y + groundSnapOffset; // ������ɃI�t�Z�b�g���Ė��܂��h��
        }
        else
        {
            // Raycast ��������Ȃ��ꍇ�̓��O�i�f�o�b�O�j���c���iInspector�̃p�����[�^��v�m�F�j
            Debug.LogWarning("[EnemyWarp] Ground �� Raycast ��������܂���ł����BgroundLayer / cast ���� ���m�F���Ă��������B");
        }

        // ���[�v�悪���̃R���C�_�Əd�Ȃ��Ă������֏������ړ����ē������i�ő� attempt ��j
        int attempts = 0;
        while (attempts < overlapFixMaxAttempts)
        {
            Collider2D overl = Physics2D.OverlapCircle(candidate, overlapCheckRadius, groundLayer);
            if (overl == null) break;
            candidate.y += overlapCheckRadius * 1.5f; // �������グ��
            attempts++;
        }
        if (attempts >= overlapFixMaxAttempts)
        {
            Debug.LogWarning("[EnemyWarp] ���[�v�悪�Փ˂��Ă���\��������܂��B�C�����K�v�ł��B");
        }

        nextWarpTarget = candidate;

        // �\���G�t�F�N�g��z�u�i�\���ʒu�͏�����Ɂj
        if (warpWarningEffect != null)
        {
            if (warningInstance != null) Destroy(warningInstance);
            Vector3 warnPos = (Vector3)nextWarpTarget + Vector3.up * 0.05f;
            warningInstance = Instantiate(warpWarningEffect, warnPos, Quaternion.identity);
        }

        // �f�o�b�O���O
        // Debug.Log("[EnemyWarp] Next target: " + nextWarpTarget);
    }

    void ExecuteWarp()
    {
        // Rigidbody2D ���g���Ĉʒu���ړ��itransform �ł͂Ȃ� rb.position ���g���j
        rb.position = nextWarpTarget;

        // �d�v�F���[�v��̎c�����x�����ꂢ�ɏ����i���� y �����j
        rb.velocity = Vector2.zero;
        rb.Sleep(); // ������������������x�~������i�K�v�Ȃ� WakeUp �ŕ��A�j
        rb.WakeUp();

        // ���[�v��G�t�F�N�g
        if (warpEffectAfter != null)
        {
            GameObject ef = Instantiate(warpEffectAfter, (Vector3)nextWarpTarget, Quaternion.identity);
            Destroy(ef, 1.0f);
        }

        // �\���G�t�F�N�g������
        if (warningInstance != null)
        {
            Destroy(warningInstance);
            warningInstance = null;
        }
    }

    IEnumerator FadeSprite(float from, float to, float duration)
    {
        if (spriteRenderer == null) yield break;
        float elapsed = 0f;
        Color c = spriteRenderer.color;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float a = Mathf.Lerp(from, to, elapsed / duration);
            c.a = a;
            spriteRenderer.color = c;
            yield return null;
        }
        c.a = to;
        spriteRenderer.color = c;
    }

    // �f�o�b�O�p�FGizmos�ŗ\���ʒu��Ray������
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(nextWarpTarget, 0.12f);

        Vector2 rayStart = nextWarpTarget + Vector2.up * groundCastStartHeight;
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(rayStart, rayStart + Vector2.down * groundCastDistance);
    }
}
