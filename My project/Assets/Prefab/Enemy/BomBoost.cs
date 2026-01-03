using System.Collections;
using UnityEngine;

public class BomBoost : MonoBehaviour
{
    [Header("Boost設定")]
    [SerializeField] private float boostSpeed = 3f;
    [SerializeField] private float boostForce = 10f;
    [SerializeField] private float boostDistance = 3f;
    [SerializeField] private LayerMask hitMask;
    [SerializeField] private float dashBounceForce = 12f;

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
        if (!boosting) return;

        // Boost方向に一定速度で移動
        rbody.velocity = boostDir * boostSpeed;
    }

    // ---- Boostトリガー（矢印方向対応） ----
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Boost")) return;

        // Boostの向きをそのまま採用（→ や ↑ など）
        boostDir = other.transform.right.normalized;
        boostStartPos = rbody.position;

        // 距離と次Boost確認
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

        rbody.AddForce(boostDir * boostForce, ForceMode2D.Impulse);
        boosting = true;

        StartCoroutine(BoostCoroutine(actualDistance, nextBoost));
    }

    private IEnumerator BoostCoroutine(float distance, GameObject nextBoost)
    {
        while (boosting)
        {
            if (Vector2.Distance(boostStartPos, rbody.position) >= distance)
            {
                if (nextBoost != null)
                {
                    boosting = false;
                }
                else
                {
                    ResetBoost();
                }
            }

            yield return new WaitForFixedUpdate();
        }
    }

    // ---- Dashタグにぶつかったら上方向に跳ねる ----
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Dash")) return;

        StartCoroutine(DashBounceCoroutine());
    }

    private IEnumerator DashBounceCoroutine()
    {
        col.isTrigger = true;

        rbody.velocity = Vector2.zero;
        rbody.gravityScale = 1f;
        rbody.AddForce(Vector2.up * dashBounceForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(0.2f);

        col.isTrigger = false;
        yield return new WaitForSeconds(0.05f);
    }

    private void ResetBoost()
    {
        boosting = false;
        rbody.velocity = new Vector2(0, rbody.velocity.y);
        rbody.gravityScale = 1f;
    }
}
