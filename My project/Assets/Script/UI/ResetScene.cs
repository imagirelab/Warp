using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetScene : MonoBehaviour
{
    // ‚±‚ÌŠÖ”‚ğButton‚ÌOnClick()‚É“o˜^
    public void ResetCurrentScene()
    {
        // Œ»İ‚ÌƒV[ƒ“–¼‚ğæ“¾‚µ‚ÄÄ“Ç‚İ‚İ
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
