using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{

    //Config

    [Header("Movement")]

    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float shieldCooldown = 0.3f;

    [Header("Animation")]

    [SerializeField] float deathTime = 1f;
   [SerializeField] float atackTime = 0.5f;
    [SerializeField] float shieldTime = 1f;
    

    [Header("Game Objects")]

    [SerializeField] GameObject shield;


    //State
    bool isAlive = true;
    bool isShieldUp = false;

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
            if (!isShieldUp)
            {
                Run();
                FlipSprite();
                Jump();
                JumpAnimationChange();
                Atack();
            }
            Shield();
        }
        else
        {
            Die();
        }
    }

    private void Die()
    {
         StartCoroutine(Death());   
        myRigidbody.velocity = new Vector2(0f, 0f);       

    }

    private void Run()
    {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("Running", playerHasHorizontalSpeed);
    }

    private void Atack()
    {
        if(CrossPlatformInputManager.GetButtonDown("Fire1"))
        {
            StartCoroutine(Melee());
        }
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

 
     private IEnumerator Death()
    {       
         myRigidbody.velocity = new Vector2(0f, 0f);       
        myAnimator.SetBool("Running", false);
        myAnimator.SetBool("Fall", false);
        myAnimator.SetBool("Hurt", false);
        myAnimator.SetBool("Jump", false);
        myAnimator.SetBool("Atack", false);
        myAnimator.SetBool("Death", true);
        myAnimator.SetBool("Shield", false);

        yield return new WaitForSeconds(deathTime);
        myAnimator.SetBool("GameOver", true);
    }

    private IEnumerator Melee()
    {
        myAnimator.SetBool("Atack", true);
        yield return new WaitForSeconds(atackTime);
        myAnimator.SetBool("Atack", false);
    }

    private IEnumerator Brace()
    {
        myRigidbody.velocity = new Vector2(0f, 0f);
        myAnimator.SetBool("Shield", true);
     
        shield.SetActive(true);
        isShieldUp = true;
        yield return new WaitForSeconds(shieldCooldown);
        myAnimator.SetBool("ShieldCooldown", true);
        yield return new WaitForSeconds(shieldTime);
        myAnimator.SetBool("ShieldCooldown", false);
        myAnimator.SetBool("Shield", false);
        shield.SetActive(false);
        isShieldUp = false;
        
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
                myAnimator.SetBool("Jump", false);
                myAnimator.SetBool("Fall", true);
            }
        }
        if(myFeet.IsTouchingGround())
        {
            myAnimator.SetBool("Jump", false);
            myAnimator.SetBool("Fall", false);
        }
        

    }

    private void Shield()
    {
        bool playerHasVerticalSpeed= Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;
        if (!isShieldUp)
        {
            if (CrossPlatformInputManager.GetButtonDown("Fire2"))
            {
                if (!playerHasVerticalSpeed)
                {
                    StartCoroutine(Brace());
                }
            }
        }


    }
  

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<EnemyMovement>() && gameObject.GetComponent<Player>()){

         //  isAlive = false;
            
        }
    }

} 
