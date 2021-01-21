using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearingBlocks : MonoBehaviour
{

    [SerializeField] float dissappearTime = 1f;
    [SerializeField] bool isTimed = false;
   

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isTimed)
        {
            Destroy(gameObject, dissappearTime);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (!isTimed)
        {
            Destroy(gameObject);
        }
    }

}
