using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : MonoBehaviour
{

    [SerializeField] GameObject trapHitbox;
    [SerializeField] GameObject trapLights;
    TravelerMovement myTraveler;

    private void Start()
    {
        myTraveler = FindObjectOfType<TravelerMovement>();
    }


    public void ActivateTrap()
    {
        trapHitbox.gameObject.SetActive(true);
        trapLights.SetActive(true);
    }
    public void DeactivateTrap()
    {
        trapHitbox.gameObject.SetActive(false);
        trapLights.SetActive(false);
    }




    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<TravelerMovement>())
        {
            myTraveler.ChangeIsAlive(false);
        }
    }

}
