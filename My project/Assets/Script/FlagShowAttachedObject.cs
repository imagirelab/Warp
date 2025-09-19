using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class FlagShowAttachedObject : MonoBehaviour
{
#if UNITY_EDITOR
    [SerializeField] private SceneAsset sceneAsset; // Inspectorでアタッチするシーン
#endif
    [SerializeField] private FadeController fadeController; // フェード制御用

    private string sceneName;

    private void Awake()
    {
#if UNITY_EDITOR
        if (sceneAsset != null)
        {
            sceneName = sceneAsset.name;
        }
#endif
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player") && fadeController != null)
        {
            if (!string.IsNullOrEmpty(sceneName))
            {
                fadeController.StartFade(sceneName);
            }
        }
    }
}
