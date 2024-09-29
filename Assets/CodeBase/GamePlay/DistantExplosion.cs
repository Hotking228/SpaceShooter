using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))] 
public class DistantExplosion : MonoBehaviour
{
    [SerializeField]private FollowTarget target;
    [SerializeField]private Projectile projectile;
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.root.GetComponent<Destructible>() == projectile.Parent || collision.isTrigger) return;
        if (target.Target == collision.transform.root.GetComponent<Destructible>())
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, GetComponent<CircleCollider2D>().radius);
            for (int i = 0; i < colliders.Length; i++)
            {
                Destructible dest = colliders[i].transform.root.GetComponent<Destructible>();
                if (dest != null && dest != projectile.Parent)
                    dest.ApplyDamage(projectile.Damage);
            }
            projectile.OnProjectileLifetimeEnd(collision, transform.position);
        }
    }
}
