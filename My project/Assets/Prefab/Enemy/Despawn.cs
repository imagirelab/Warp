using UnityEngine;

public class Despawn : MonoBehaviour
{
    [SerializeField] private float lifetime = 10f; // Á‚¦‚é‚Ü‚Å‚ÌŠÔ

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }
}
