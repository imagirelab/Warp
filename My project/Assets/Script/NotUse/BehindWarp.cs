using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehindWarp : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // ���N���b�N�Ŏ��s
        {
            Vector2 origin = transform.position;

            // �}�E�X�ʒu�����[���h���W�ɕϊ�
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mouseWorldPos - origin).normalized;
            float distance = Vector2.Distance(origin, mouseWorldPos);

            // ���C�L���X�g��S���擾�i���ׂẴq�b�g�𒲂ׂ�j
            RaycastHit2D[] hits = Physics2D.RaycastAll(origin, direction, distance);

            Transform farthestEnemy = null;
            float maxDist = -1f;

            foreach (var hit in hits)
            {
                if (hit.collider != null && hit.collider.CompareTag("Enemy"))
                {
                    float d = Vector2.Distance(origin, hit.point);
                    if (d > maxDist)
                    {
                        maxDist = d;
                        farthestEnemy = hit.collider.transform;
                    }
                }
            }

            if (farthestEnemy != null)
            {
                if (farthestEnemy.childCount > 0)
                {
                    Transform child = farthestEnemy.GetChild(0);
                    transform.position = child.position;
                    Debug.Log("��ԉ��� Enemy �̎q�Ƀ��[�v: " + child.name);
                }
                else
                {
                    Debug.Log("Enemy �Ɏq�I�u�W�F�N�g������܂���");
                }
            }
            else
            {
                Debug.Log("Enemy �^�O�̂����I�u�W�F�N�g��������܂���ł���");
            }
        }
    }
}
