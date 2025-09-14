using UnityEngine;
using UnityEngine.UI;

public class ShowButtonOnKey : MonoBehaviour
{
    [SerializeField] private Button[] targetButtons;  // 表示/非表示を切り替えたいボタンを複数登録できる

    void Start()
    {
        // 最初は全部非表示にする
        SetButtonsActive(false);
    }

    void Update()
    {
        // スペースキーを押したらボタンを全部表示
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetButtonsActive(true);
        }

        // エスケープキーを押したらボタンを全部非表示
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetButtonsActive(false);
        }
    }

    // まとめてON/OFF切り替え
    private void SetButtonsActive(bool isActive)
    {
        foreach (Button btn in targetButtons)
        {
            if (btn != null)
            {
                btn.gameObject.SetActive(isActive);
            }
        }
    }
}
