using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    TravelerMovement myTraveler;
    Rigidbody2D myRigidbody2D;
    [SerializeField] float speed;
    [SerializeField] bool isMovingVertical = true;
    [SerializeField] bool movingWay = true;

    private void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myTraveler = FindObjectOfType<TravelerMovement>();     
    }

   private void Update()
    {
        if (isMovingVertical)
        {
            if (movingWay)
            {
                MoveProjectile(0, -speed);
                RotateProjectile(90);

            }
            else
            {
                MoveProjectile(0, speed);
                RotateProjectile(270);
            }
        }
        else
        {
            if (movingWay)
            {
                MoveProjectile(speed, 0);
                RotateProjectile(180);
            }
            else
            {
                MoveProjectile(-speed, 0);
                RotateProjectile(0);
            }
        }
    }

    private void RotateProjectile(int rotate)
    {
        var rotationVector = transform.rotation.eulerAngles;
        rotationVector.z = rotate;
        transform.rotation = Quaternion.Euler(rotationVector);
    }





    private void MoveProjectile(float speedX,float speedY)
    {
        Vector2 projectileVelocity = new Vector2(speedX, speedY);
        myRigidbody2D.velocity = projectileVelocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<TravelerMovement>())
        {
            myTraveler.ChangeIsAlive(false);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
