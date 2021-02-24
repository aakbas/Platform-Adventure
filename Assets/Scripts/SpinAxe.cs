using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAxe : MonoBehaviour
{

    [SerializeField] float spinSpeed = 50f;

    TravelerMovement myTraveler;

    // Start is called before the first frame update
    void Start()
    {
        myTraveler = FindObjectOfType<TravelerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, spinSpeed * Time.deltaTime); //rotates 50 degrees per second around z axis
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<TravelerMovement>())
        {
            myTraveler.ChangeIsAlive(false);
        }
    }



}
