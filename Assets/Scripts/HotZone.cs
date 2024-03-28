using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotZone : MonoBehaviour
{
    Enemy enemy;
    private bool inRange;
    Animator orcAnim;

    private void Awake()
    {
        enemy = GetComponentInParent<Enemy>();
        orcAnim = GetComponentInParent<Animator>();
    }

    void Update()
    {
        if(inRange && !orcAnim.GetCurrentAnimatorStateInfo(0).IsName("Orc_Simple_Attack"))
        {
            enemy.Flip();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = true;
            orcAnim.SetTrigger("IsAttentionning");
        }
    }
    private void OnTriggerExit2D(Collider2D check)
    {
        if (check.gameObject.CompareTag("Player"))
        {
            inRange = false;
            gameObject.SetActive(false);
            enemy.detector.SetActive(true);
            enemy.isAttacking = false;
            enemy.doUSeeAnEnemy = false;
            enemy.SelectTheTarget();    
        }
    }
}
