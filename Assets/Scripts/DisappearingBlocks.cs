using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearingBlocks : MonoBehaviour
{
  
    // Config paramaeters
 
    [SerializeField] float dissappearTime = 1f;
    [SerializeField] bool isTimed = false;
    CapsuleCollider2D myCapsuleCollider;

    private void Start()
    {
        myCapsuleCollider = GetComponent<CapsuleCollider2D>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<TravelerMovement>())
        {
            if (isTimed)
            {
                Destroy(gameObject, dissappearTime);
            }
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<TravelerMovement>())
        {

            if (!isTimed)
            {
                Destroy(gameObject);
            }
        }
    }

}
