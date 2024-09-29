using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBoundary : MonoBehaviour
{
    #region Singleton
    public static LevelBoundary Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return; 
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    [SerializeField] private float m_Radius;
    public float Radius => m_Radius;

    public enum Mode
    {
        Limit,
        Teleport,
        Destroy
    }
    [SerializeField] private Mode m_limitMode;
    public Mode LimitMode => m_limitMode;

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        UnityEditor.Handles.color = Color.green;
        UnityEditor.Handles.DrawWireDisc(transform.position, transform.forward, m_Radius);
    }
#endif
}
