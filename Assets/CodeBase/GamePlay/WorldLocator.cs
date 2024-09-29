using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WorldLocator : MonoBehaviour
{
    [HideInInspector] public UnityEvent<Collider2D> collidePoint;
    private string IgnoreTag = "PlayerShip";
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.root.gameObject.tag!= IgnoreTag)
        collidePoint.Invoke(collision);
    }
}
