using UnityEngine;

public class FlagShowAttachedObject : MonoBehaviour
{
    [SerializeField] private GameObject objectToShow;        // フェード後に表示したいオブジェクト
    [SerializeField] private FadeController fadeController;  // フェード制御用

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player") && fadeController != null)
        {
            if (objectToShow != null)
            {
                fadeController.StartFade(objectToShow);
            }
        }
    }
}
