using UnityEngine;

public class AutoDespawn : MonoBehaviour
{
    [SerializeField] private float destroyTime = 30f; // 60•b

    private void Start()
    {
        Destroy(gameObject, destroyTime);
    }
}
