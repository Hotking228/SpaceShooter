using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDebrisParts : MonoBehaviour
{
    [SerializeField] private Destructible[] parts;
    [SerializeField] private float randomSpeed;
     private void OnDestroy()
     {
        if (GetComponent<Destructible>().HitPoints != 0) return;
        for (int i = 0; i < parts.Length; i++)
        {
            Rigidbody2D partRb = Instantiate(parts[i], (Vector2)transform.position + Random.insideUnitCircle * 2, Quaternion.Euler(0,0,Random.Range(0, 360))).GetComponent<Rigidbody2D>();
            if(partRb != null)
            partRb.velocity = Random.insideUnitCircle * randomSpeed;
            
        }
     }
}
