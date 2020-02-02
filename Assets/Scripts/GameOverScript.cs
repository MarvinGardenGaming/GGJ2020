using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour
{

    public Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        SetScore();
    }

    void SetScore()
    {
        if(GameObject.FindGameObjectWithTag("Ship") != null)
        {
            GameObject rocketShip = GameObject.FindGameObjectWithTag("Ship");

            var score = rocketShip.transform.GetComponent<RocketScript>().scoreInt;

            print("Score is: " + score);

            scoreText.text = score.ToString();

            Destroy(rocketShip);
        }
    }
}
