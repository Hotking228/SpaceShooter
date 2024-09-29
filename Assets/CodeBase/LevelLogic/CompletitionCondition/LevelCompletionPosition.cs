using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR;

public class LevelCompletionPosition : LevelCondition
{
    [SerializeField] private float m_Radius;

    public override bool isCompleted
    {
        get
        {
            if(Player.Instance.ActiveShip != null)
            if (Vector3.Distance(Player.Instance.ActiveShip.transform.position, transform.position) <= m_Radius)
                return true;
            return false;
     
        
        
        }
    }

#if UNITY_EDITOR

    private Color gizmosColor = new Color(1, 1, 1, 0.3f);

    private void OnDrawGizmosSelected()
    {
        Handles.color = gizmosColor;
        Handles.DrawSolidDisc(transform.position, transform.forward, m_Radius);
    }

#endif
}
