using UnityEngine;

public class BomTrigger : MonoBehaviour
{
    [Header("どのスポナーを起動するか設定")]
    [SerializeField] private BomSpawner linkedSpawner; // Inspectorでペア設定

    [Header("一度だけ生成するか")]
    [SerializeField] private bool spawnOnce = true;

    private bool hasSpawned = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasSpawned && other.CompareTag("Player"))
        {
            if (linkedSpawner != null)
            {
                linkedSpawner.SpawnBomb();
                if (spawnOnce) hasSpawned = true;
            }
        }
    }
}
