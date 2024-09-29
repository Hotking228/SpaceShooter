using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
[RequireComponent(typeof(Projectile))]
public class FollowTarget : MonoBehaviour
{
    private Destructible target;
    public Destructible Target=>target;
    private Destructible parent;
    private Projectile thisProjectile;
    [SerializeField] private float rotationSpeed;
    
    private void Start()
    {
        thisProjectile = GetComponent<Projectile>();
        parent = thisProjectile.Parent;
    }
    private void Update()
    {
       if (target != null)
       {
            Follow();
       }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (target != null) return;
        Destructible dest = collision.transform.root.GetComponent<Destructible>();
        if (dest != null && dest != parent && dest.GetComponent<SpaceShip>()!=null)
        {
            target = dest;
        }
    }
    private void Follow()
    {
        Vector3 targetRotation = (target.transform.position - transform.position).normalized;
        transform.up = targetRotation;
       
       
    }
}
