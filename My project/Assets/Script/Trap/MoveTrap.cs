using UnityEngine;

public class MoveTrap : MonoBehaviour
{
    [SerializeField] private string targetName = "TargetPoint";
    [SerializeField] private float baseMoveSpeed = 5f; // ��{�X�s�[�h

    private Transform targetPoint;

    void OnEnable()
    {
        var target = GameObject.Find(targetName);
        if (target)
        {
            targetPoint = target.transform;
        }
        else
        {
            targetPoint = null;
            Debug.LogWarning("�w�肳�ꂽ�I�u�W�F�N�g��������܂���B: " + targetName);
        }
    }

    void Update()
    {
        if (targetPoint == null) return;

        // warpDistance��0.5�Ȃ�X�s�[�h�����ɂ���i1.0�Ȃ猳�̂܂܁j
        float speedModifier = Manager.warpDistance;
        float currentSpeed = baseMoveSpeed * speedModifier;

        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPoint.position,
            currentSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPoint.position) < 0.05f)
        {
            Destroy(gameObject);
        }
    }
}
