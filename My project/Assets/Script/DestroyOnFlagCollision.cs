using UnityEngine;

public class DestroyOnFlagCollision : MonoBehaviour
{
    [SerializeField] private float delay = 1f; // 削除までの遅延時間（秒）

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Flag"))
        {
            // 指定時間後に自分自身を削除
            Destroy(gameObject, delay);
        }
    }
}
