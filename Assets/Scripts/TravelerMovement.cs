using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class TravelerMovement : MonoBehaviour
{
    // Configuration Parameters
    [Header("Movement")]
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumSpeed = 5f;
    [SerializeField] float AirSpeed = 5f;
    [SerializeField] float upwardsDashSpeed = 15f;
    float UpwardsDashMovingSpeed = 0;
    bool isUpwardsDashing = false;
    [SerializeField]int upwardsDashCounter = 1;


    //State
    bool isAlive = true;

    //Cached

    Rigidbody2D myRigidbody;
    Animator myAnimator;
    Feet myFeet;
    CapsuleCollider2D myCapsuleCollider;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCapsuleCollider = GetComponent<CapsuleCollider2D>();
        myFeet = FindObjectOfType<Feet>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            Run();
            AirMove();
            Jump();
            UpwardsDash();
            FlipSprite();
            JumpAnimationChange();
        }
    }

    private void Run()
    {
        if (myFeet.IsTouchingGround())
        {
            Moving(runSpeed);
        }
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("Running", playerHasHorizontalSpeed);

    }

    private void AirMove()
    {
        if (!myFeet.IsTouchingGround())
        {
            if (!isUpwardsDashing)
            {
                Moving(AirSpeed);
            }
        }
    }


    private void FlipSprite()
    {
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
            if (myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
            {
                if (CrossPlatformInputManager.GetButtonDown("Jump"))
                {
                    Vector2 jumpVelocityToAdd = new Vector2(myRigidbody.velocity.x, jumSpeed);
                    myRigidbody.velocity += jumpVelocityToAdd;
                }
            }
        }
    }

    private void JumpAnimationChange()
    {
        bool playerHasVerticalSpeed = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;
        var verticalPos = myRigidbody.velocity.y;
        if (playerHasVerticalSpeed)
        {
            if (verticalPos > 0)
            {
                myAnimator.SetBool("Jump", true);
            }
            else
            {
                isUpwardsDashing = false;
                myAnimator.SetBool("Jump", false);
                myAnimator.SetBool("Fall", true);
            }
        }
        else
        {
            isUpwardsDashing = false;
            myAnimator.SetBool("UpwardsDash", false);
            upwardsDashCounter = 1;
        }
        if (myFeet.IsTouchingGround())
        {
            myAnimator.SetBool("Jump", false);
            myAnimator.SetBool("Fall", false);
        }

    }

    private void UpwardsDash()
    {
        if (upwardsDashCounter==1)
        {
            if (!myFeet.IsTouchingGround())
            {
                if (CrossPlatformInputManager.GetButtonDown("UpwardsDash"))
                {
                    isUpwardsDashing = true;
                    Vector2 jumpVelocityToAdd = new Vector2(UpwardsDashMovingSpeed, upwardsDashSpeed);
                    myRigidbody.velocity = jumpVelocityToAdd;                  
                    myAnimator.SetBool("UpwardsDash", true);
                    upwardsDashCounter = 0;
                }
            }
            
        }
        
        {

        }
    }

    private void Moving(float speed)
    {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        Vector2 playerVelocity = new Vector2(controlThrow * speed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;
    }


}
