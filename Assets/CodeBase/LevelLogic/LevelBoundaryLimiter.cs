using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBoundaryLimiter : MonoBehaviour
{
    private Destructible ship;
    private void Start()
    {
        ship = GetComponent<Destructible>();
    }
    private void Update()
    {
        if (LevelBoundary.Instance == null)
            return;

        var lb = LevelBoundary.Instance;

        var r = lb.Radius;
        
        if(transform.position.magnitude > r)
        {
            if (lb.LimitMode == LevelBoundary.Mode.Limit)
            {
                transform.position = transform.position.normalized * r;
            }
            if (lb.LimitMode == LevelBoundary.Mode.Teleport)
            {
                transform.position = -transform.position.normalized * r;

            }
            if (lb.LimitMode == LevelBoundary.Mode.Destroy)
            {

                ship.ApplyDamage(ship.HitPoints);
            }
        }
    }
}
