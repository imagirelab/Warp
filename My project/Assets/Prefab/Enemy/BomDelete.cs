using UnityEngine;

public class BomDelete : MonoBehaviour
{
    [SerializeField] private int damageToBoss = 30;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Hit(collision.collider);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Hit(other);
    }

    private void Hit(Collider2D collider)
    {
        if (collider.CompareTag("Enemy"))
        {
            Debug.Log("”š”­");
            Destroy(gameObject);
        }
        else if (collider.CompareTag("Boss"))
        {
            Debug.Log("Boss‚Éƒqƒbƒg");

            BossHP bossHP = collider.GetComponent<BossHP>();
            if (bossHP != null)
            {
                bossHP.TakeDamage(damageToBoss);
            }

            Destroy(gameObject);
        }
    }
}
