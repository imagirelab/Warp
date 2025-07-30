using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpP : MonoBehaviour
{
    public float maxDistance = 3f;
    public Transform WarpPoint;  // ���[�v��� Transform

    void Update()
    {
        if (Input.GetMouseButtonDown(1))  // �E�N���b�N
        {
            Vector2 origin = transform.position;
            Vector2 direction = transform.up;

            RaycastHit2D hit = Physics2D.Raycast(origin, direction, maxDistance);

            if (hit.collider != null)
            {
                Transform hitTransform = hit.collider.transform;

                if (hitTransform.childCount > 0)
                {
                    WarpPoint = hitTransform.GetChild(0);
                    Debug.Log("�q�b�g�����I�u�W�F�N�g�̎q��WarpPoint�ɐݒ�: " + WarpPoint.name);
                }
                else
                {
                    Debug.Log("�q�b�g�����I�u�W�F�N�g�Ɏq������܂���");
                    WarpPoint = null;
                }
            }
            else
            {
                if (transform.childCount > 0)
                {
                    WarpPoint = transform.GetChild(0);
                    Debug.Log("�q�b�g���Ȃ������̂ŁA�����̎q��WarpPoint�ɐݒ�: " + WarpPoint.name);
                }
                else
                {
                    Debug.Log("�������g�ɂ��q������܂���");
                    WarpPoint = null;
                }
            }
        }
    }
}
