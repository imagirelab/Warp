using UnityEngine;

public class ShowObject: MonoBehaviour
{
    [Header("ゴールしたら表示するオブジェクト群")]
    [SerializeField] private GameObject[] objectsToShow;

    [Header("ゴールしたら非表示にするオブジェクト群")]
    [SerializeField] private GameObject[] objectsToHide;

    private bool goalReached = false; // ゴール状態を記録

    private void Start()
    {
        // 初期状態設定
        foreach (var obj in objectsToShow)
        {
            if (obj != null)
                obj.SetActive(false);
        }

        foreach (var obj in objectsToHide)
        {
            if (obj != null)
                obj.SetActive(true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // プレイヤーに当たったら
        if (collision.gameObject.CompareTag("Player") && !goalReached)
        {
            goalReached = true;

            foreach (var obj in objectsToShow)
            {
                if (obj != null)
                    obj.SetActive(true); // まとめて表示
            }

            foreach (var obj in objectsToHide)
            {
                if (obj != null)
                    obj.SetActive(false); // まとめて非表示
            }
        }
    }
}
