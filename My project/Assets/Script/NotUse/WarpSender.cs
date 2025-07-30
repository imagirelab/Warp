using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpSender : MonoBehaviour
{
    [SerializeField] private Transform warpTarget; //���[�v��
    [SerializeField] private float launchForce = 5f; //��яo����

    private void Update()
    {
        //warpTarget���ݒ肳��Ă��Ȃ��ꍇ�A�^�O�ɉ��������[�v�^�[�Q�b�g���Ď擾
        if (warpTarget == null)
        {
            if (CompareTag("Warp1"))
            {
                GameObject warpTargetObject = GameObject.FindGameObjectWithTag("WarpP2");
                if (warpTargetObject != null)
                {
                    warpTarget = warpTargetObject.transform;
                    Debug.Log("Warp1�^�O�Ȃ̂�Warp2�^�[�Q�b�g���Đݒ�");
                }
                else
                {
                    Debug.LogWarning("Warp2�^�O�̃I�u�W�F�N�g��������܂���ł���");
                }
            }
            else if (CompareTag("Warp2"))
            {
                GameObject warpTargetObject = GameObject.FindGameObjectWithTag("WarpP1");
                if (warpTargetObject != null)
                {
                    warpTarget = warpTargetObject.transform;
                    Debug.Log("Warp2�^�O�Ȃ̂�Warp1�^�[�Q�b�g���Đݒ�");
                }
                else
                {
                    Debug.LogWarning("Warp1�^�O�̃I�u�W�F�N�g��������܂���ł���");
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (warpTarget != null)
        {
            Rigidbody2D targetRb = collision.collider.GetComponent<Rigidbody2D>();

            if (targetRb != null)
            {
                // ���[�v������
                collision.transform.position = warpTarget.position;

                // ���x���Z�b�g
                targetRb.velocity = Vector2.zero;

                // warpTarget�̉�]���g���āA��яo�������𐳂����v�Z
                Vector2 launchDirection = warpTarget.rotation * Vector3.up;

                // ��яo��
                targetRb.AddForce(launchDirection * launchForce, ForceMode2D.Impulse);
            }
            else
            {
                Debug.LogWarning("���[�v�Ώۂ�Rigidbody2D�����Ă��܂���I");
            }
        }
        else
        {
            Debug.LogWarning("WarpSender�Ƀ��[�v�悪�ݒ肳��Ă��܂���I");
        }
    }
}
