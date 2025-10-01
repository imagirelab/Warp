using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("移動設定")]
    [SerializeField] private float speed = 5f;        // 通常速度
    [SerializeField] private float jumpP = 300f;      // ジャンプ力

    [Header("Boost設定")]
    [SerializeField] private float boostSpeed = 3f;    // 通過時に加算する速度
    [SerializeField] private float boostForce = 10f;   // 瞬間的に飛ばす力
    [SerializeField] private float boostDistance = 3f; // 効果の距離

    private Rigidbody2D rbody;
    private Animator animator;

    private float originalSpeed;
    private Vector2 originalDirection;

    private Vector2 boostDir;
    private Vector2 boostStartPos;
    private bool boosting = false;

    private string originalTag; // 元のタグを保持

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        transform.rotation = Quaternion.Euler(0, 0, 0);

        originalSpeed = speed;
        originalDirection = Vector2.right;

        originalTag = gameObject.tag; // 開始時のタグを保存
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(rbody.velocity.y) < 0.01f)
        {
            rbody.AddForce(transform.up * jumpP);
        }
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

        // 瞬間的に飛ばす
        rbody.AddForce(boostDir * boostForce, ForceMode2D.Impulse);

        // 速度加速
        speed += boostSpeed;

        if (!boosting)
        {
            boosting = true;
            gameObject.tag = "Dash"; // タグをDashに変更
            StartCoroutine(BoostCoroutine());
        }
    }

    private IEnumerator BoostCoroutine()
    {
        while (boosting)
        {
            rbody.velocity = boostDir.normalized * speed;

            // 一定距離を進んだら元に戻す
            if (Vector2.Distance(boostStartPos, rbody.position) >= boostDistance)
            {
                ResetBoost();
            }

            yield return new WaitForFixedUpdate();
        }
    }

    private void ResetBoost()
    {
        speed = originalSpeed;
        rbody.velocity = originalDirection * speed;
        boosting = false;
        gameObject.tag = originalTag; // タグを元に戻す
    }

    // 地面に着地したらBoost解除
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Obstacle"))
        {
            if (boosting)
            {
                ResetBoost();
            }
        }
    }
}
