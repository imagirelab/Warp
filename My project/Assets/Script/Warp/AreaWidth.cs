using UnityEngine;

public class AreaWidth : MonoBehaviour
{
    private float baseScale = 2.0f;         // �ʏ�T�C�Y
    private float expandedScale = 20.0f;    // �ő�g��T�C�Y
    private float expandSpeed = 2.0f;       // �g��X�s�[�h

    private float currentScale;

    public float warp = 0f;                // ���[�v�����i���a�j
    public bool justReleased = false;      // ���[�v�������X�V�ς݂��t���O

    private void Start()
    {
        currentScale = baseScale;
        transform.localScale = new Vector3(currentScale, currentScale, 1f);
    }

    public void Update()
    {
        if (!Input.GetMouseButton(1))
        {
            if (!justReleased)
            {
                warp = currentScale;
                justReleased = true;
            }
            currentScale = baseScale;
        }
        else
        {
            justReleased = false;
            currentScale = Mathf.MoveTowards(currentScale, expandedScale, expandSpeed * Time.deltaTime);
        }

        transform.localScale = new Vector3(currentScale, currentScale, 1f);
    }

    public float CurrentScale()
    {
        return warp * 0.5f * 0.5f;
    }

    public bool JustReleased
    {
        get => justReleased;
        set => justReleased = value;
    }
}
