using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    DamageDealer mydamage;


    private void Start()
    {
        mydamage = FindObjectOfType<DamageDealer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<EnemyMovement>())
        {
            collision.GetComponent<Health>().DealDamage(mydamage.GetDamage());
        }

    }

}
