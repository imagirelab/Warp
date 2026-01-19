using UnityEngine;
using UnityEngine.UI;

public class ShowButtonsWhenActive : MonoBehaviour
{
    [Header("ï\é¶êßå‰Ç∑ÇÈÉ{É^Éì")]
    [SerializeField] private Button[] targetButtons;

    private void OnEnable()
    {
        SetButtonsActive(true);
    }

    private void OnDisable()
    {
        SetButtonsActive(false);
    }

    private void SetButtonsActive(bool isActive)
    {
        if (targetButtons == null) return;

        foreach (Button btn in targetButtons)
        {
            if (btn != null)
                btn.gameObject.SetActive(isActive);
        }
    }
}
