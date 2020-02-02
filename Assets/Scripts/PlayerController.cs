using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

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

    public float repairBarValue;

    public AudioSource laserSound;

    public AudioSource enemySound;

    public AudioSource npcSound0;

    public AudioSource npcSound1;

    public AudioSource npcSound2;

    public AudioSource npcSound3;

    public AudioSource npcSound4;

    public AudioSource npcSound5;

    public AudioSource npcSound6;

    public AudioSource npcSound7;

    public AudioSource npcSound8;

    public AudioSource npcSound9;

    public AudioSource npcSound10;

    public AudioSource npcSound11;

    public AudioSource npcSound12;

    public AudioSource npcSound13;

    public AudioSource repairSound1;

    public AudioSource repairSound2;

    private Vector3 v3Pos;

    private float fAngle;

    private float nextEnemySoundTime;

    private float nextNPCSoundTime;

    private float nextRepairSoundTime;

    private int currentNPCSound;


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

        currentNPCSound = 1;

        nextEnemySoundTime = Time.time;

        nextNPCSoundTime = Time.time;

        nextRepairSoundTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        HandleFire();
        LowerRepairBarOverTime();
        repairBarValue = repairValue / 100f;
    }

    private void FixedUpdate()
    {
        HandleMovement();

        if (carryingNPC == true && Time.time >= nextNPCSoundTime)
        {
            if(currentNPCSound == 1)
            {
                npcSound0.Play();
                currentNPCSound += 1;
            }
            else if (currentNPCSound == 2)
            {
                npcSound1.Play();
                currentNPCSound += 1;
            }
            else if (currentNPCSound == 3)
            {
                npcSound2.Play();
                currentNPCSound += 1;
            }
            else if (currentNPCSound == 4)
            {
                npcSound3.Play();
                currentNPCSound += 1;
            }
            else if (currentNPCSound == 5)
            {
                npcSound4.Play();
                currentNPCSound += 1;
            }
            else if (currentNPCSound == 6)
            {
                npcSound5.Play();
                currentNPCSound += 1;
            }
            else if (currentNPCSound == 7)
            {
                npcSound6.Play();
                currentNPCSound += 1;
            }
            else if (currentNPCSound == 8)
            {
                npcSound7.Play();
                currentNPCSound += 1;
            }
            else if (currentNPCSound == 9)
            {
                npcSound8.Play();
                currentNPCSound += 1;
            }
            else if (currentNPCSound == 10)
            {
                npcSound9.Play();
                currentNPCSound += 1;
            }
            else if (currentNPCSound == 11)
            {
                npcSound10.Play();
                currentNPCSound += 1;
            }
            else if (currentNPCSound == 12)
            {
                npcSound11.Play();
                currentNPCSound += 1;
            }
            else if (currentNPCSound == 13)
            {
                npcSound12.Play();
                currentNPCSound = 1;
            }


            nextNPCSoundTime = Time.time + 2f;
        }

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

        if (rb.velocity == new Vector2(0,0))
        {
            v3Pos = Camera.main.WorldToScreenPoint(gameObject.transform.position);

            v3Pos = Input.mousePosition - v3Pos;

            fAngle = Mathf.Atan2(v3Pos.y, v3Pos.x) * Mathf.Rad2Deg;
            if (fAngle < 0.0f)
            {
                fAngle += 360.0f;
            }

            if(fAngle > 67.5f && fAngle < 112.5f)
            {
                //Up
                lastFacingDirectionFloat = 1f;
            } else if (fAngle > 22.5 && fAngle < 67.5)
            {
                //Up Right
                lastFacingDirectionFloat = 2f;
            }
            else if (fAngle > 337.5f || fAngle < 22.5f)
            {
                //Right
                lastFacingDirectionFloat = 3f;
            }
            else if (fAngle > 292.5f && fAngle < 337.0f)
            {
                //DownRight
                lastFacingDirectionFloat = 4f;
            }
            else if (fAngle > 247.5f && fAngle < 292.5f)
            {
                //Down
                lastFacingDirectionFloat = 5f;
            }
            else if (fAngle > 202.5f && fAngle < 247.5f)
            {
                //DownLeft
                lastFacingDirectionFloat = 6f;
            }
            else if (fAngle > 157.5f && fAngle < 202.5f)
            {
                //Left
                lastFacingDirectionFloat = 7f;
            }
            else if (fAngle > 112.5f && fAngle < 157.5f)
            {
                //UpLeft
                lastFacingDirectionFloat = 8f;
            }

        }
        animator.SetFloat("LastFacingDirection", lastFacingDirectionFloat);
    }

    void HandleFire()
    {
        if (Input.GetButtonDown("Fire1") && rb.velocity != new Vector2(0, 0))
        {
            laserSound.Play();
            var rotation = 0;
            var bulletMovement = new Vector2(0, 0);
            var bulletTransform = new Vector3(0, 0, 0);
            if(lastFacingDirectionFloat == 1)
            {
                rotation = 90;
                bulletMovement = new Vector2(0, 1);
                bulletTransform = new Vector3(transform.position.x + 0.1f, transform.position.y, -1);

            }
            else if (lastFacingDirectionFloat == 2)
            {
                rotation = 45;
                bulletMovement = new Vector2(1, 1);
                bulletTransform = new Vector3(transform.position.x + 0.85f, transform.position.y + 0.3f, -1);

            }
            else if (lastFacingDirectionFloat == 3)
            {
                rotation = 0;
                bulletMovement = new Vector2(1, 0);
                bulletTransform = new Vector3(transform.position.x + 1f, transform.position.y - 0.32f, -1);

            }
            else if (lastFacingDirectionFloat == 4)
            {
                rotation = 315;
                bulletMovement = new Vector2(1, -1);
                bulletTransform = new Vector3(transform.position.x + 0.25f, transform.position.y - 0.52f, -1);

            }
            else if (lastFacingDirectionFloat == 5)
            {
                rotation = 270;
                bulletMovement = new Vector2(0, -1);
                bulletTransform = new Vector3(transform.position.x - 0.15f, transform.position.y - 1f, -1);

            }
            else if (lastFacingDirectionFloat == 6)
            {
                rotation = 225;
                bulletMovement = new Vector2(-1, -1);
                bulletTransform = new Vector3(transform.position.x - 0.25f, transform.position.y - 0.52f, -1);

            }
            else if (lastFacingDirectionFloat == 7)
            {
                rotation = 180;
                bulletMovement = new Vector2(-1, 0);
                bulletTransform = new Vector3(transform.position.x - 1f, transform.position.y - 0.32f, -1);

            }
            else if (lastFacingDirectionFloat == 8)
            {
                rotation = 135;
                bulletMovement = new Vector2(-1, 1);
                bulletTransform = new Vector3(transform.position.x - 0.85f, transform.position.y + 0.3f, -1);

            }

            bulletMovement = bulletMovement.normalized;
            GameObject bullet = Instantiate(bulletPrefab, bulletTransform, Quaternion.Euler(0,0,rotation));
            bullet.GetComponent<Rigidbody2D>().velocity = bulletMovement * 15f;
            bullet.gameObject.tag = "Bullet";
            Destroy(bullet, 2.0f);
        }

        if (Input.GetButtonDown("Fire1") && rb.velocity == new Vector2(0, 0))
        {
            laserSound.Play();
            var rotation = fAngle;
            var rotationRadians = rotation * Mathf.Deg2Rad;
            var bulletMovement = new Vector2(Mathf.Cos(rotationRadians), Mathf.Sin(rotationRadians));
            var bulletTransform = new Vector3(0, 0, 0);
            if (lastFacingDirectionFloat == 1)
            {
                bulletTransform = new Vector3(transform.position.x + 0.1f, transform.position.y, -1);

            }
            else if (lastFacingDirectionFloat == 2)
            {
                bulletTransform = new Vector3(transform.position.x + 0.85f, transform.position.y + 0.3f, -1);

            }
            else if (lastFacingDirectionFloat == 3)
            {
                bulletTransform = new Vector3(transform.position.x + 1f, transform.position.y - 0.32f, -1);

            }
            else if (lastFacingDirectionFloat == 4)
            {
                bulletTransform = new Vector3(transform.position.x + 0.25f, transform.position.y - 0.52f, -1);

            }
            else if (lastFacingDirectionFloat == 5)
            {
                bulletTransform = new Vector3(transform.position.x - 0.15f, transform.position.y - 1f, -1);

            }
            else if (lastFacingDirectionFloat == 6)
            {
                bulletTransform = new Vector3(transform.position.x - 0.25f, transform.position.y - 0.52f, -1);

            }
            else if (lastFacingDirectionFloat == 7)
            {
                bulletTransform = new Vector3(transform.position.x - 1f, transform.position.y - 0.32f, -1);

            }
            else if (lastFacingDirectionFloat == 8)
            {
                bulletTransform = new Vector3(transform.position.x - 0.85f, transform.position.y + 0.3f, -1);

            }
            bulletMovement = bulletMovement.normalized;
            GameObject bullet = Instantiate(bulletPrefab, bulletTransform, Quaternion.Euler(0, 0, rotation));
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
        repairBarValue = repairValue / 100f;


        GameObject repairBar = GameObject.FindGameObjectWithTag("RepairBar");
        repairBar.transform.localScale = new Vector3(repairBarValue, 1f);
    }

    void HandleMovement()
    {
        rb.velocity = moveVector * moveSpeed * Time.fixedDeltaTime;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "NPC")
        {
            print("collided NPC");
            if (savedNPC == true)
            {
                savedNPC = false;
                Destroy(collision.gameObject);

                print("destroyed NPC");
            }
        }

        if (collision.gameObject.tag == "Ship" && carryingNPC == true)
        {
            var repairSpeed = 1f;
            repairValue = repairValue + repairSpeed;
            repairBarValue = repairValue / 100f;
            var repairSound = 1;

            if (Time.time >= nextRepairSoundTime)
            {
                if(repairSound == 1)
                {
                    repairSound1.Play();
                    repairSound = 2;
                }
                else if (repairSound == 2)
                {
                    repairSound2.Play();
                    repairSound = 1;    
                }

                nextRepairSoundTime = Time.time + 0.4f;
            }

            if (repairBarValue >= 1f)
            {
                repairValue = 100f;
                repairBarValue = 1f;

                shouldLaunch = true;

                carryingNPC = false;
                print("SAVED");
                savedNPC = true;
                npcSound0.Stop();
                npcSound1.Stop();
                npcSound2.Stop();
                npcSound3.Stop();
                npcSound4.Stop();
                npcSound5.Stop();
                npcSound6.Stop();
                npcSound7.Stop();
                npcSound8.Stop();
                npcSound9.Stop();
                npcSound10.Stop();
                npcSound11.Stop();
                npcSound12.Stop();
                npcSound13.Stop();
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

            if (Time.time >= nextEnemySoundTime)
            {
                enemySound.Play();

                nextEnemySoundTime = Time.time + 1f;
            }
            var damage = 1f;
            healthValue = healthValue - damage;
            var healthBarValue = healthValue / 1000f;

            if(healthBarValue < 0f)
            {
                healthBarValue = 0f;
                gameManager.EndGame();
            }

            GameObject bar = GameObject.FindGameObjectWithTag("PlayerHealth");
            bar.transform.localScale = new Vector3(healthBarValue, 1f);
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "NPC")
        {
            collision.gameObject.transform.GetComponent<NPCscript>().follow = true;
            carryingNPC = true;

        }
    }
}
