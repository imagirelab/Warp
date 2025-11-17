using System.Collections;
using UnityEngine;

public class BomBoost : MonoBehaviour
{
    [Header("Boost設定")]
    [SerializeField] private float boostSpeed = 3f;       // 通常速度に上乗せされる速度
    [SerializeField] private float boostForce = 10f;      // ブースト初速
    [SerializeField] private float boostDistance = 3f;    // 移動距離
    [SerializeField] private LayerMask hitMask;           // 障害物検知用
    [SerializeField] private float dashBounceForce = 12f; // Dashに当たったときの上方向跳ね力

    private Rigidbody2D rbody;
    private Collider2D col;
    private bool boosting = false;

    private Vector2 boostDir;
    private Vector2 boostStartPos;

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    private void FixedUpdate()
    {
        if (!boosting)
            return;
    }

    // ---- Boostタグ（Trigger）でブースト開始 ----
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Boost"))
            return;

        StartBoost(other.transform.right.normalized);
    }

    private void StartBoost(Vector2 direction)
    {
        boostDir = direction;
        boostStartPos = rbody.position;

        // Boost方向にRayを飛ばして距離を確認
        RaycastHit2D hit = Physics2D.Raycast(boostStartPos, boostDir, boostDistance, hitMask);

        float actualDistance = boostDistance;
        GameObject nextBoost = null;

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Obstacle"))
                actualDistance = hit.distance - 0.1f;
            else if (hit.collider.CompareTag("Boost"))
                nextBoost = hit.collider.gameObject;
        }

        // Boost処理開始
        rbody.AddForce(boostDir * boostForce, ForceMode2D.Impulse);
        boosting = true;

        StartCoroutine(BoostCoroutine(actualDistance, nextBoost));
    }

    // ---- Dashタグ（Collider）で跳ねる ----
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Dash"))
        {
            // Dashに当たったら自分を一時的にTriggerにして上に上昇
            StartCoroutine(DashBounceCoroutine());
        }
    }

    private IEnumerator DashBounceCoroutine()
    {
        // ColliderをTriggerに変更
        col.isTrigger = true;

        // 上方向に跳ねる
        rbody.velocity = Vector2.zero;
        rbody.AddForce(Vector2.up * dashBounceForce, ForceMode2D.Impulse);

        // 少し待ってから元に戻す（0.1秒で十分）
        yield return new WaitForSeconds(0.1f);

        col.isTrigger = false;
    }

    private IEnumerator BoostCoroutine(float distance, GameObject nextBoost)
    {
        float speed = boostSpeed;

        while (boosting)
        {
            rbody.velocity = boostDir.normalized * speed;

            if (Vector2.Distance(boostStartPos, rbody.position) >= distance)
            {
                if (nextBoost != null)
                {
                    boosting = false;
                    // 次のBoostで再Triggerされるのを待つ
                }
                else
                {
                    ResetBoost();
                }
            }

            yield return new WaitForFixedUpdate();
        }
    }

    private void ResetBoost()
    {
        boosting = false;
        rbody.velocity = Vector2.zero;
    }
}
