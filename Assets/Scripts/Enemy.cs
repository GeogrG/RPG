using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Game Stats")]
    [SerializeField] float health;
    [SerializeField] float attackRange;
    [SerializeField] float casulties;
    public float moveSpeed;
    private float timer;
    private float waitTime = 1.5f;
    [HideInInspector]public bool isAttacking;
    private bool isCooling;

    [Header("Where to move?")]
    public Transform leftLimit;
    public Transform rightLimit;
    [HideInInspector]public Transform target;
    [HideInInspector]public bool doUSeeAnEnemy = false;
    public GameObject detector;
    public GameObject hotZone;
    Animator orcAnimator;
    mover mover;
    BoxCollider2D box;
    

    private void Start()
    {
        orcAnimator = GetComponent<Animator>();
        SelectTheTarget();
        isCooling = false;
        box = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        Movement();
        if (!InsideOfLimits() && !doUSeeAnEnemy)
        {
            SelectTheTarget();
        }
        if (doUSeeAnEnemy)
        {
            Logic();
        }
        Cooling();
        InsideOfLimits();
        Flip();
    }

    private void Logic()
      {
        float distance = Vector2.Distance(target.position, transform.position);
        if (distance <= attackRange && !isCooling)
        {
            isAttacking = true;
            orcAnimator.SetBool("IsIdleing", false);
            orcAnimator.SetBool("IsSimplyAttacking", true);
            moveSpeed = 0;
        }
        else if (distance <= attackRange && isCooling)
        {
            isAttacking = true;
            orcAnimator.SetBool("IsSimplyAttacking", false);
            orcAnimator.SetBool("IsIdleing", true);
            moveSpeed = 0;
        }
        else if (distance > attackRange)
        {
            isAttacking = false;
            moveSpeed = 2f;
            orcAnimator.SetBool("IsSimplyAttacking", false);
            orcAnimator.SetBool("IsIdleing", false);
        }
      }

    private bool InsideOfLimits()
    {
        return transform.position.x > leftLimit.position.x && transform.position.x < rightLimit.position.x;
    }

    public void TriggerCooling()
    {
        isCooling = true;
    }

    private void Cooling()
    {
        if(isCooling)
            timer += Time.deltaTime;
            if(timer > waitTime)
            {
            isCooling = false;
            timer = 0f;
            }
    }

    public void SelectTheTarget()
    {
        float distanceToLeft = Vector3.Distance(transform.position, leftLimit.position);
        float distanceToRight = Vector3.Distance(transform.position, rightLimit.position);

        target = distanceToLeft > distanceToRight ? leftLimit : rightLimit;
    }

    public void Flip()
    {
        Vector3 rotation = transform.eulerAngles;
        rotation.y = (transform.position.x < target.position.x)?  180f : 0f;
        transform.eulerAngles = rotation;
    }

    private void Movement()
    {
        if (!orcAnimator.GetCurrentAnimatorStateInfo(0).IsName("Orc_Attention") && !isAttacking)
        {
            orcAnimator.SetBool("IsWalking", true);
            Vector2 targetPos = new Vector2(target.position.x, transform.position.y);
            var movementThisFrame = Time.deltaTime * moveSpeed;
            transform.position = Vector2.MoveTowards(transform.position, targetPos, movementThisFrame);
        }
        else
        {
            orcAnimator.SetBool("IsWalking", false);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Death();
        }
    }
    private void Death()
    {
            Destroy(gameObject);
    }
    private void Attack()
    {
        if (box.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            mover = FindObjectOfType<mover>();
            mover.TakeDamage(casulties);
            if (mover.health <= 0)
            {
                orcAnimator.SetBool("IsSimplyAttacking", false);
                orcAnimator.SetBool("IsIdleing", false);
                isAttacking = false;
                doUSeeAnEnemy = false;
                moveSpeed = 2f;
            }
        }
    }
}
