using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterAttacker : MonoBehaviour
{
    Animator animator;
    [Header("ForShooting")]
    [SerializeField] GameObject fireball;
    [SerializeField] GameObject instantiatePoint;

    [Header("Slowing")]
    [SerializeField] float newVelocity;
    [SerializeField] float previousVelocity;
    [SerializeField] float nextSpeedInceaceRate = 2f;

    [Header("AttackRate")]
    [SerializeField] float nextAttactRate = 4f;

    private float nextAttack = 0f;
    private float nextSpeedIncreace = 0f;
    mover mover;
    void Start()
    {
        animator = GetComponent<Animator>();
        mover = FindObjectOfType<mover>();
    }
    void Update()
    {
        Attacking();
    }

    public void Attacking()
    {
        if (Time.time >= nextAttack)
        {
            mainAttack();
        }
    }

    private void mainAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("IsSimplyAttacking");
            mover.ReduceSpeed(newVelocity);
            nextAttack = Time.time + 1f / nextAttactRate;
        }
        else if (Time.time >= nextSpeedIncreace)
        {
            mover.MakeItLikeBefore(previousVelocity);
            nextSpeedIncreace = Time.time + 1f / nextSpeedInceaceRate;
        }
    }

    void Fire()
    {
        Instantiate(fireball, instantiatePoint.transform.position, instantiatePoint.transform.rotation);
    }
   
}
