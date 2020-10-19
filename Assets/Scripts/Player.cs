using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{

    //Config
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
     float deathTime = 0.8f;
   [SerializeField] float atackTime = 0.5f;


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
            JumpAnimationChange();
            Atack();
        }
        else
        {
            Die();
        }
    }

    private void Die()
    {

        StartCoroutine(Death());
      


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
            myAnimator.SetTrigger("Death");       
        yield return new WaitForSeconds(deathTime);
        myAnimator.ResetTrigger("Death");
    }

    private IEnumerator Melee()
    {
        myAnimator.SetBool("Atack", true);
        yield return new WaitForSeconds(atackTime);
        myAnimator.SetBool("Atack", false);
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


  

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<EnemyMovement>() && gameObject.GetComponent<Player>()){

           isAlive = false;
            
        }
    }

} 
