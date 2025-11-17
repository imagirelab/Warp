using UnityEngine;
using TMPro;

public class NumberCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI numberText; // Canvas上のUIテキスト
    [SerializeField] private int initialValue = 2;       // 初期値

    private const string DeathCountKey = "TotalDeaths"; // PlayerPrefsで死亡累計を保存

    // 全インスタンスで共有する変数
    private static int totalDeaths = -1;
    private static int currentValue = -1;

    private void Awake()
    {
        // 初回起動時のみPlayerPrefsから読み込み
        if (totalDeaths < 0)
        {
            totalDeaths = PlayerPrefs.GetInt(DeathCountKey, 0);
            currentValue = Mathf.Max(0, initialValue - totalDeaths);
        }

        UpdateText();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            DecreaseNumber(); // 初期値から死亡累計を引いて表示
        }
    }

    private void DecreaseNumber()
    {
        totalDeaths++;
        PlayerPrefs.SetInt(DeathCountKey, totalDeaths);
        PlayerPrefs.Save();

        currentValue = Mathf.Max(0, initialValue - totalDeaths);

        UpdateAllInstancesText();

        Debug.Log($"現在の数字: {currentValue} (死亡累計: {totalDeaths})");
    }

    private void UpdateText()
    {
        if (numberText != null)
        {
            numberText.text = currentValue.ToString();
        }
    }

    // すべての NumberCounter インスタンスのテキストを更新
    private void UpdateAllInstancesText()
    {
        NumberCounter[] allCounters = FindObjectsOfType<NumberCounter>();
        foreach (var counter in allCounters)
        {
            counter.UpdateText();
        }
    }

    // デバッグ用：死亡累計をリセット
    [ContextMenu("Reset Death Count")]
    private void ResetDeaths()
    {
        totalDeaths = 0;
        currentValue = initialValue;
        PlayerPrefs.DeleteKey(DeathCountKey);
        UpdateAllInstancesText();
        Debug.Log("死亡累計をリセットしました");
    }
}
