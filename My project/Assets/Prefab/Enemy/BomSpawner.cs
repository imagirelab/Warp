using UnityEngine;

public class BomSpawner : MonoBehaviour
{
    [Header("スポーンするオブジェクト")]
    [SerializeField] private GameObject bombPrefab;

    // 外部から呼ばれたら生成
    public void SpawnBomb()
    {
        Instantiate(bombPrefab, transform.position, Quaternion.identity);
    }
}
