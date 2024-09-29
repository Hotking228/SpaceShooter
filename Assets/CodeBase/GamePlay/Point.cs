using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    float currentLifeTime;
    [SerializeField] private float lifeTime; 
    // Start is called before the first frame update
    void Start()
    {
        currentLifeTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        currentLifeTime += Time.deltaTime;

        if (currentLifeTime >= lifeTime)
        {
            Destroy(gameObject);
        }
    }
}
