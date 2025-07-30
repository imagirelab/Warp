using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject objectToSpawn1; //���N���b�N�ŏo������I�u�W�F�N�g
    [SerializeField] private GameObject objectToSpawn2; //�E�N���b�N�ŏo������I�u�W�F�N�g

    private GameObject currentObject1; //���ݏo�����Ă���objectToSpawn1
    private GameObject currentObject2; //���ݏo�����Ă���objectToSpawn2

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //���N���b�N
        {
            //���łɃI�u�W�F�N�g�����݂��Ă�����폜
            if (currentObject1 != null)
            {
                Destroy(currentObject1);
            }

            //�}�E�X�ʒu���擾
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10f; // �J��������̓K�؂ȋ�����ݒ�

            //��ʍ��W�����[���h���W�ɕϊ�
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            //�V�����I�u�W�F�N�g�𐶐�
            currentObject1 = Instantiate(objectToSpawn1, worldPosition, Quaternion.identity);
        }

        if (Input.GetMouseButtonDown(1)) // �E�N���b�N
        {
            //���łɃI�u�W�F�N�g�����݂��Ă�����폜
            if (currentObject2 != null)
            {
                Destroy(currentObject2);
            }

            //�}�E�X�ʒu���擾
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10f; //�J��������̓K�؂ȋ�����ݒ�

            //��ʍ��W�����[���h���W�ɕϊ�
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            //�V�����I�u�W�F�N�g�𐶐�
            currentObject2 = Instantiate(objectToSpawn2, worldPosition, Quaternion.identity);
        }
    }
}
