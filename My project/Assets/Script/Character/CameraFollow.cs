using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;   // �Ǐ]�Ώہi�v���C���[�j
    [SerializeField] private Vector3 offset = new Vector3(0, 0, -10); // �J�����̈ʒu�␳
    [SerializeField] private float followSpeed = 10f; // �Ǐ]���x

    void LateUpdate()
    {
        if (target == null) return;

        // �X���[�Y�ɒǏ]�i���ڒǏ]�������Ȃ�Lerp���ȗ��j
        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);
    }
}
