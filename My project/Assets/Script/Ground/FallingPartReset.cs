using UnityEngine;

public class FallingPartReset : MonoBehaviour
{
    private Vector3 startPos;

    private void Awake()
    {
        startPos = transform.localPosition;
    }

    private void OnEnable()
    {
        transform.localPosition = startPos;
    }
}
