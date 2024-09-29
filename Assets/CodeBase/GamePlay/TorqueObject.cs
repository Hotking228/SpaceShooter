using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TorqueObject : MonoBehaviour
{
    [SerializeField] private float speedRotation;
    private float angle = 0;
    private void Update()
    {

        transform.rotation = Quaternion.Euler(0, 0, angle);
        angle += speedRotation;
        if(angle >= 360)
            angle = 0;
    }
}
