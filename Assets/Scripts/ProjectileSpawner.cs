using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    //Config params
  
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] GameObject gun;
    [SerializeField] float spawnRate;

    //states
    bool spawn = true;


    // Start is called before the first frame update
    IEnumerator Start()
    {
        while (spawn)
        {
            yield return new WaitForSeconds(spawnRate);
            SpawnProjectile();
        }
       
    }

    private void SpawnProjectile()
    {
        GameObject newProjectile = Instantiate(projectilePrefab, gun.transform.position, Quaternion.identity);
        newProjectile.transform.parent = transform;
    }

   public void StopSpawning()
    {
        spawn = false;
    }
}
