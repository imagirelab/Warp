using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteLine : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bom"))
        {
            Destroy(other.gameObject);
        }
    }
}
