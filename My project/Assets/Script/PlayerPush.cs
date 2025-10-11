//using UnityEngine;

//public class PlayerPush: MonoBehaviour
//{
//    [SerializeField] private float pushForce = 500f; // ノックバック力

//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//        if (!collision.CompareTag("Player")) return;

//        Rigidbody2D playerRb = collision.GetComponent<Rigidbody2D>();
//        if (playerRb == null) return;

//        // オブジェクトの向き
//        Vector2 dir = transform.localScale.x >= 0 ? Vector2.right : Vector2.left;

//        // ノックバック
//        playerRb.AddForce(dir * pushForce, ForceMode2D.Impulse);
//    }
//}
