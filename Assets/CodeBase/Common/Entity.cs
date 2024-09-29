using UnityEngine;

/// <summary>
/// Basic class of all interactive game objects on scene.
/// </summary>


public abstract class Entity : MonoBehaviour
{
    /// <summary>
    /// Object's name for user
    /// </summary>
    [SerializeField] private string m_Nickname;
    public string Nickname => m_Nickname;



}
