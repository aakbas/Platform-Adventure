﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    [Header("HUD")]
    [SerializeField] GameObject[] hudAbilityCounter;
    [Header("SFX")]
    [SerializeField] AudioClip jumpSFX;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] AudioClip dashSFX;
    float SFXVolume;
   







    //State
    [Header("State")]
    [SerializeField] bool isAlive = true;
    [SerializeField] bool isDashing = false;
    [SerializeField] bool isUpwardsDashing = false;

    //Cached

    Rigidbody2D myRigidbody;
    Animator myAnimator;
    Feet myFeet;
    BoxCollider2D myBoxCollider;
    LevelLoader levelLoader;
    [SerializeField] Joystick joystick;

    // Start is called before the first frame update
    void Start()
    {
        SFXVolume = GameData.GetSFXVolume();
        globalAbilityCounter = GameData.GetAbilitiyPower();
        abilityCounter = globalAbilityCounter;
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBoxCollider = GetComponent<BoxCollider2D>();
        myFeet = FindObjectOfType<Feet>();
        levelLoader = FindObjectOfType<LevelLoader>();
        joystick = FindObjectOfType<Joystick>();
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
            }
            else
            {                
                myRigidbody.velocity = Vector3.ClampMagnitude(myRigidbody.velocity, 15);
            }
            FlipSprite();
            JumpAnimationChange();
        }
        /*
        if (isAlive)
        {
            if (!isDashing)
            {
                Run();
                AirMove();
                Jump();           
            }
            else
            {
               
            }
            if (abilityCounter>0)
            {
                Dash();
                UpwardsDash();
            }         
           
        }       */
        hudControl();
    }



    //Movement--->
    private void Run()
    {

        if (myFeet.IsTouchingGround())
        {
            Moving(runSpeed);
        }
        else
        {
            AirMove();
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

    public void Jump()
    {
        if (myFeet.IsTouchingGround())
        {
            if (myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
            {
                
                    Vector2 jumpVelocityToAdd = new Vector2(myRigidbody.velocity.x, jumpSpeed);
                    myRigidbody.velocity += jumpVelocityToAdd;
                    AudioSource.PlayClipAtPoint(jumpSFX, transform.position, SFXVolume);
                
            }
        }
    }

    private void Moving(float speed)
    {
        float controlThrow = joystick.Horizontal;
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
                
                    isUpwardsDashing = true;
                    Vector2 jumpVelocityToAdd = new Vector2(UpwardsDashMovingSpeed, upwardsDashSpeed);
                    myRigidbody.velocity = jumpVelocityToAdd;                  
                    myAnimator.SetBool("UpwardsDash", true);
                    upwardsDashCounter = 0;
                    abilityCounter--;
                    AudioSource.PlayClipAtPoint(dashSFX, transform.position, SFXVolume);
                    FindObjectOfType<AnalyticsTracker>().SkillEventTrigger();
                
            }            
        }                
    }

    private void Dash()
    {
               

            isDashing = true;
            myAnimator.SetBool("Dash",true);            
            StartCoroutine(StopDash());
            myRigidbody.velocity = new Vector2(dashSpeed * transform.localScale.x, myRigidbody.velocity.y);
            AudioSource.PlayClipAtPoint(dashSFX, transform.position, SFXVolume);
            abilityCounter--;
            FindObjectOfType<AnalyticsTracker>().SkillEventTrigger();
            
        
    }

    private void RestoreAbilityPower(int restoreAmount)
    {
        abilityCounter = restoreAmount;
    }

    private void hudControl()
    {

        for (int i = 0; i < hudAbilityCounter.Length; i++)
        {
            if (abilityCounter>i)
            {
                hudAbilityCounter[i].SetActive(true);
              
            }
            else
            {
                hudAbilityCounter[i].SetActive(false);
              
            }

        }


    }
     
    //Mobile Abilities--->

    public void MobileJump()
    {
        if (isAlive)
        {
            if (!isDashing)
            {
                Jump();
            }
        }
    }

    public void MobileUpwardsDash()
    {
        if (isAlive)
        {
            if (abilityCounter>0)
            {
                UpwardsDash();
            }
        }
    }

    public void MobileDash()
    {
        if (isAlive)
        {
            if (abilityCounter>0)
            {
                Dash();
            }
        }
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
        bool playerHasVerticalSpeed = Mathf.Abs(myRigidbody.velocity.y) >1f;
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

    //Death---->

    public void ChangeIsAlive(bool newState)
    {
        isAlive = newState;
        if (!newState)
        {
            Death();
        }
    }

    private void Death()
    {
        myRigidbody.velocity = new Vector2(0f, 0f);
        myAnimator.SetBool("Running", false);
        myAnimator.SetBool("Jump", false);
        myAnimator.SetBool("Fall", false);
        myAnimator.SetBool("UpwardsDash", false);
        myAnimator.SetBool("Dash", false);
        myAnimator.SetBool("Death",true );
        FindObjectOfType<DeathCounter>().AddDeathCount();
        FindObjectOfType<AnalyticsTracker>().DeathEventTrigger();
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, SFXVolume-20);
        StartCoroutine(RestartAfterDeath());
        
    }

    private void StopDeathAnimation()
    {
        myAnimator.SetBool("Death", false);
    }

    private IEnumerator RestartAfterDeath()
    {
        yield return new WaitForSeconds(1);
        levelLoader.RestartScene();
    }





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
