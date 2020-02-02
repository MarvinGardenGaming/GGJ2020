using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketScript : MonoBehaviour
{
    public bool launchRocket;

    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        launchRocket = false;
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
    }

    public void Launch()
    {
        rb.AddForce(new Vector2(0, 500));
        launchRocket = false;
        Invoke("Return", 4f);
    }

    public void Return()
    {
        print("RETURNED");
        rb.MovePosition(new Vector2(0, 20));
        rb.velocity = new Vector3(0, -20, 0);
    }
}
