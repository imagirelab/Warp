using System.Collections;
using UnityEngine;

public class ThwompMove : MonoBehaviour
{
    [Header("移動設定")]
    [SerializeField] private float dropDistance = 5f;
    [SerializeField] private float dropSpeed = 10f;
    [SerializeField] private float riseSpeed = 5f;

    [Header("待機時間")]
    [SerializeField] private float waitBeforeDrop = 1f;
    [SerializeField] private float waitAtBottom = 0.2f;
    [SerializeField] private float waitAtTop = 1f;

    private Vector3 startLocalPos;
    private Vector3 targetLocalPos;

    private void Start()
    {
        // ローカル座標で保存
        startLocalPos = transform.localPosition;
        targetLocalPos = startLocalPos + Vector3.down * dropDistance;

        StartCoroutine(MoveLoop());
    }

    private IEnumerator MoveLoop()
    {
        yield return new WaitForSeconds(waitBeforeDrop);

        while (true)
        {
            // 落下
            yield return StartCoroutine(MoveTo(targetLocalPos, dropSpeed));

            // 下で待機
            yield return new WaitForSeconds(waitAtBottom);

            // 上昇
            yield return StartCoroutine(MoveTo(startLocalPos, riseSpeed));

            // 上で待機
            yield return new WaitForSeconds(waitAtTop);
        }
    }

    private IEnumerator MoveTo(Vector3 target, float speed)
    {
        while (Vector3.Distance(transform.localPosition, target) > 0.05f)
        {
            transform.localPosition = Vector3.MoveTowards(
                transform.localPosition,
                target,
                speed * Time.deltaTime
            );
            yield return null;
        }

        transform.localPosition = target;
    }
}
