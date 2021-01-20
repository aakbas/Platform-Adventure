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

    private void MoveVerticaly()
    {
        
           Vector2 platformVelocity = new Vector2(myRigidbody2D.velocity.x, platformSpeed);
        myRigidbody2D.velocity = platformVelocity;
    }

    private void MoveHorizontaly()
    {
        Vector2 playerVelocity = new Vector2(platformSpeed, myRigidbody2D.velocity.y);
        myRigidbody2D.velocity = playerVelocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<TrapCheckpoint>())
        {
            platformSpeed *= -1;
        }
    }

}
