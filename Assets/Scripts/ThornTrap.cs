using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornTrap : MonoBehaviour
{

    TravelerMovement myTraveler;

    private void Start()
    {
        myTraveler = FindObjectOfType<TravelerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<TravelerMovement>())
        {
            myTraveler.ChangeIsAlive(false);
        }
    }

    




}
