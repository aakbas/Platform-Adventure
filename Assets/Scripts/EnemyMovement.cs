using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    //Config Params
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] Player player;
    [SerializeField] float chaseCooldown = 1f;
    [SerializeField] float chaseStopTime = 1f;

    //States
  [SerializeField]   bool isChasingPlayer = false;
  [SerializeField]  bool isReachedBorder = false;

    //Cached Ref
    Rigidbody2D myRigidbody;
    CircleCollider2D myCircleCollider;
    BoxCollider2D myBoxCollider;
    


    
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myCircleCollider = GetComponent<CircleCollider2D>();
        myBoxCollider = GetComponent<BoxCollider2D>();
        
    }

 
    void Update()
    {
       
        LocatePositions();
        FlipSprite();
        Move();
        
    }

    private void Move()
    {
        var moveTowards =transform.localScale.x;

        if (!isReachedBorder && !isChasingPlayer)
        {
            if (moveTowards > 0)
            {
                myRigidbody.velocity = new Vector2(-moveSpeed,0f);
            }
            else
            {
                myRigidbody.velocity = new Vector2(moveSpeed, 0f);
            }        
        }

        if(!isReachedBorder && isChasingPlayer)
        {
            if (LocatePositions()<0)
            {
                myRigidbody.velocity = new Vector2(-moveSpeed, 0f);
            }
            else
            {
                myRigidbody.velocity = new Vector2(moveSpeed, 0f);
            }
        }   



    }



    

    private float LocatePositions()
    {
        var playerPosX = player.transform.position.x;
        var enemyPosX = gameObject.transform.position.x;
        var deltaX = playerPosX - enemyPosX;
        return deltaX;
    }  

    private void FlipSprite()
    {
        //if player is moving horizontaly  reverse the scaling of x axis
        bool enemyHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        if (enemyHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(-(myRigidbody.velocity.x)), 1f);
        }

    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            isChasingPlayer = true;
        }
       
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            isChasingPlayer = false;
        }
    }




}
