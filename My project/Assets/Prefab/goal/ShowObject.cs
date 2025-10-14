using UnityEngine;

public class ShowObject : MonoBehaviour
{
    [SerializeField] private GameObject objectToShow; // 表示するオブジェクト

    private void Start()
    {
        if (objectToShow != null)
            objectToShow.SetActive(false); // 最初は非表示
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 衝突したオブジェクトがプレイヤーか判定
        if (collision.gameObject.CompareTag("Player"))
        {
            if (objectToShow != null)
                objectToShow.SetActive(true); // 表示
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // 離れたら非表示にする場合
        if (collision.gameObject.CompareTag("Player"))
        {
            if (objectToShow != null)
                objectToShow.SetActive(false);
        }
    }
}
