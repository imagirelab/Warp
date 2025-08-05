using UnityEngine;

public class EnemyWarp : MonoBehaviour
{
    [SerializeField] private float predictionTime = 0.5f;     // �\������
    [SerializeField] private float warpCooldown = 3.0f;       // �N�[���^�C��
    [SerializeField] private float offsetBack = 0.5f;         // ������O�Ƀ��[�v

    [Header("���[�v�p�G�t�F�N�g")]
    [SerializeField] private GameObject warpEffectAfter;      // ���[�v��̔����G�t�F�N�g
    [SerializeField] private GameObject warpWarningEffect;    // ���[�v�O�̗\���G�t�F�N�g

    private float lastWarpTime = -Mathf.Infinity;
    private Transform player;
    private Rigidbody2D playerRb;

    private GameObject warningInstance;   // ���ݕ\�����̗\���G�t�F�N�g
    private Vector2 nextWarpTarget;       // ���̃��[�v����L�^

    void Start()
    {
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
            playerRb = playerObj.GetComponent<Rigidbody2D>();
        }
        else
        {
            Debug.LogError("Player��������܂���BTag�����������m�F���Ă��������B");
        }
    }

    void Update()
    {
        if (player == null || playerRb == null) return;

        // �N�[���^�C�����߂��Ă���Η\�����Ėڈ���o��
        if (Time.time - lastWarpTime >= warpCooldown)
        {
            PredictWarpPosition();  // �\�����o��
            Invoke(nameof(ExecuteWarp), 0.5f);  // �����x��ă��[�v�i0.5�b��j
            lastWarpTime = Time.time;
        }
    }

    // ���[�v�ʒu��\�����A�����ɖڈ���o��
    void PredictWarpPosition()
    {
        Vector2 predictedPosition = (Vector2)player.position + playerRb.velocity * predictionTime;
        Vector2 backOffset = playerRb.velocity.normalized * offsetBack;
        nextWarpTarget = predictedPosition - backOffset;

        // �\���G�t�F�N�g��z�u
        if (warpWarningEffect != null)
        {
            if (warningInstance != null) Destroy(warningInstance); // �O��̂�����
            warningInstance = Instantiate(warpWarningEffect, nextWarpTarget, Quaternion.identity);
        }
    }

    // ���ۂɃ��[�v����
    void ExecuteWarp()
    {
        transform.position = nextWarpTarget;

        // ���[�v��G�t�F�N�g
        if (warpEffectAfter != null)
        {
            GameObject effect = Instantiate(warpEffectAfter, transform.position, Quaternion.identity);
            Destroy(effect, 0.5f);
        }

        // �\���G�t�F�N�g������
        if (warningInstance != null)
        {
            Destroy(warningInstance);
            warningInstance = null;
        }

        Debug.Log("�G���\���n�_�Ƀ��[�v���܂���: " + nextWarpTarget);
    }
}
