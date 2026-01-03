using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TimeAttackManager : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    private float elapsedTime = 0f;
    private bool isRunning = false;

    private static TimeAttackManager instance;

    // GoalTrigger で使うための公開アクセサ
    public static TimeAttackManager Instance => instance;

    [Header("シーンが変わったら自動でタイマー開始")]
    public bool autoStart = true;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
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
        if (timeText == null) return;

        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);
        int milliseconds = Mathf.FloorToInt((elapsedTime * 1000f) % 1000f);
        timeText.text = $"{minutes:00}:{seconds:00}.{milliseconds:000}";
    }

    public void StartTimer()
    {
        isRunning = true;
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    public void ResetTimer()
    {
        elapsedTime = 0f;
        UpdateTimeDisplay();
    }

    public float GetTime()
    {
        return elapsedTime;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 新しいシーンのUIテキスト探す
        var obj = GameObject.Find("TimeText");
        if (obj != null)
            timeText = obj.GetComponent<TextMeshProUGUI>();

        if (autoStart)
        {
            ResetTimer();
            StartTimer();
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
