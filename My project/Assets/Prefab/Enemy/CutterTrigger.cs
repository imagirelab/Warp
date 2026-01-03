using UnityEngine;

public class CutterTrigger : MonoBehaviour
{
    [Header("一度だけ生成するか")]
    [SerializeField] private bool spawnOnce = true;

    private CutterSpawner linkedSpawner;
    private bool hasSpawned = false;

    private void Start()
    {
        // TrapSpawn タグの付いたシーン上オブジェクトを取得
        GameObject spawnObj = GameObject.FindWithTag("TrapSpawn");

        if (spawnObj != null)
        {
            linkedSpawner = spawnObj.GetComponent<CutterSpawner>();

            if (linkedSpawner == null)
            {
                Debug.LogError("TrapSpawn に CutterSpawner が付いていません");
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
            linkedSpawner.SpawnCutter();

            if (spawnOnce)
                hasSpawned = true;
        }
    }
}
