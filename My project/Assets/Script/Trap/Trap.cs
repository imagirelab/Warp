using UnityEngine;
using UnityEngine.SceneManagement; // ← 追加

public class Trap : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // プレイヤーにのみ反応する場合はタグを変更
        if (collision.gameObject.CompareTag("Trap"))
        {
            Debug.Log("Trapに衝突！シーンをリセットします。");

            // 現在のシーンを再読み込み（最初の状態に戻す）
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
