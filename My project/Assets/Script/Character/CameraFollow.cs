using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform target;                  // プレイヤー
    [SerializeField] private float smoothSpeed = 0.1f; // 追従の滑らかさ
    private Vector3 offset;                    // プレイヤーとの距離を保持

    void Start()
    {
        // シーンにある "Player" タグのオブジェクトを探す
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            target = playerObj.transform;
            offset = transform.position - target.position; // 生成時の距離を保持
        }
        else
        {
            Debug.LogWarning("CameraFollow: 'Player' タグのオブジェクトが見つかりません");
        }
    }

    // プレハブ生成後に呼び出してターゲットを更新
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        offset = transform.position - target.position; // 生成時の距離を保持
    }

    void LateUpdate()
    {
        if (target == null) return;

        // 横方向だけ追従、縦方向は固定
        Vector3 desiredPosition = new Vector3(target.position.x + offset.x, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
    }
}
