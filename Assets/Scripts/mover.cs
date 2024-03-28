using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mover : MonoBehaviour
{
    [Header("GameStats")]
    [SerializeField] float jumpForce;
    public float health;
    public float xVelocity;
    bool isFacingRight = true;
    CircleCollider2D myBody;
    Animator animator;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        myBody = GetComponent<CircleCollider2D>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Jumping();
        Walking();
        FlipSprite();
        Death();
    }

    private void FlipSprite()
    {
        bool isMoving = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        if (isMoving)
        {
            if (Input.GetAxis("Horizontal") > 0 && !isFacingRight)
            {
                Flipper();
            }
            else if (Input.GetAxis("Horizontal")< 0 && isFacingRight)
            {
                Flipper();
            }
        }
        else return;
    }

    private void Walking()
    {
        float xMove = Input.GetAxis("Horizontal") * Time.deltaTime;
        Vector2 movement = new Vector2(xMove * xVelocity, rb.velocity.y);
        rb.velocity = movement;
        if (Mathf.Abs(rb.velocity.x) > 0.1f && myBody.IsTouchingLayers(LayerMask.GetMask("Walkable")))
        {
            animator.SetBool("IsWalking", true);
        }
        else animator.SetBool("IsWalking", false);
    }

    private void Jumping()
    {
        if (Input.GetButton("Jump") && myBody.IsTouchingLayers(LayerMask.GetMask("Walkable")))
            {
                animator.SetBool("IsJumping", true);
                Vector2 jumpV = new Vector2(0, jumpForce);
                rb.velocity += jumpV;
            }
            else
            {
            animator.SetBool("IsJumping", false);
            }
    }
    private void Flipper()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0, 180, 0);
    }
    public void ReduceSpeed(float newVelocity)
    {
        xVelocity = newVelocity;
    }
    public void MakeItLikeBefore(float previousVelocity)
    {
        xVelocity = previousVelocity;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }
    private void Death()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
