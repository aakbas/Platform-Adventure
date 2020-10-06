using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    Rigidbody2D myRigidbody;
    [SerializeField] float moveSpeed=1f;
    BoxCollider2D myBoxCollider;
    


    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myBoxCollider = GetComponent<BoxCollider2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        
    }

    private void Move()
    {
        if(myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Path Checkpoint")))
        {
            moveSpeed *= -1;
            transform.localScale =new Vector2(transform.localScale.x*(-1),1f);
        }
        myRigidbody.velocity = new Vector2(-moveSpeed, 0f);
        
    }

 
}
