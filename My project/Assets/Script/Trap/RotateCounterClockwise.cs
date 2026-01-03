using UnityEngine;

public class RotateCounterClockwise : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 360f; // ‰ñ“]‘¬“x(‹/•b)

    private void Update()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}
