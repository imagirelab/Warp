using UnityEngine;

public class Launcherdelete : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �Փ˂����I�u�W�F�N�g�̃��C���[�`�F�b�N
        if (collision.gameObject.layer == LayerMask.NameToLayer("Missile"))
        {
            // Missile ������
            Destroy(gameObject);
        }
    }
}
