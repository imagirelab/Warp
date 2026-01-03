using UnityEngine;
using UnityEngine.UI;

public class BossHP : MonoBehaviour
{
    [Header("HP設定")]
    [SerializeField] private int maxHP = 300;
    private int currentHP;

    [Header("HPバー")]
    [SerializeField] private Slider hpSlider;

    [Header("スポーンするオブジェクト")]
    [SerializeField] private GameObject spawnPrefab;

    [Header("スポーン距離")]
    [SerializeField] private float spawnDistance = 10f;

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

        if (currentHP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (player != null && spawnPrefab != null)
        {
            // プレイヤーの向いている方向（2D想定）
            Vector3 spawnPos =
                player.position + player.right * spawnDistance;

            Instantiate(spawnPrefab, spawnPos, Quaternion.identity);
        }

        Debug.Log("Boss撃破");
        BossDefeatedFlag.IsDefeated = true;
        Destroy(gameObject);
    }
}
