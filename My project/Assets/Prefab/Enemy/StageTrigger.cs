using UnityEngine;

public class StageTrigger : MonoBehaviour
{
    [Header("どのスポナーを起動するか設定")]
    [SerializeField] private StageSpawner linkedSpawner; // Inspectorでペア設定

    [Header("一度だけ生成するか")]
    [SerializeField] private bool spawnOnce = true;

    private bool hasSpawned = false;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!hasSpawned && other.CompareTag("Player"))
        {
            if (linkedSpawner != null)
            {
                linkedSpawner.SpawnStage();
                if (spawnOnce) hasSpawned = true;
            }
        }
    }
}
