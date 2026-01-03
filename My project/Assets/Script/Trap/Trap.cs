using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Trap : MonoBehaviour
{
    [Header("効果音")]
    [SerializeField] private AudioSource seSource;
    [SerializeField] private AudioClip trapSE;

    [Header("シーンリセット待機時間")]
    [SerializeField] private float reloadDelay = 0.5f;

    private bool triggered = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // プレイヤーに当たったら
        if (triggered) return;
        if (!collision.gameObject.CompareTag("Trap")) return;

        triggered = true;

        Debug.Log("Trapに衝突！効果音再生 → シーンリセット");

        // 効果音再生
        if (seSource != null && trapSE != null)
        {
            seSource.PlayOneShot(trapSE);
        }

        // 少し待ってからシーンリセット
        StartCoroutine(ReloadScene());
    }

    private IEnumerator ReloadScene()
    {
        yield return new WaitForSeconds(reloadDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
