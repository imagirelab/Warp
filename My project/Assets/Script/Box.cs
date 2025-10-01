using UnityEngine;

public class Box : MonoBehaviour
{
    [Header("依存先のオブジェクト（これが消えたら自分も消える）")]
    [SerializeField] private GameObject target;

    [SerializeField] private float checkInterval = 0.1f; // チェック間隔(秒)

    private void Start()
    {
        if (target != null)
        {
            // 定期的に target を監視する
            InvokeRepeating(nameof(CheckTarget), checkInterval, checkInterval);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Dash"))
        {
            Destroy(gameObject);
        }
    }

    private void CheckTarget()
    {
        if (target == null)
        {
            Destroy(gameObject);
        }
    }
}
