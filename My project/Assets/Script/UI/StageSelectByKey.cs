using UnityEngine;
using UnityEngine.UI;

public class StageSelectByKey : MonoBehaviour
{
    [Header("ステージボタン（順番に1?4）")]
    [SerializeField] private Button stage1Button;
    [SerializeField] private Button stage2Button;
    [SerializeField] private Button stage3Button;
    [SerializeField] private Button stage4Button;

    private Button[] allButtons;

    private void Awake()
    {
        allButtons = new Button[]
        {
            stage1Button,
            stage2Button,
            stage3Button,
            stage4Button
        };

        // ★ 初期状態：Stage1 だけ表示
        SetActiveButton(stage1Button);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetActiveButton(stage1Button);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetActiveButton(stage2Button);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetActiveButton(stage3Button);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SetActiveButton(stage4Button);
        }
    }

    private void SetActiveButton(Button activeButton)
    {
        foreach (Button button in allButtons)
        {
            if (button == null) continue;

            // 選択されたものだけ表示
            button.gameObject.SetActive(button == activeButton);
        }
    }
}
