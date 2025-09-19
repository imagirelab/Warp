using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class FadeController : MonoBehaviour
{
    [SerializeField] private Image fadeImage;       // 黒いImageをアタッチ
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private float waitTime = 0.5f;

    // フェード中に非表示にしたいボタン（複数対応）
    [SerializeField] private Button[] buttonsToHide;

    private string nextSceneName;

    // シーン切り替え開始（外部からシーン名を渡す）
    public void StartFade(string sceneName)
    {
        nextSceneName = sceneName;
        StartCoroutine(FadeOutIn());
    }

    private IEnumerator FadeOutIn()
    {
        // フェードアウト
        yield return StartCoroutine(Fade(0f, 1f));

        HideButtons();

        // 少し待機（真っ暗な状態）
        yield return new WaitForSeconds(waitTime);

        // シーンロード
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }

        // フェードイン
        yield return StartCoroutine(Fade(1f, 0f));
    }

    private IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float elapsed = 0f;
        Color c = fadeImage.color;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / fadeDuration);
            fadeImage.color = new Color(c.r, c.g, c.b, alpha);
            yield return null;
        }

        fadeImage.color = new Color(c.r, c.g, c.b, endAlpha);
    }

    private void HideButtons()
    {
        foreach (Button btn in buttonsToHide)
        {
            if (btn != null)
            {
                btn.gameObject.SetActive(false);
            }
        }
    }
}
