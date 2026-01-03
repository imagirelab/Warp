using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTrap : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            Debug.Log("当たった");

            // 当たったオブジェクトを下に10移動
            Transform hitObj = collision.transform;
            hitObj.position += Vector3.down * 10f;
        }
    }
}
