using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerScript : MonoBehaviour
{

    public GameObject enemyPrefab;

    public GameObject player;

    public GameObject npcPrefab;

    public int initialEnemies;

    public int initialNPCs;


    // Start is called before the first frame update
    public void Start()
    {
        var a = 1;

        while(a <= initialNPCs)
        {
            a += 1;
            SpawnNPCs();
        }
        var i = 1;
        while(i <= initialEnemies)
        {
            i += 1;
            SpawnEnemies();
        }

        InvokeRepeating("SpawnEnemies", 2f, 3f);
    }
    public void EndGame()
    {
        SceneManager.LoadScene(sceneName: "GameOver");
    }

    void SpawnEnemies()
    {
        var deltaX = 0f;
        var deltaY = 0f;

        var randomX = 0f;
        var randomY = 0f;
        // Keep looping until it generates a number larger than 10 so that
        // Enemies spawn off screen
        while(deltaX < 8|| deltaY < 8)
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

        var enemyPosition = new Vector3(randomX, randomY, -1);
        GameObject enemy = Instantiate(enemyPrefab, enemyPosition, Quaternion.identity);
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
