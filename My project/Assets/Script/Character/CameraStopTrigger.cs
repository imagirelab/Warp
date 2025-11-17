using UnityEngine;

public class CameraStopTrigger : MonoBehaviour
{
    [Header("’Ç]‰ğœ‚·‚éƒJƒƒ‰")]
    [SerializeField] private CameraFollow cameraFollow;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && cameraFollow != null)
        {
            cameraFollow.StopFollowing();
        }
    }
}