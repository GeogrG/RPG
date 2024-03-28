using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAreaChect : MonoBehaviour
{
    Enemy enemy;

    private void Awake()
    {
        enemy = GetComponentInParent<Enemy>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            enemy.target = collision.transform;
            enemy.doUSeeAnEnemy = true;
            enemy.hotZone.SetActive(true);
        }
    }
}
