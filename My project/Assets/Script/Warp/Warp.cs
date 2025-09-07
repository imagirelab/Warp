using UnityEngine;

public class Warp : MonoBehaviour
{
    [SerializeField] public AreaWidth areaWidth;
    public Transform playerTransform;
    [SerializeField] private WarpDistance warpDistanceChecker;

    [Header("ワープ予定地点表示用")]
    [SerializeField] private GameObject warpPreviewPrefab; // 半透明マーカーのPrefab
    private GameObject warpPreviewInstance;                // 実際に出現するマーカー

    private bool wasRightClickHeld = false;

    void Update()
    {
        if (areaWidth == null || playerTransform == null) return;

        // --- 右クリックでマーカーを生成・削除 ---
        if (Input.GetMouseButtonDown(1))
        {
            if (warpPreviewPrefab != null && warpPreviewInstance == null)
            {
                Vector3 previewPos = GetWarpPosition();
                warpPreviewInstance = Instantiate(warpPreviewPrefab, previewPos, Quaternion.identity);
                Debug.Log("Warp preview created at: " + previewPos);
            }
        }
        if (Input.GetMouseButtonUp(1) && warpPreviewInstance != null)
        {
            Destroy(warpPreviewInstance);
            warpPreviewInstance = null;
        }

        // --- マーカーが存在する限り常に更新 ---
        if (warpPreviewInstance != null)
        {
            Vector3 previewPos = GetWarpPosition();
            warpPreviewInstance.transform.position = previewPos;
        }

        // --- 右クリック離した瞬間にワープ ---
        if (wasRightClickHeld && !Input.GetMouseButton(1))
        {
            WarpToMousePosition();
        }

        wasRightClickHeld = Input.GetMouseButton(1);
    }

    // ワープ先を計算する共通処理
    Vector3 GetWarpPosition()
    {
        Vector3 playerPos = playerTransform.position;
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = playerPos.z;

        Vector3 direction = mouseWorldPos - playerPos;
        float radius = areaWidth != null ? areaWidth.CurrentScale() : direction.magnitude;

        if (radius <= 0.01f)
        {
            return mouseWorldPos;
        }
        else
        {
            Vector3 normalizedDir = direction.normalized;
            return playerPos + normalizedDir * radius;
        }
    }

    void WarpToMousePosition()
    {
        Vector3 playerPos = playerTransform.position;
        Vector3 warpPos = GetWarpPosition();

        float distanceWarped = Vector3.Distance(playerPos, warpPos);
        Debug.Log($"Warped distance: {distanceWarped} (半径: {areaWidth.CurrentScale()})");

        playerTransform.position = warpPos;

        if (warpDistanceChecker != null)
        {
            warpDistanceChecker.CheckNearTrap(warpPos);
        }

        // リセット
        areaWidth.warp = 0f;
        areaWidth.JustReleased = false;
    }
}
