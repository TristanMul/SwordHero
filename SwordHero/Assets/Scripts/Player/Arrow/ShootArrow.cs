using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootArrow : MonoBehaviour
{
    public GameObject projectile;
    public GameObject player;
    public float range;
    public float shotPower;
    public float fireRate;
    public int numberOfProjectiles;
    public float angleOfShots;
    //public PlayerMovement enemyPos;
    float lastShot;
    float distance;

    IEnumerator FireObject()
    {
        int middleShot = numberOfProjectiles / 2;
        for (int i = 0; i < numberOfProjectiles; i++) {
            Shoot((i - middleShot) * angleOfShots);
        }
       /*GameObject _projectile = Instantiate(projectile);
        Physics.IgnoreCollision(_projectile.GetComponent<Collider>(), player.GetComponent<Collider>());
        _projectile.transform.position = player.GetComponent<PlayerMovement>().firingPoint.transform.position;
        Vector3 rot = _projectile.transform.eulerAngles;
        _projectile.transform.rotation = Quaternion.Euler(rot.x, player.GetComponent<PlayerMovement>().firingPoint.transform.eulerAngles.y, rot.z);
        _projectile.GetComponent<Rigidbody>().AddForce(player.GetComponent<PlayerMovement>().firingPoint.forward * shotPower, ForceMode.Impulse);*/
        yield return null;
    }

    private void Shoot(float direction)
    {
        GameObject _projectile = Instantiate(projectile);
        Physics.IgnoreCollision(_projectile.GetComponent<Collider>(), player.GetComponent<Collider>());
        _projectile.transform.position = player.GetComponent<PlayerMovement>().firingPoint.transform.position;
        Vector3 rot = _projectile.transform.eulerAngles;
        _projectile.transform.rotation = Quaternion.Euler(rot.x, player.GetComponent<PlayerMovement>().firingPoint.transform.eulerAngles.y + direction, rot.z);
        _projectile.GetComponent<Rigidbody>().AddForce(_projectile.transform.forward * shotPower, ForceMode.Impulse);
        //StartCoroutine(DestroyProjectile(_projectile, destroyAfter));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player.GetComponent<PlayerMovement>()._enemy)
        {
            distance = Vector3.Distance(player.transform.position, player.GetComponent<PlayerMovement>()._enemy.transform.position);

            if (distance < range && (Time.time > fireRate + lastShot))
            {
                StartCoroutine(FireObject());
                lastShot = Time.time;
            }

            if (distance < range)
            {
                player.GetComponent<PlayerMovement>().attackAnim = true;
            }
            else if (distance > range)
            {
                player.GetComponent<PlayerMovement>().attackAnim = false;
            }
        }
        else
        {
            player.GetComponent<PlayerMovement>().attackAnim = false;
        }
    }
}
