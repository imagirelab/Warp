using UnityEngine;

public class BossBomSpawn : MonoBehaviour
{
    [Header("一度だけ生成するか")]
    [SerializeField] private bool spawnOnce = true;

    private BomSpawner linkedSpawner;
    private bool hasSpawned = false;

    private void Start()
    {
        // TrapSpawn タグの付いた「シーン上の BomSpawner」を取得
        GameObject spawnObj = GameObject.FindWithTag("TrapSpawn");

        if (spawnObj != null)
        {
            linkedSpawner = spawnObj.GetComponent<BomSpawner>();

            if (linkedSpawner == null)
            {
                Debug.LogError("TrapSpawn に BomSpawner が付いていません");
            }
        }
        else
        {
            Debug.LogError("TrapSpawn タグのオブジェクトがシーンに存在しません");
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!hasSpawned && other.CompareTag("Player") && linkedSpawner != null)
        {
            linkedSpawner.SpawnBomb();

            if (spawnOnce)
                hasSpawned = true;
        }
    }
}