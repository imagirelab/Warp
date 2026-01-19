using UnityEngine;
using UnityEngine.UI;

public class BossHP : MonoBehaviour
{
    [Header("HP設定")]
    [SerializeField] private int maxHP = 200;
    private int currentHP;

    [Header("HPバー")]
    [SerializeField] private Slider hpSlider;

    [Header("スポーンするオブジェクト")]
    [SerializeField] private GameObject spawnPrefab;

    [Header("スポーン距離")]
    [SerializeField] private float spawnDistance = 10f;

    [Header("HP100以下で有効化するスクリプト名")]
    [SerializeField] private string targetScriptName;

    [Header("フェーズ切り替えHP")]
    [SerializeField] private int phase2HPThreshold = 100;

    private bool phase2Activated = false;

    private Transform player;

    private void Start()
    {
        currentHP = maxHP;

        hpSlider.maxValue = maxHP;
        hpSlider.value = currentHP;

        // Player取得
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogError("Player が見つかりません");
        }
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        currentHP = Mathf.Max(currentHP, 0);
        hpSlider.value = currentHP;

        // HP100以下で一度だけスクリプトを有効化
        if (!phase2Activated && currentHP <= phase2HPThreshold)
        {
            ActivateScriptByName();
        }

        if (currentHP <= 0)
        {
            Die();
        }
    }

    private void ActivateScriptByName()
    {
        phase2Activated = true;

        MonoBehaviour[] scripts = GetComponents<MonoBehaviour>();

        foreach (MonoBehaviour script in scripts)
        {
            if (script.GetType().Name == targetScriptName)
            {
                script.enabled = true;
                Debug.Log($"{targetScriptName} を有効化しました");
                return;
            }
        }

        Debug.LogWarning($"{targetScriptName} が見つかりませんでした");
    }

    private void Die()
    {
        if (player != null && spawnPrefab != null)
        {
            Vector3 spawnPos =
                player.position + player.right * spawnDistance;

            Instantiate(spawnPrefab, spawnPos, Quaternion.identity);
        }

        Debug.Log("Boss撃破");
        BossDefeatedFlag.IsDefeated = true;
        Destroy(gameObject);
    }
}
