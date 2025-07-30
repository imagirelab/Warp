using UnityEngine;

public class Boss : MonoBehaviour
{
    public Transform player;
    public Rigidbody2D playerRb;
    public float predictionTime = 0.5f; // 0.5�b���\��
    public float moveSpeed = 3f;
    public float attackRange = 1.5f;
    private Rigidbody2D rbody;

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector2 predictedPos = PredictPlayerPosition();
        float distance = Vector2.Distance(transform.position, predictedPos);

        if (distance > attackRange)
        {
            // �\���ʒu�Ɍ������Ĉړ�
            Vector2 direction = (predictedPos - (Vector2)transform.position).normalized;
            rbody.velocity = new Vector2(direction.x * moveSpeed, rbody.velocity.y);
        }
        else
        {
            // �߂Â�����U���i�����ɍU������������j
            Debug.Log("�\���n�_�ɍU���I");
        }
    }

    Vector2 PredictPlayerPosition()
    {
        return (Vector2)player.position + playerRb.velocity * predictionTime;
    }
}
