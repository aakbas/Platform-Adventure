using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feet : MonoBehaviour
{

    Collider2D myCollider;

    private void Start()
    {
        myCollider = GetComponent<Collider2D>();
    }

    public bool IsTouchingGround()
    {

        if (myCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return true;
        }
        else
        {
            return false;
        }

        
    }


}
