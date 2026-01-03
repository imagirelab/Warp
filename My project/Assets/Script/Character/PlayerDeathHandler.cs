using System.Collections;
using UnityEngine;
using TMPro;

public class PlayerDeathHandler : MonoBehaviour
{
    private CharacterSpawner spawner;

    private static int lifeCount = -1;
    private const int MaxLives = 2;

    private TextMeshProUGUI lifeText;
    private GameObject gameOverUI;

    [Header("リスポーン遅延")]
    [SerializeField] private float respawnDelay = 0.8f;

    private bool isDead = false;

    private void Awake()
    {
        if (lifeCount < 0)
            lifeCount = MaxLives;
    }

    private void Start()
    {
        gameOverUI = GameObject.Find("GameOver");
        if (gameOverUI != null)
            gameOverUI.SetActive(false);

        var obj = GameObject.Find("LifeText");
        if (obj != null)
            lifeText = obj.GetComponent<TextMeshProUGUI>();

        UpdateLifeDisplay();
    }

    public void SetSpawner(CharacterSpawner spawner)
    {
        this.spawner = spawner;
    }

    // --- Death（Trigger）
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Death"))
            StartDeathProcess();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Death"))
            StartDeathProcess();
    }

    // --- Bom（Collision）
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

    // Boost中ならBom無視
    private void TryRespawnIfNotBoosting()
    {
        if (CompareTag("Boost") || CompareTag("Dash"))
            return;

        StartDeathProcess();
    }

    // ===== 死亡処理開始 =====
    private void StartDeathProcess()
    {
        if (isDead) return;
        isDead = true;

        StartCoroutine(DeathCoroutine());
    }

    private IEnumerator DeathCoroutine()
    {
        // ここで死亡SE・アニメ・エフェクトを入れられる
        yield return new WaitForSeconds(respawnDelay);

        Stage.ResetSpawnFlag();

        if (spawner == null)
            yield break;

        lifeCount--;
        UpdateLifeDisplay();

        if (lifeCount <= 0)
        {
            GameOver();
        }
        else
        {
            spawner.ResetCharacterPosition();
            isDead = false;
        }
    }

    private void GameOver()
    {
        Time.timeScale = 0f;

        if (gameOverUI != null)
            gameOverUI.SetActive(true);

        lifeCount = MaxLives;
        UpdateLifeDisplay();
    }

    private void UpdateLifeDisplay()
    {
        if (lifeText != null)
            lifeText.text = lifeCount.ToString();
    }
}
