using UnityEngine;

public class QuitGame : MonoBehaviour
{
    // この関数をButtonのOnClick()に登録する
    public void Quit()
    {
#if UNITY_EDITOR
        // エディタ上では再生停止
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // ビルドではアプリ終了
        Application.Quit();
#endif
    }
}
