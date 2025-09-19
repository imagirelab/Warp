using UnityEngine;

public class PlaceObjectWithRotation : MonoBehaviour
{
    [Header("プレハブ設定")]
    [SerializeField] private GameObject objectPrefab; // 配置する本体
    [SerializeField] private Camera mainCamera;       // メインカメラ
    [SerializeField] private Vector3 offset;          // プレビュー表示用オフセット

    [Header("回転設定")]
    [SerializeField] private float rotationAngle = 45f; // 回転角度

    private float currentRotation = 0f; // 現在のプレビュー回転

    void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;
    }

    void Update()
    {
        // マウス位置に追従
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = transform.position.z;
        transform.position = mouseWorldPos + offset;

        // --- マウスホイールで回転 ---
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f) // 上スクロール
        {
            currentRotation += rotationAngle;
        }
        else if (scroll < 0f) // 下スクロール
        {
            currentRotation -= rotationAngle;
        }

        // 上限を -90°～90° に制限
        currentRotation = Mathf.Clamp(currentRotation, -90f, 90f);

        // プレビューオブジェクトの回転を更新
        transform.rotation = Quaternion.Euler(0f, 0f, currentRotation);

        // 左クリックでオブジェクトを配置
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(objectPrefab, transform.position, Quaternion.Euler(0f, 0f, currentRotation));
        }
    }
}
