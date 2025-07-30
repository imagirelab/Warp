using UnityEngine;

public class HomingMissile : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float speed = 5f;
    private float rotationSpeed = 300f;

    void OnEnable()
    {
        // �L�������Ƀv���C���[�^�O����Ď擾���Đݒ�
        var player = GameObject.FindGameObjectWithTag("Player");

        if (player)
        {
            target = player.transform;
        }
        else
        {
            target = null;
            Debug.LogWarning("Player �^�O�̃I�u�W�F�N�g��������܂���B");

        }
    }

    void Update()
    {
        if (target == null) return;

        // �^�[�Q�b�g�Ƃ̌������Z�o
        Vector2 direction = (Vector2)target.position - (Vector2)transform.position;
        direction.Normalize();

        // ���݂̌������擾
        float currentRotation = transform.rotation.eulerAngles.z;

        // �ړI�̌������Z�o
        float targetRotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        // ��Ԃ��ĉ�]
        float newRotation = Mathf.MoveTowardsAngle(currentRotation, targetRotation, rotationSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0f, 0f, newRotation);

        // �O�Ɉړ�
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // obstacle���C���[�ɓ������������
        if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            Debug.Log("������");
            Destroy(gameObject);
        }
    }
}
