using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            FindObjectOfType<TimeAttackManager>().StopTimer();
            Debug.Log("ゴール！ タイム: " + FindObjectOfType<TimeAttackManager>().GetTime());
        }
    }
}
