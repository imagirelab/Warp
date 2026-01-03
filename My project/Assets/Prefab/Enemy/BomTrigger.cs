using UnityEngine;

public class BomTrigger : MonoBehaviour
{
    [Header("一度だけ生成するか")]
    [SerializeField] private bool spawnOnce = true;

    [Header("スポーンに使う BomSpawner（Inspectorで指定）")]
    [SerializeField] private BomSpawner linkedSpawner;

    private bool hasSpawned = false;

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
