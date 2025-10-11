using UnityEngine;

public class PlayerDeathHandler : MonoBehaviour
{
    private CharacterSpawner spawner;

    public void SetSpawner(CharacterSpawner spawner)
    {
        this.spawner = spawner;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Death"))
        {
            TryRespawn();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Death"))
        {
            TryRespawn();
        }
    }

    private void TryRespawn()
    {
        if (spawner != null)
        {
            spawner.ResetCharacterPosition();
        }
    }
}
