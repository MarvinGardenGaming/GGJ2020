using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject bulletPrefab;

    public Animator animator;

    public Rigidbody2D rb;

    public float moveSpeed;

    private Vector2 moveVector;

    private float lastFacingDirectionFloat;
    // Up = 1
    // UpRight = 2
    // Right = 3
    // DownRight = 4
    // Down = 5
    // DownLeft = 6
    // Left = 7
    // UpLeft = 8

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        HandleFire();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    void HandleInput()
    {
        moveVector.x = Input.GetAxis("Horizontal");
        moveVector.y = Input.GetAxis("Vertical");
        animator.SetFloat("Horizontal", moveVector.x);
        animator.SetFloat("Vertical", moveVector.y);
        animator.SetFloat("Speed", moveVector.sqrMagnitude);
        if(moveVector.y > 0.0 && moveVector.x == 0.0)
        {
            //Up
            lastFacingDirectionFloat = 1f;
        } else if(moveVector.y > 0.0 && moveVector.x > 0.0)
        {
            //Up Right
            lastFacingDirectionFloat = 2f;
        }
        else if (moveVector.y == 0.0 && moveVector.x > 0.0)
        {
            //Right
            lastFacingDirectionFloat = 3f;
        }
        else if (moveVector.y < 0.0 && moveVector.x > 0.0)
        {
            //DownRight
            lastFacingDirectionFloat = 4f;
        }
        else if (moveVector.y < 0.0 && moveVector.x == 0.0)
        {
            //Down
            lastFacingDirectionFloat = 5f;
        }
        else if (moveVector.y < 0.0 && moveVector.x < 0.0)
        {
            //DownLeft
            lastFacingDirectionFloat = 6f;
        }
        else if (moveVector.y == 0.0 && moveVector.x < 0.0)
        {
            //Left
            lastFacingDirectionFloat = 7f;
        }
        else if (moveVector.y > 0.0 && moveVector.x < 0.0)
        {
            //UpLeft
            lastFacingDirectionFloat = 8f;
        }
        animator.SetFloat("LastFacingDirection", lastFacingDirectionFloat);
    }

    void HandleFire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = moveVector;
        }
    }

    void HandleMovement()
    {
        rb.velocity = moveVector * moveSpeed * Time.fixedDeltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("trigger entered and is" + collision);

        if (collision.gameObject.tag == "Enemy")
        {
            print("triggered enemy");
        }
    }
}
