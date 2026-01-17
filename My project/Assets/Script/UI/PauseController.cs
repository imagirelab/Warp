using UnityEngine;

public class PauseController : MonoBehaviour
{
    private bool isPaused = false; // 現在のポーズ状態

    [SerializeField] private GameObject pauseUI; // ポーズ時に表示するUI
    [SerializeField] private GameObject TitleBotton; // ポーズ時に表示するUI

    void Start()
    {
        if (pauseUI != null)
            pauseUI.SetActive(false); // 最初は非表示
        if (TitleBotton != null)
            TitleBotton.SetActive(false);
    }

    void Update()
    {
        // エスケープキーが押されたら
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0f; // ゲームを停止
        isPaused = true;
        if (pauseUI != null)
            pauseUI.SetActive(true); // ポーズUI表示
        if (TitleBotton != null)
            TitleBotton.SetActive(true); // ポーズUI表示
    }

    void ResumeGame()
    {
        Time.timeScale = 1f; // ゲーム再開
        isPaused = false;
        if (pauseUI != null)
            pauseUI.SetActive(false); // ポーズUI非表示
        if (TitleBotton != null)
            TitleBotton.SetActive(false); // ポーズUI非表示
    }
}
