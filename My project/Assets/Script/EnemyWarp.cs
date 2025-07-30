using UnityEngine;

public class EnemyWarp : MonoBehaviour
{
    [SerializeField] private float predictionTime = 0.5f;     // ���b���\�����邩
    [SerializeField] private float warpCooldown = 3.0f;       // ���[�v�̃N�[���^�C���i�b�j
    [SerializeField] private float offsetBack = 0.5f;         // �v���C���[�\���ʒu�̏������Ƀ��[�v����

    [Header("���[�v��̃G�t�F�N�g")]
    [SerializeField] private GameObject warpEffectAfter;      // ���[�v��G�t�F�N�g

    private float lastWarpTime = -Mathf.Infinity;
    private Transform player;
    private Rigidbody2D playerRb;

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

        if (Time.time - lastWarpTime >= warpCooldown)
        {
            WarpToPredictedPosition();
            lastWarpTime = Time.time;
        }
    }

    void WarpToPredictedPosition()
    {
        // �\���ʒu���v�Z�i�ʒu + ���x * ���ԁj
        Vector2 predictedPosition = (Vector2)player.position + playerRb.velocity * predictionTime;

        // �v���C���[�̐i�s�����ɏ�����O�Ƀ��[�v
        Vector2 backOffset = playerRb.velocity.normalized * offsetBack;
        Vector2 warpTarget = predictedPosition - backOffset;

        // ���[�v���s
        transform.position = warpTarget;

        // --- ���[�v��̃G�t�F�N�g�̂� ---
        if (warpEffectAfter != null)
        {
            GameObject effect = Instantiate(warpEffectAfter, transform.position, Quaternion.identity);
            Destroy(effect, 0.5f);
        }

        Debug.Log("�G���v���C���[�̗\���ʒu�Ƀ��[�v���܂���: " + warpTarget);
    }
}
