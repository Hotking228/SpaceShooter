using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncTransform : MonoBehaviour
{

    [SerializeField]private Transform targetTransform;

    private void FixedUpdate()
    {
        if (targetTransform == null) return;
        transform.position = new Vector3( targetTransform.position.x, targetTransform.position.y, transform.position.z);
    }

    public void SetTarget(Transform target)
    {
        targetTransform = target;
    }
}
