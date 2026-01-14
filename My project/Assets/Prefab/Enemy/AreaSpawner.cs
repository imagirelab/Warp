using UnityEngine;

public class AreaSpawner : MonoBehaviour
{
    [Header("スポーン設定")]
    [SerializeField] private GameObject spawnPrefab;
    [SerializeField] private float spawnRange = 5f;
    [SerializeField] private bool spawnOnce = true;

    private Transform player;
    private bool hasSpawned = false;

    void Start()
    {
        // シーン上に生成されたPlayerを探す
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogWarning("Playerが見つかりません。タグを確認してください。");
        }
    }

    void Update()
    {
        if (hasSpawned && spawnOnce) return;
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= spawnRange)
        {
            Spawn();
        }
    }

    private void Spawn()
    {
        Instantiate(spawnPrefab, transform.position, Quaternion.identity);
        hasSpawned = true;
    }

    // 範囲をSceneビューに表示
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, spawnRange);
    }
}
