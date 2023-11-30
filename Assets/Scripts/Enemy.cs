using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    public GameObject Player;
    bool active;

    public LayerMask playerLayer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        active = false;
    }

    void Update()
    {
        anim.SetBool("Walk", false);

        if (transform.position.x - 7 <= Player.transform.position.x)
        {
            active = true;
        }

        if (active)
        {
            rb.velocity = new Vector2(-1, rb.velocity.y);
            anim.SetBool("Walk", true);
        }

        if (Hit())
        {
            Destroy(gameObject);
        }
    }

    bool Hit()
    {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.up;
        float distance = 0.2f;

        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, playerLayer);
        if (hit.collider != null)
        {
            return true;
        }

        return false;
    }

    void OnCollisionStay2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Death")
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Border")
        {
            Destroy(gameObject);
        }
    }
}
