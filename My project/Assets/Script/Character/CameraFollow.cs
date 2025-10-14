using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform target;
    [SerializeField] private float smoothSpeed = 0.1f;
    private Vector3 offset;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            target = playerObj.transform;
            offset = transform.position - target.position;
        }
        else
        {
            Debug.LogWarning("CameraFollow: 'Player' タグのオブジェクトが見つかりません");
        }
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        offset = transform.position - target.position;
    }

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = new Vector3(target.position.x + offset.x, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
    }

    // 外部から追従解除を呼べるように関数を追加
    public void StopFollowing()
    {
        target = null;
    }
}
