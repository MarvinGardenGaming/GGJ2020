using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerScript : MonoBehaviour
{

    public GameObject enemyPrefab;

    public int initialEnemies;


    // Start is called before the first frame update
    public void Start()
    {
        var i = 1;
        while(i <= initialEnemies)
        {
            i += 1;
            SpawnEnemies();
        }

        InvokeRepeating("SpawnEnemies", 2f, 2f);
    }
    public void EndGame()
    {
        SceneManager.LoadScene(sceneName: "GameOver");
    }

    void SpawnEnemies()
    {
        var randomX = UnityEngine.Random.RandomRange(-8, 9);
        var randomY = UnityEngine.Random.RandomRange(-5, 5);
        var enemyPosition = new Vector3(randomX, randomY, -1);
        GameObject bullet = Instantiate(enemyPrefab, enemyPosition, Quaternion.identity);
    }
}
