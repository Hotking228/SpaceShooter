using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncRotation : MonoBehaviour
{
    [SerializeField] private Transform target;

    private void FixedUpdate()
    {
        if (target == null) return;

        transform.rotation = target.rotation;
    }
}
