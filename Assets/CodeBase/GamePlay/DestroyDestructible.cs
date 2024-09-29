using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDestructible : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        SpaceShip spaceShip = collision.transform.root.GetComponent<SpaceShip>();
        if (spaceShip != null)
        {
            spaceShip.ApplyDamage(spaceShip.HitPoints);
        }
    }
    
}
