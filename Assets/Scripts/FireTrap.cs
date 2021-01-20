using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : MonoBehaviour
{

    [SerializeField] GameObject trapHitbox;
    TravelerMovement myTraveler;

    private void Start()
    {
        myTraveler = FindObjectOfType<TravelerMovement>();
    }


    public void ActivateTrap()
    {
        trapHitbox.gameObject.SetActive(true);
    }
    public void DeactivateTrap()
    {
        trapHitbox.gameObject.SetActive(false);
    }




    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<TravelerMovement>())
        {
            myTraveler.ChangeIsAlive(false);
        }
    }

}
