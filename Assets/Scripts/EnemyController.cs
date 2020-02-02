using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D rb;

    public float EnemyHealth;

    private Transform target;

    public float moveSpeed;

    public Animator animator;

    private float startingHealth;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        startingHealth = EnemyHealth;
    }

    private void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            EnemyHealth = EnemyHealth - 10f;
            var colorValue = EnemyHealth / startingHealth;
            gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.red, Color.blue, colorValue);
            if(EnemyHealth <= 0f)
            {
                Destroy(gameObject);
            }
        }
    }

    private void Update()
    {
        animator.SetFloat("Enemy1Horizontal", rb.velocity.x);
        animator.SetFloat("Enemy1Vertical", rb.velocity.y);
    }
}
