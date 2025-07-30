using UnityEngine;

public class WarpDistance : MonoBehaviour
{
    [SerializeField] private float checkDistance = 0.5f; // Trap�܂ł̋����������l

    private float timer = 0f;
    private bool isTimerRunning = false;

    void Update()
    {
        if (isTimerRunning)
        {
            timer += Time.deltaTime;
            if (timer >= 3f)
            {
                Manager.warpDistance = 1.0f;
                Debug.Log("3�b�o�߂����̂� Manager.warpDistance �� 1.0 �ɖ߂���");
                isTimerRunning = false;
                timer = 0f;
            }
        }
    }

    public void CheckNearTrap(Vector3 warpPos)
    {
        GameObject[] traps = GameObject.FindGameObjectsWithTag("Trap");
        foreach (GameObject trap in traps)
        {
            float distance = Vector3.Distance(warpPos, trap.transform.position);
            if (distance <= checkDistance)
            {
                Manager.warpDistance = 0.5f;
                Debug.Log("Trap���߂��̂� Manager.warpDistance = 0.5 �ɂ���");

                // 3�b��Ɍ��ɖ߂����߂̃^�C�}�[�J�n
                timer = 0f;
                isTimerRunning = true;
                return;
            }
        }

        // Trap���߂��ɖ����ꍇ�A�������Ȃ��i3�b��ɖ߂��j
    }
}
