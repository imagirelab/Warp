using UnityEngine;
using System.Collections;

public class CutterSpawner : MonoBehaviour
{
    [Header("スポーンするオブジェクト")]
    [SerializeField] private GameObject cutterPrefab;

    [Header("1段階ごとの上昇量")]
    [SerializeField] private float moveUpAmount = 2f;

    [Header("1段階の移動時間（秒）")]
    [SerializeField] private float moveDuration = 0.5f; // ★0.5秒

    // 外部から呼ばれたら生成
    public void SpawnCutter()
    {
        GameObject cutter = Instantiate(
            cutterPrefab,
            transform.position,
            Quaternion.identity
        );

        if (Random.value <= 0.5f)
        {
            StartCoroutine(MoveUpTwoSteps(cutter.transform));
        }
    }

    // 2段階で上昇
    private IEnumerator MoveUpTwoSteps(Transform target)
    {
        if (target == null) yield break;

        // 1段階目（0.5秒）
        yield return StartCoroutine(MoveUpOverTime(target));

        if (target == null) yield break;

        // 2段階目（0.5秒）
        yield return StartCoroutine(MoveUpOverTime(target));
    }

    // 1段階分 上昇
    private IEnumerator MoveUpOverTime(Transform target)
    {
        if (target == null) yield break;

        Vector3 startPos = target.position;
        Vector3 endPos = startPos + Vector3.up * moveUpAmount;

        float elapsed = 0f;

        while (elapsed < moveDuration)
        {
            if (target == null) yield break;

            elapsed += Time.deltaTime;
            float t = elapsed / moveDuration;

            target.position = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }

        if (target != null)
            target.position = endPos;
    }
}
