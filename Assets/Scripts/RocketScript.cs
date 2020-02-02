using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RocketScript : MonoBehaviour
{
    public bool launchRocket;

    public Text scoreText;

    public int scoreInt;

    public Transform repair;

    public float repairValue;

    public Animator animator;

    public GameObject npcPrefab;

    public GameObject player;

    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        launchRocket = false;
        scoreInt = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(launchRocket == true)
        {
            Launch();
        }

        if (rb.transform.position.y < 0)
        {
            rb.velocity = new Vector3(0, 0, 0);
            rb.MovePosition(new Vector2(0, 0));
        }

        GameObject repairBar = GameObject.FindGameObjectWithTag("RepairBar");
        var repairValue = repairBar.transform.localScale.x;

        animator.SetFloat("RepairValue", repairValue);
    }

    public void Launch()
    {
        scoreInt = scoreInt + 1;
        scoreText.text = scoreInt.ToString();
        rb.AddForce(new Vector2(0, 500));
        launchRocket = false;
        Invoke("Return", 4f);
        SpawnNPCs();
    }

    public void Return()
    {
        rb.MovePosition(new Vector2(0, 20));
        rb.velocity = new Vector3(0, -20, 0);

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.GetComponent<PlayerController>().repairValue = 0f;
    }

    void SpawnNPCs()
    {

        var deltaX = 0f;
        var deltaY = 0f;

        var randomX = 0f;
        var randomY = 0f;
        // Keep looping until it generates a number larger than 10 so that
        // NPCS spawn off screen
        while (deltaX < 8 || deltaY < 8)
        {
            randomX = UnityEngine.Random.Range(-50, 50);
            randomY = UnityEngine.Random.Range(-50, 50);

            //Code to make enemy spawn off screen;

            deltaX = randomX - player.transform.position.x;
            if (deltaX < 0)
            {
                deltaX = deltaX * -1;
            }

            deltaY = randomY - player.transform.position.y;
            if (deltaY < 0)
            {
                deltaY = deltaY * -1;
            }
        }

        var npcPosition = new Vector3(randomX, randomY, -1);
        GameObject npc = Instantiate(npcPrefab, npcPosition, Quaternion.identity);
        npc.gameObject.tag = "NPC";
    }
}
