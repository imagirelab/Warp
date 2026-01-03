using UnityEngine;

public class MoveCutter : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;

    private Rigidbody2D rb;
    private Camera mainCamera;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
    }

    private void FixedUpdate()
    {
        // 左へ移動
        rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);

        // カメラ外なら消す
        if (IsOutOfCamera())
        {
            Destroy(gameObject);
        }
    }

    private bool IsOutOfCamera()
    {
        Vector3 viewportPos = mainCamera.WorldToViewportPoint(transform.position);

        // 左画面外に完全に出たら
        return viewportPos.x < 0;
    }
}
