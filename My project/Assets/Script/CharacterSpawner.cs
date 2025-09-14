using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    [SerializeField] private GameObject characterPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Warp warpController;        // Warpスクリプト参照

    private GameObject spawnedCharacter;

    private void OnEnable()
    {
        SpawnCharacter();
    }

    private void SpawnCharacter()
    {
        if (spawnedCharacter != null)
        {
            Destroy(spawnedCharacter);
        }

        if (characterPrefab != null && spawnPoint != null)
        {
            spawnedCharacter = Instantiate(characterPrefab, spawnPoint.position, spawnPoint.rotation);

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
}
