using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCscript : MonoBehaviour
{
    public bool follow;


    public  Transform target;
    // Start is called before the first frame update
    void Start()
    {
        follow = false;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if(follow == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, 0.3f);
        }
    }
}
