using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerDeathHandler : MonoBehaviour
{
    private CharacterSpawner spawner;

    private static int lifeCount = -1;
    private const int MaxLives = 2;

    private TextMeshProUGUI lifeText;
    private GameObject gameOverUI;
    private GameObject titleUI;

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
        // GameOver UI
        gameOverUI = GameObject.Find("GameOver");
        if (gameOverUI != null)
            gameOverUI.SetActive(false);

        // TITLE UI
        titleUI = GameObject.Find("TITLE (1)");
        if (titleUI != null)
            titleUI.SetActive(false);

        // Life 表示
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
        // 死亡演出待ち
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

    // ===== GameOver =====
    private void GameOver()
    {
        Time.timeScale = 0f;

        // ★ 親を含めて強制的に表示
        ForceShow(gameOverUI);
        ForceShow(titleUI);

        if (gameOverUI != null)
            gameOverUI.SetActive(true);

        if (titleUI != null)
            titleUI.SetActive(true);

        // ★ LifeText を非表示
        if (lifeText != null)
            lifeText.gameObject.SetActive(false);

        // 次回用にライフ初期化
        lifeCount = MaxLives;
    }


    // ★ 親を遡ってすべて ON にする
    private void ForceShow(GameObject target)
    {
        if (target == null) return;

        Transform t = target.transform;
        while (t != null)
        {
            if (!t.gameObject.activeSelf)
                t.gameObject.SetActive(true);

            t = t.parent;
        }
    }

    // ★ TITLEボタンから呼ぶ
    public void ReturnToTitle()
    {
        Time.timeScale = 1f;   // timeScale戻し忘れ防止
        SceneManager.LoadScene("Title");
    }

    private void UpdateLifeDisplay()
    {
        if (lifeText != null)
            lifeText.text = lifeCount.ToString();
    }
}
