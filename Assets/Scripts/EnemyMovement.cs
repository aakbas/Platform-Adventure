using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    //Config Params
    [SerializeField] float moveSpeed = 1f;

    //States
    bool isChasingPlayer = false;

    //Cached Ref
    Rigidbody2D myRigidbody;
    CircleCollider2D myCircleCollider;
    


    
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myCircleCollider = GetComponent<CircleCollider2D>();
        
    }

 
    void Update()
    {
        if (!isChasingPlayer)
        {
            Move();
        }
        
    }

    private void Move()
    {
        if(myCircleCollider.IsTouchingLayers(LayerMask.GetMask("Path Checkpoint")))
        {
            moveSpeed *= -1;
            transform.localScale =new Vector2(transform.localScale.x*(-1),1f);
        }
        myRigidbody.velocity = new Vector2(-moveSpeed, 0f);
        
    }

 
}
