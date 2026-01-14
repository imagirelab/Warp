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

    [Header("接地判定")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance = 0.1f;
    [SerializeField] private LayerMask Obstacle;

    [Header("効果音")]
    [SerializeField] private AudioSource seSource;
    [SerializeField] private AudioClip jumpSE;
    [SerializeField] private AudioClip boostSE;
    [SerializeField] private AudioClip boostBomSE;
    [SerializeField] private AudioClip footstep1;
    [SerializeField] private AudioClip footstep2;

    [Header("足音設定")]
    [SerializeField] private float footstepInterval = 0.35f;

    private Rigidbody2D rbody;
    private Animator animator;

    private float originalSpeed;
    private Vector2 originalDirection;

    private Vector2 boostDir;
    private Vector2 boostStartPos;
    private bool boosting = false;

    private string originalTag;

    private bool wasGrounded = true;

    private Coroutine footstepCoroutine;
    private bool footToggle = false;

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

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

        bool isGrounded = IsGrounded();
        bool isRunning = isGrounded && !boosting;

        // ★ 地面が無い状態に入った瞬間だけジャンプSE
        if (!isGrounded && wasGrounded)
        {
            seSource.PlayOneShot(jumpSE);
        }

        wasGrounded = isGrounded;

        // アニメーション
        animator.SetBool("isJumping", !isGrounded);
        animator.SetBool("isRunning", isRunning);
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(
            groundCheck.position,
            Vector2.down,
            groundCheckDistance,
            Obstacle
        );

        return hit.collider != null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Boost")) return;

        // Boost効果音
        seSource.PlayOneShot(boostSE);

        boostDir = collision.transform.right.normalized;
        boostStartPos = rbody.position;

        RaycastHit2D hit = Physics2D.Raycast(
            boostStartPos,
            boostDir,
            boostDistance,
            hitMask
        );

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

        rbody.AddForce(boostDir * boostForce, ForceMode2D.Impulse);
        speed += boostSpeed;

        if (!boosting)
        {
            boosting = true;
            gameObject.tag = "Dash";
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
        gameObject.tag = originalTag;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (boosting)
        {
            // ★ Boost中にBomに当たった時
            if (collision.collider.CompareTag("Bom"))
            {
                if (boostBomSE != null)
                {
                    seSource.PlayOneShot(boostBomSE);
                }

                Rigidbody2D bomRb = collision.collider.GetComponent<Rigidbody2D>();
                if (bomRb != null)
                {
                    Vector2 bounceDir = new Vector2(1f, 1f).normalized;
                    float force = 20f;
                    bomRb.velocity = bounceDir * force;
                }

                return;
            }

            // Flagは無敵
            if (collision.collider.CompareTag("Flag"))
            {
                return;
            }
        }

        // Obstacleに当たったらBoost終了
        if (collision.collider.CompareTag("Obstacle"))
        {
            if (boosting)
                ResetBoost();
        }
    }

    // ===== 足音ループ（必要なら復活）=====
    /*
    private IEnumerator FootstepLoop()
    {
        while (true)
        {
            footToggle = !footToggle;

            if (footToggle)
                seSource.PlayOneShot(footstep1);
            else
                seSource.PlayOneShot(footstep2);

            yield return new WaitForSeconds(footstepInterval);
        }
    }
    */
}
