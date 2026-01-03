using UnityEngine;

public class TrapSpawnKiller : MonoBehaviour
{
    private void OnEnable()
    {
        if (BossDefeatedFlag.IsDefeated)
        {
            Destroy(gameObject);
        }
    }
}
