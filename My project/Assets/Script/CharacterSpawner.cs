using UnityEngine;
using UnityEngine.SceneManagement; // ← シーン再読み込みに必要

public class CharacterSpawner : MonoBehaviour
{
    [SerializeField] private GameObject characterPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Warp warpController; // Warpスクリプト参照

    private GameObject spawnedCharacter;

    private void OnEnable()
    {
        SpawnCharacter();
    }

    private void SpawnCharacter()
    {
        if (spawnedCharacter != null) return;

        if (characterPrefab != null && spawnPoint != null)
        {
            spawnedCharacter = Instantiate(characterPrefab, spawnPoint.position, spawnPoint.rotation);

            var deathHandler = spawnedCharacter.GetComponent<PlayerDeathHandler>();
            if (deathHandler != null)
            {
                deathHandler.SetSpawner(this);
            }

            if (warpController != null)
            {
                warpController.SetPlayerTransform(spawnedCharacter.transform);
            }
        }
        else
        {
            Debug.LogWarning("CharacterSpawner: キャラクターPrefabまたはSpawnPointが未設定です。");
        }
    }

    // ?? シーン全体を最初からリセットする
    public void ResetCharacterPosition()
    {
        // 現在のシーン名を取得して再読み込み
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);
    }
}
