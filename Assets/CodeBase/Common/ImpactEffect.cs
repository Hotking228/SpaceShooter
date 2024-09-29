using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactEffect : MonoBehaviour
{
    [SerializeField] private float lifeTime;
    private float time;

    private void Update()
    {
        time += Time.deltaTime;
        if(time >= lifeTime)
            Destroy(gameObject);
    }
}
