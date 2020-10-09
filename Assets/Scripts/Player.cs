﻿using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{

    //Config
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] Projectile playerProjectile;
    [SerializeField] GameObject playerGun;

    //State
    bool isAlive = true;

    //Cached
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    BoxCollider2D myCollider;
    Feet myFeet;
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCollider = GetComponent<BoxCollider2D>();
        myFeet = FindObjectOfType<Feet>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            Run();
            FlipSprite();
            Jump();
            Climb();
            fireProjectile();
            Melee();
        }
        else
        {
            Die();
        }
    }

    private void Die()
    {
        myRigidbody.velocity = new Vector2(0f, 0f);
        myAnimator.SetBool("Death", true);

    }

    private void Run()
    {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("Running", playerHasHorizontalSpeed);
    }

    private void FlipSprite()
    {
        //if player is moving horizontaly  reverse the scaling of x axis
        bool playerHasHorizontalspeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalspeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f);
        }

    }

    private void Jump()
    {
        if (myFeet.IsTouchingGround())
        {
            if (myCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
            {
                if (CrossPlatformInputManager.GetButtonDown("Jump"))
                {
                    Vector2 jumpVelocityToAdd = new Vector2(myRigidbody.velocity.x, jumpSpeed);
                    myRigidbody.velocity += jumpVelocityToAdd;
                }
            }
        }
    }

    private void Climb()
    {
        if (myRigidbody.IsTouchingLayers(LayerMask.GetMask("Climb")))
        {
            float controlThrow = CrossPlatformInputManager.GetAxis("Vertical");
            Vector2 playerVelocity = new Vector2(myRigidbody.velocity.x, controlThrow * climbSpeed);
            myRigidbody.velocity = playerVelocity;
            bool playerHasVerticalSpeed = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;
            myAnimator.SetBool("Climbing", playerHasVerticalSpeed);
            myRigidbody.gravityScale = 0;
        }
        else
        {
            myAnimator.SetBool("Climbing", false);
            myRigidbody.gravityScale = 1;
        }


    }

    private void fireProjectile (){
        if (CrossPlatformInputManager.GetButtonDown("Fire1"))
        {
            myAnimator.SetBool("Projectile", true);
        }
        }

    private void CreateProjectile()
    {
        if (!playerProjectile) { return; }
        Instantiate(playerProjectile, playerGun.transform.position, Quaternion.identity);
         myAnimator.SetBool("Projectile", false);
    }
    public float shootTowards()

    {
        
        return transform.localScale.x;
    }

    private void Melee()
    {
        if (CrossPlatformInputManager.GetButtonDown("Fire2"))
        {
            myAnimator.SetBool("Melee", true);
           
        }
    }
    private void StopMelee()
    {
        myAnimator.SetBool("Melee", false);
    } 

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<EnemyMovement>() && gameObject.GetComponent<Player>()){

           isAlive = false;
            
        }
    }

} 
