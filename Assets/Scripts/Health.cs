using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int healt = 3;

    private void Update()
    {
        Die();
    }

    public int GetHealth()
    {
        return healt;
    }

    public void DealDamage(int damage)
    {
        healt -= damage;
    }

    public void Die()
    {
        if (healt <= 0)
        {
            Destroy(gameObject);
        }
    }

   
}
