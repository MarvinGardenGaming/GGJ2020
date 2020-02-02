using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour
{
    public Rigidbody2D intro;

    // Start is called before the first frame update
    void Start()
    {
        intro.AddForce(new Vector2(0, 2300));
        Invoke("TransitionScene", 30f);
    }

    // Update is called once per frame
    void TransitionScene()
    {
        SceneManager.LoadScene("Sandbox");
    }
}
