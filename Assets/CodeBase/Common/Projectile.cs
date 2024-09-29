using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Entity
{
    [SerializeField] private float m_Velocity;
    [SerializeField] private float m_Lifetime;
    [SerializeField] private int m_Damage;
    public int Damage => m_Damage;
    [SerializeField] private ImpactEffect m_ImpactEffect;
    private float m_Timer;

    private void Update()
    {
        float stepLength = Time.deltaTime * m_Velocity;
        Vector2 step = transform.up * stepLength;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, stepLength);

        if (hit)
        {
            Destructible dest =  hit.collider.transform.root.GetComponent<Destructible>();

            if (dest != null && dest != m_Parent)
            {
                dest.ApplyDamage(m_Damage);


                if (dest.HitPoints <= 0)
                {
                    if (m_Parent == Player.Instance.ActiveShip)
                    {
                        Player.Instance.AddScore(dest.ScoreValue);

                        if (dest is SpaceShip) 
                        {
                            Player.Instance.AddKill();
                        }
                    }
                }
                
            }
            
            OnProjectileLifetimeEnd(hit.collider, hit.point);
        }


        m_Timer += Time.deltaTime;
        if (m_Timer >= m_Lifetime)
        {
            Destroy(gameObject);
        }

        transform.position += new Vector3(step.x, step.y, 0);

    }
    public void OnProjectileLifetimeEnd(Collider2D collider, Vector2 position)
    {
        if(m_ImpactEffect!=null)
        Instantiate(m_ImpactEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
        
    }
   

    private Destructible m_Parent;
    public Destructible Parent => m_Parent;

    public void SetParentShooter(Destructible parent)
    {
        m_Parent = parent;
    }
}
