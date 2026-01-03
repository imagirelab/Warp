using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    [Header("ゴール時に削除するオブジェクトのタグ")]
    [SerializeField] private string targetTag = "DestroyOnGoal";

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Player")) return;

        // タイムアタック停止
        if (TimeAttackManager.Instance != null)
        {
            TimeAttackManager.Instance.StopTimer();
            Debug.Log("ゴール！ タイム: " + TimeAttackManager.Instance.GetTime());
        }

        // タグで対象を全削除
        if (!string.IsNullOrEmpty(targetTag))
        {
            GameObject[] targets = GameObject.FindGameObjectsWithTag(targetTag);
            foreach (GameObject obj in targets)
            {
                Destroy(obj);
            }
        }
        else
        {
            Debug.LogWarning("GoalTrigger: 削除対象のタグが設定されていません。");
        }
    }
}
