using UnityEngine;

public class Stage : MonoBehaviour
{
    [SerializeField] private GameObject stagePrefab;

    private static bool hasSpawned = false;

    private void Start()
    {
        TrySpawn();
    }

    // 生成処理
    private void TrySpawn()
    {
        if (hasSpawned) return;
        hasSpawned = true;

        SpawnStage();
    }

    public void SpawnStage()
    {
        Instantiate(stagePrefab, transform.position, Quaternion.identity);
    }

    // ★ リスポーン時に外から呼ぶ用
    public static void ResetSpawnFlag()
    {
        hasSpawned = false;
    }
}
