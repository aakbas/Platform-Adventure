using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    float shootTowards;
    [SerializeField] float projectileSpeed=3f;
    DamageDealer myDamage;
    // Start is called before the first frame update
    void Start()
    {
        shootTowards = FindObjectOfType<Player>().shootTowards();
        myDamage = FindObjectOfType<DamageDealer>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (shootTowards > 0)
        {
            transform.Translate(Vector2.right * Time.deltaTime * projectileSpeed);
        }
        else
        {
            transform.Translate(Vector2.left * Time.deltaTime * projectileSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<EnemyMovement>())
        {
            collision.GetComponent<Health>().DealDamage(myDamage.GetDamage());
        }


        Destroy(gameObject);
        
    }

}
