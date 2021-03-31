using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    public GameObject projectile;
    public float fireRate;
    public float shotPower;
    float lastShot;

    private void Update()
    {
        if ((Time.time > fireRate + lastShot))
        {
            Shoot();
            lastShot = Time.time;
        }
    }

    void Shoot()
    {
        GameObject _projectile = Instantiate(projectile);
        _projectile.transform.position = transform.position;
        Vector3 rot = _projectile.transform.eulerAngles;
        _projectile.transform.rotation = Quaternion.Euler(rot.x,transform.eulerAngles.y, rot.z);
        _projectile.GetComponent<Rigidbody>().AddForce(transform.forward * shotPower, ForceMode.Impulse);
    }
}
