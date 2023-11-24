using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool moveLeft;
    private bool moveRight;
    private float horizontalMove;
    private float speed = 3;
    private bool jump;
    bool isJumping;

    SpriteRenderer sr;
    Animator anim;

    public LayerMask groundLayer;
    public LayerMask enemyLayer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        moveLeft = false;
        moveRight = false;
        jump = false;

        isJumping = false;
    }

    public void PointerDownLeft()
    {
        moveLeft = true;
    }

    public void PointerUpLeft()
    {
        moveLeft = false;
    }

    public void PointerDownRight()
    {
        moveRight = true;
    }

    public void PointerUpRight()
    {
        moveRight = false;
    }

    public void PointerClickJump()
    {
        if (IsGrounded())
        {
            jump = true;
        }
    }

    public void PointerUpJump()
    {
        jump = false;
    }

    void Update()
    {
        anim.SetBool("Run", false);
        anim.SetBool("Jump", false);

        MovementPlayer();

        if (EnemyRight())
        {
            LoadDeath();
        }

        if (EnemyLeft())
        {
            LoadDeath();
        }

        if (IsGrounded())
        {
            print("grounded");
        }

        if (isJumping == true)
        {
            anim.SetBool("Jump", true);
            if (IsGrounded() && rb.velocity.y < 1)
            {
                isJumping = false;
            }
        }
    }

    private void MovementPlayer()
    {
        if (moveLeft)
        {
            horizontalMove = -speed;
            anim.SetBool("Run", true);
            sr.flipX = true;
        }

        else if (moveRight)
        {
            horizontalMove = speed;
            anim.SetBool("Run", true);
            sr.flipX = false;
        }

        else if (jump && IsGrounded())
        {
            JumpInAir();
        }

        else
        {
            horizontalMove = 0;
        }
    }

    public void JumpInAir()
    {
        rb.velocity = new Vector2(0, 8);
        jump = false;
        isJumping = true;
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontalMove, rb.velocity.y);
    }

    bool IsGrounded()
    {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        float distance = 0.3f;

        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
        if (hit.collider != null)
        {
            return true;
        }

        return false;
    }

    bool EnemyRight()
    {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.right;
        float distance = 0.4f;

        Debug.DrawRay(position, direction,Color.green);
        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, enemyLayer);
        if (hit.collider != null)
        {
            return true;
        }

        return false;
    }

    bool EnemyLeft()
    {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.left;
        float distance = 0.4f;

        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, enemyLayer);
        if (hit.collider != null)
        {
            return true;
        }

        return false;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Win")
        {
            LoadWin();
        }

        if (collision.gameObject.tag == "Death")
        {
            LoadDeath();
        }
    }

    void LoadWin()
    {
        SceneManager.LoadScene("End Menu");
    }

    void LoadDeath()
    {
        SceneManager.LoadScene("Dead Menu");
    }
}
