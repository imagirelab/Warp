using UnityEngine;

public class DespawnStage : MonoBehaviour
{
    [SerializeField] private float lifetimeAfterHit = 10f;
    private bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasTriggered) return;

        if (other.CompareTag("Player"))
        {
            hasTriggered = true;

            // ルート（いちばん上の親）を取得
            Transform root = transform.root;

            // ルートオブジェクトごと削除
            Destroy(root.gameObject, lifetimeAfterHit);
        }
    }
}
