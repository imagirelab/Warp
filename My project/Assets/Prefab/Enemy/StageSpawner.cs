using UnityEngine;

public class StageSpawner : MonoBehaviour
{
    [SerializeField] private GameObject stagePrefab; // Project ‚Ì Prefab

    public void SpawnStage()
    {
        if (stagePrefab == null)
        {
            Debug.LogError("Prefab ‚ªİ’è‚³‚ê‚Ä‚¢‚Ü‚¹‚ñI");
            return;
        }

        GameObject obj = Instantiate(stagePrefab);
        obj.transform.position = transform.position;
    }
}
