using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootProjectileLogic : MonoBehaviour
{
    //This is the generic script used for characters that use ranged attack.
    public GameObject projectile;
    public GameObject player;
    public float range;
    public float fireRate;
    public int numberOfProjectiles;
    public float angleOfShots;
    public PlayerMovement enemyPos;
    float lastShot;
    float distance;

    IEnumerator FireObject()
    {
        int middleShot = numberOfProjectiles / 2;
        for (int i = 0; i < numberOfProjectiles; i++)
        {
            Shoot((i - middleShot) * angleOfShots, transform, 10f);
        }
        yield return null;
    }
    void Shoot(float direction, Transform location, float fireSpeed)
    {
        GameObject _projectile = Instantiate(projectile);
        _projectile.transform.position = location.position;

    }
}
