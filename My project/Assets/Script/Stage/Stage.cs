using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage : MonoBehaviour
{
    [SerializeField] private GameObject stagePrefab;

    private static bool hasSpawned = false;

    private void Awake()
    {
        // ★ シーン切り替え時に呼ばれるイベント登録
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        // ★ イベント解除（重要）
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        TrySpawn();
    }

    // ===== シーンロード時 =====
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // ★ シーン切り替えで必ずリセット
        hasSpawned = false;
    }

    // 生成処理
    private void TrySpawn()
    {
        if (hasSpawned) return;
        hasSpawned = true;

        SpawnStage();
    }

    public void SpawnStage()
    {
        Instantiate(stagePrefab, transform.position, Quaternion.identity);
    }

    // ★ リスポーン時に外から呼ぶ用
    public static void ResetSpawnFlag()
    {
        hasSpawned = false;
    }
}
