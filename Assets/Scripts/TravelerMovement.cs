using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class TravelerMovement : MonoBehaviour
{
    // Configuration Parameters
    [Header("Movement")]
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float AirSpeed = 5f;
    [Header("Ability")]
    [SerializeField] float upwardsDashSpeed = 15f;
    [SerializeField] float dashSpeed = 15;
    [SerializeField] int globalAbilityCounter;
    [SerializeField] int abilityCounter;
    [SerializeField] int upwardsDashCounter = 1;
    float UpwardsDashMovingSpeed = 0;
    
   


    //State
    [Header("State")]
    [SerializeField] bool isAlive = true;
    [SerializeField] bool isDashing = false;
    [SerializeField] bool isUpwardsDashing = false;

    //Cached

    Rigidbody2D myRigidbody;
    Animator myAnimator;
    Feet myFeet;
    CapsuleCollider2D myCapsuleCollider;

    // Start is called before the first frame update
    void Start()
    {    
        globalAbilityCounter = GameData.GetAbilitiyPower();
        abilityCounter = globalAbilityCounter;
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
            if (!isDashing)
            {
                Run();
                AirMove();
                Jump();           
            }
            if (abilityCounter>0)
            {
                Dash();
                UpwardsDash();
            }         
            FlipSprite();
            JumpAnimationChange();
        }       
    }



    //Movement--->
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

    private void Jump()
    {
        if (myFeet.IsTouchingGround())
        {
            if (myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
            {
                if (CrossPlatformInputManager.GetButtonDown("Jump"))
                {
                    Vector2 jumpVelocityToAdd = new Vector2(myRigidbody.velocity.x, jumpSpeed);
                    myRigidbody.velocity += jumpVelocityToAdd;
                }
            }
        }
    }

    private void Moving(float speed)
    {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        Vector2 playerVelocity = new Vector2(controlThrow * speed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;
    }



    //Abilities---->

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
                    abilityCounter--;
                }
            }            
        }                
    }

    private void Dash()
    {

        if (CrossPlatformInputManager.GetButtonDown("Dash"))
        {
            isDashing = true;
            myAnimator.SetBool("Dash",true);            
            StartCoroutine(StopDash());
            myRigidbody.velocity = new Vector2(dashSpeed * transform.localScale.x, myRigidbody.velocity.y);
            abilityCounter--;
        }
    }

    private void RestoreAbilityPower(int restoreAmount)
    {
        abilityCounter = restoreAmount;
    }
     


    // Coroutines----->
     public IEnumerator StopDash()
    {
        yield return new WaitForSeconds(0.3f);
        isDashing = false;
        myAnimator.SetBool("Dash", false);
        myRigidbody.velocity += new Vector2(runSpeed * transform.localScale.x, myRigidbody.velocity.y);

    }



    //Animation Changes ---->

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

        }
        if (myFeet.IsTouchingGround())
        {
            myAnimator.SetBool("Jump", false);
            myAnimator.SetBool("Fall", false);
            upwardsDashCounter = 1;
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








    // Delete After Fix


    //On Trigger Stuff

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<AbilityPointRestore>())
        {
            RestoreAbilityPower(collision.GetComponent<AbilityPointRestore>().RestoreAbilityPower());
            Debug.Log(collision.GetComponent<AbilityPointRestore>().RestoreAbilityPower());
        }
    }




}
