using UnityEngine;
using TMPro;

public class PlayerDeathHandler : MonoBehaviour
{
    private CharacterSpawner spawner;

    private static int lifeCount = -1; // 初期値を -1 にしてAwakeで設定
    private const int MaxLives = 2;

    private TextMeshProUGUI lifeText;
    private GameObject gameOverUI;

    private void Awake()
    {
        // lifeCount が未初期化なら初期値にする
        if (lifeCount < 0)
            lifeCount = MaxLives;
    }

    private void Start()
    {
        // GameOverUI を探す
        gameOverUI = GameObject.Find("GameOver");
        if (gameOverUI != null)
            gameOverUI.SetActive(false);

        // LifeText を探す
        var obj = GameObject.Find("LifeText");
        if (obj != null)
            lifeText = obj.GetComponent<TextMeshProUGUI>();

        UpdateLifeDisplay();
    }

    public void SetSpawner(CharacterSpawner spawner)
    {
        this.spawner = spawner;
    }

    // --- Death判定（Trigger）
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Death"))
            TryRespawn();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Death"))
            TryRespawn();
    }

    // --- Bomとの接触（Collision）
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Bom"))
            TryRespawnIfNotBoosting();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Bom"))
            TryRespawnIfNotBoosting();
    }

    // ? Boost中ならBom衝突を無視
    private void TryRespawnIfNotBoosting()
    {
        if (CompareTag("Boost") || CompareTag("Dash"))
            return; // Boost中は無敵

        TryRespawn();
    }

    private void TryRespawn()
    {
        if (spawner == null) return;

        lifeCount--;
        UpdateLifeDisplay();

        if (lifeCount <= 0)
            GameOver();
        else
            spawner.ResetCharacterPosition();
    }

    private void GameOver()
    {
        Time.timeScale = 0f;

        if (gameOverUI != null)
            gameOverUI.SetActive(true);

        // ゲームオーバー時に残機を初期値に戻す
        lifeCount = MaxLives;
        UpdateLifeDisplay();
    }

    private void UpdateLifeDisplay()
    {
        if (lifeText != null)
            lifeText.text = lifeCount.ToString();
    }
}
