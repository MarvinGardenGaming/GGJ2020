using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Transform health;

    public float healthValue;

    public Transform repair;

    public float repairValue;

    public GameObject bulletPrefab;

    public GameObject rocketShip;

    public ManagerScript gameManager;

    public Animator animator;

    public Rigidbody2D rb;

    public float moveSpeed;

    public bool carryingNPC;

    public bool savedNPC;

    public bool shouldLaunch;

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
        carryingNPC = false;
        savedNPC = false;
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        HandleFire();
        LowerRepairBarOverTime();
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
            var rotation = 0;
            var bulletMovement = new Vector2(0, 0);
            if(lastFacingDirectionFloat == 1)
            {
                rotation = 90;
                bulletMovement = new Vector2(0, 1);
            }
            else if (lastFacingDirectionFloat == 2)
            {
                rotation = 45;
                bulletMovement = new Vector2(1, 1);
            }
            else if (lastFacingDirectionFloat == 3)
            {
                rotation = 0;
                bulletMovement = new Vector2(1, 0);
            }
            else if (lastFacingDirectionFloat == 4)
            {
                rotation = 315;
                bulletMovement = new Vector2(1, -1);
            }
            else if (lastFacingDirectionFloat == 5)
            {
                rotation = 270;
                bulletMovement = new Vector2(0, -1);
            }
            else if (lastFacingDirectionFloat == 6)
            {
                rotation = 225;
                bulletMovement = new Vector2(-1, -1);
            }
            else if (lastFacingDirectionFloat == 7)
            {
                rotation = 180;
                bulletMovement = new Vector2(-1, 0);
            }
            else if (lastFacingDirectionFloat == 8)
            {
                rotation = 135;
                bulletMovement = new Vector2(-1, 1);
            }
            bulletMovement = bulletMovement.normalized;
            var bulletTransform = new Vector3(transform.position.x, transform.position.y, -1);
            GameObject bullet = Instantiate(bulletPrefab, bulletTransform, Quaternion.Euler(0,0,rotation));
            bullet.GetComponent<Rigidbody2D>().velocity = bulletMovement * 15f;
            bullet.gameObject.tag = "Bullet";
            Destroy(bullet, 2.0f);
        }
    }

    void LowerRepairBarOverTime()
    {
        var dwindleSpeed = 0.2f;
        if(repairValue > 0f)
        {
            repairValue = repairValue - dwindleSpeed;

        }
        var repairBarValue = repairValue / 100f;

        GameObject repairBar = GameObject.FindGameObjectWithTag("RepairBar");
        repairBar.transform.localScale = new Vector3(repairBarValue, 1f);
    }

    void HandleMovement()
    {
        rb.velocity = moveVector * moveSpeed * Time.fixedDeltaTime;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ship" && carryingNPC == true)
        {
            print("Touching rocket");
            var repairSpeed = 0.7f;
            repairValue = repairValue + repairSpeed;
            var repairBarValue = repairValue / 100f;

            if (repairBarValue >= 1f)
            {
                repairValue = 0f;
                repairBarValue = 0f;
                print("TAKE OFF");

                shouldLaunch = true;

                carryingNPC = false;
                savedNPC = true;
            }

            GameObject repairBar = GameObject.FindGameObjectWithTag("RepairBar");
            repairBar.transform.localScale = new Vector3(repairBarValue, 1f);

            if(shouldLaunch == true)
            {
                rocketShip.transform.GetComponent<RocketScript>().launchRocket = true;
                shouldLaunch = false;
            }
        }


        if (collision.gameObject.tag == "Enemy")
        {
            var damage = 2f;
            healthValue = healthValue - damage;
            var healthBarValue = healthValue / 100f;

            if(healthBarValue < 0f)
            {
                healthBarValue = 0f;
                gameManager.EndGame();
            }

            GameObject bar = GameObject.FindGameObjectWithTag("PlayerHealth");
            bar.transform.localScale = new Vector3(healthBarValue, 1f);
        }

        if (collision.gameObject.tag == "NPC")
        {
            print("Touching NPC");
            if (savedNPC == true)
            {
                savedNPC = false;
                Destroy(collision.gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "NPC")
        {
            print("FOUND ASTRONAUT");
            collision.gameObject.transform.GetComponent<NPCscript>().follow = true;
            carryingNPC = true;

        }
    }
}
