using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    //Config params
    [SerializeField] float platformSpeed;


    //Cached
    Rigidbody2D myRigidbody2D;


    //States
   [SerializeField]  bool isMovingVertical=true;
   

    private void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void Update()
    {
        if (isMovingVertical)
        {
            MoveVerticaly();
        }
        else
        {
            MoveHorizontaly();
        }
    }

    // Movement direction methods
    private void MoveVerticaly()
    {
        Movement(myRigidbody2D.velocity.x, platformSpeed);       
    }

    private void MoveHorizontaly()
    {
        Movement(platformSpeed, myRigidbody2D.velocity.y);
    }

    //Change direction when hit to boundary
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<TrapCheckpoint>())
        {
            platformSpeed *= -1;
        }
    }

    //Main movement method
    private  void Movement(float x ,float y)
    {
        Vector2 platformVelocity = new Vector2(x, y);
        myRigidbody2D.velocity = platformVelocity;
    }




}
