using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireTheFireball : MonoBehaviour
{
    [Header("Damage")]
    [SerializeField] int damage;

    [SerializeField] GameObject partical;
    [SerializeField] float velocity;
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * velocity; 
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag != "AI")
        {
            Vector2 contact = gameObject.transform.position;
            GameObject fireVFX = Instantiate(partical, contact, Quaternion.identity);
            Destroy(gameObject);
            Destroy(fireVFX, 0.2f);
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }
}
