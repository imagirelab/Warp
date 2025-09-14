using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeController : MonoBehaviour
{
    [SerializeField] private Image fadeImage;       // 黒いImageをアタッチ
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private float waitTime = 0.5f;

    private GameObject currentStage;
    private GameObject nextStage;

    // 常に無効化したいステージ
    [SerializeField] private GameObject extraStageToDisable;

    // フェード中に非表示にしたいボタン（複数対応）
    [SerializeField] private Button[] buttonsToHide;

    // 最初のステージを登録
    public void SetCurrentStage(GameObject stage)
    {
        currentStage = stage;
        currentStage.SetActive(true);
    }

    // ステージ切り替え（通常）
    public void StartFade(GameObject targetStage)
    {
        nextStage = targetStage;
        StartCoroutine(FadeOutIn());
    }

    private IEnumerator FadeOutIn()
    {
        yield return StartCoroutine(Fade(0f, 1f));

        HideButtons();

        if (currentStage != null) currentStage.SetActive(false);
        if (extraStageToDisable != null) extraStageToDisable.SetActive(false);
        if (nextStage != null) nextStage.SetActive(true);

        yield return new WaitForSeconds(waitTime);

        yield return StartCoroutine(Fade(1f, 0f));

        currentStage = nextStage;
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
