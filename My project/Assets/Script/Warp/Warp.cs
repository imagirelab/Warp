using UnityEngine;

public class Warp : MonoBehaviour
{
    private Transform playerTransform;

    [Header("トラップ判定用")]
    [SerializeField] private WarpDistance warpDistanceChecker;

    [Header("ワープ予定地点表示用")]
    [SerializeField] private GameObject warpPreviewPrefab; // 半透明マーカーのPrefab
    private GameObject warpPreviewInstance;                // 実際に出現するマーカー

    private bool wasRightClickHeld = false;

    // --- 外部からプレイヤーをセット ---
    public void SetPlayerTransform(Transform player)
    {
        playerTransform = player;
    }

    void Update()
    {
        if (playerTransform == null) return;

        // --- 右クリックでマーカー生成 ---
        if (Input.GetMouseButtonDown(1))
        {
            if (warpPreviewPrefab != null && warpPreviewInstance == null)
            {
                Vector3 previewPos = GetWarpPosition();
                warpPreviewInstance = Instantiate(warpPreviewPrefab, previewPos, Quaternion.identity);
            }
        }

        // --- 右クリック離したらマーカー削除 ---
        if (Input.GetMouseButtonUp(1) && warpPreviewInstance != null)
        {
            Destroy(warpPreviewInstance);
            warpPreviewInstance = null;
        }

        // --- マーカーを常に更新 ---
        if (warpPreviewInstance != null)
        {
            warpPreviewInstance.transform.position = GetWarpPosition();
        }

        // --- 右クリック離した瞬間にワープ ---
        if (wasRightClickHeld && !Input.GetMouseButton(1))
        {
            WarpToMousePosition();
        }

        wasRightClickHeld = Input.GetMouseButton(1);
    }

    // マウスのワールド座標を返す
    Vector3 GetWarpPosition()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = playerTransform.position.z; // プレイヤーと同じZ
        return mouseWorldPos;
    }

    // ワープ実行
    public void WarpToMousePosition()
    {
        if (playerTransform == null) return;

        Vector3 warpPos = GetWarpPosition();
        playerTransform.position = warpPos;

        if (warpDistanceChecker != null)
        {
            warpDistanceChecker.CheckNearTrap(warpPos);
        }
    }

    // 任意の位置にワープするメソッド
    public void WarpToPosition(Vector3 position)
    {
        if (playerTransform == null) return;

        playerTransform.position = new Vector3(position.x, position.y, playerTransform.position.z);

        if (warpDistanceChecker != null)
        {
            warpDistanceChecker.CheckNearTrap(position);
        }
    }
}
