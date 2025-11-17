using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("移動設定")]
    [SerializeField] private float speed = 5f;

    [Header("Boost設定")]
    [SerializeField] private float boostSpeed = 3f;
    [SerializeField] private float boostForce = 10f;
    [SerializeField] private float boostDistance = 3f;
    [SerializeField] private LayerMask hitMask;
    [SerializeField] private float bomCheckDistance = 7f;

    private Rigidbody2D rbody;
    private Animator animator;

    private float originalSpeed;
    private Vector2 originalDirection;

    private Vector2 boostDir;
    private Vector2 boostStartPos;
    private bool boosting = false;

    private string originalTag;

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        transform.rotation = Quaternion.Euler(0, 0, 0);

        originalSpeed = speed;
        originalDirection = Vector2.right;
        originalTag = gameObject.tag;
    }

    void FixedUpdate()
    {
        if (!boosting)
        {
            rbody.velocity = new Vector2(speed, rbody.velocity.y);
        }

        // アニメーション判定
        if (Mathf.Abs(rbody.velocity.y) > 0.01f)
        {
            animator.SetBool("isJumping", true);
            animator.SetBool("isRunning", false);
        }
        else
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("isRunning", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Boost")) return;

        boostDir = collision.transform.right.normalized;
        boostStartPos = rbody.position;

        // Boost方向にRayを飛ばして距離と次のBoostを確認
        RaycastHit2D hit = Physics2D.Raycast(boostStartPos, boostDir, boostDistance, hitMask);

        float actualDistance = boostDistance;
        GameObject nextBoost = null;

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Obstacle"))
            {
                actualDistance = hit.distance - 0.1f;
            }
            else if (hit.collider.CompareTag("Boost"))
            {
                actualDistance = hit.distance;
                nextBoost = hit.collider.gameObject;
            }
        }

        // Boost処理開始
        rbody.AddForce(boostDir * boostForce, ForceMode2D.Impulse);
        speed += boostSpeed;

        if (!boosting)
        {
            boosting = true;
            gameObject.tag = "Dash"; // Dash状態に変更
            StartCoroutine(BoostCoroutine(actualDistance, nextBoost));
        }
    }

    private IEnumerator BoostCoroutine(float distance, GameObject nextBoost)
    {
        while (boosting)
        {
            rbody.velocity = boostDir.normalized * speed;

            if (Vector2.Distance(boostStartPos, rbody.position) >= distance)
            {
                if (nextBoost != null)
                {
                    boosting = false;
                    OnTriggerEnter2D(nextBoost.GetComponent<Collider2D>());
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
        speed = originalSpeed;
        rbody.velocity = originalDirection * speed;
        boosting = false;
        gameObject.tag = originalTag; // 元のタグに戻す
    }

    // Bomとの衝突処理
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Boost中（Dash状態）は無敵
        if (boosting && collision.collider.CompareTag("Bom"))
        {
            // Bomを上に飛ばす
            Rigidbody2D bomRb = collision.collider.GetComponent<Rigidbody2D>();
            if (bomRb != null)
            {
                bomRb.velocity = Vector2.up * 20f;
            }
            return; // ダメージ無効
        }

        // Boost中にObstacleに当たったらBoost終了
        if (collision.collider.CompareTag("Obstacle"))
        {
            if (boosting)
                ResetBoost();
        }
    }
}
