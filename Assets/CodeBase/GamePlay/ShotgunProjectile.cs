using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunProjectile : MonoBehaviour
{
    [HideInInspector]public bool firstBullet = true;
    private void Start()
    {
        if (firstBullet)
        {
            GameObject projectile;
            Quaternion rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + 20);
            projectile = Instantiate(gameObject, transform.position, rotation);
            projectile.GetComponent<ShotgunProjectile>().firstBullet = false;
            projectile.transform.rotation = rotation;
            projectile.GetComponent<Projectile>().SetParentShooter(GetComponent<Projectile>().Parent);
            rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z - 20);
            projectile = Instantiate(gameObject, transform.position, rotation);
            projectile.GetComponent<ShotgunProjectile>().firstBullet = false;
            projectile.transform.rotation = rotation;
            projectile.GetComponent<Projectile>().SetParentShooter(GetComponent<Projectile>().Parent);

        }
    }
}
