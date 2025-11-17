using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TimeAttackManager : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    private float elapsedTime = 0f;
    private bool isRunning = false;

    private static TimeAttackManager instance;

    void Awake()
    {
        // シングルトンとして保持（重複生成を防ぐ）
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded; // シーンロード時に呼ばれるイベント登録
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        isRunning = true;
    }

    void Update()
    {
        if (isRunning)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimeDisplay();
        }
    }

    void UpdateTimeDisplay()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);
        int milliseconds = Mathf.FloorToInt((elapsedTime * 1000f) % 1000f);

        if (timeText != null)
            timeText.text = $"{minutes:00}:{seconds:00}.{milliseconds:000}";
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    public void StartTimer()
    {
        isRunning = true;
    }

    public float GetTime()
    {
        return elapsedTime;
    }

    public void ResetTimer()
    {
        elapsedTime = 0f;
    }

    // ?? 新しいシーンがロードされたら自動的にTimeTextを探す
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // シーン内に「TimeText」という名前のオブジェクトがあれば自動取得
        var textObj = GameObject.Find("TimeText");
        if (textObj != null)
        {
            timeText = textObj.GetComponent<TextMeshProUGUI>();
        }
        else
        {
            Debug.LogWarning("TimeText が見つかりませんでした。シーン内に存在しません。");
        }
    }

    void OnDestroy()
    {
        // イベントを解除してメモリリーク防止
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
