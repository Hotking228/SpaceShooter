using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : SingletonBase<Player>
{
    public static SpaceShip SelectedSpaceShip;


    [SerializeField] private int m_NumLives;
    public int NumLives => m_NumLives;
    private SpaceShip m_Ship;
    [SerializeField] private SpaceShip m_PlayerShipPrefab;
    public SpaceShip ActiveShip => m_Ship;


    [SerializeField] private FollowCamera m_CameraController;
    [SerializeField] private ShipInputController m_shipInputController;
    [SerializeField] private Vector3 respawnPos;

    [SerializeField] private int m_Score;
    [SerializeField] private int m_NumKills;
    private Transform m_SpawnPoint;

    public FollowCamera followCamera => m_CameraController;

    public int Score => m_Score;
    public int NumKills => m_NumKills;


    public void Construct(FollowCamera followCamera, ShipInputController shipInputController, Transform spawnPoint)
    {
        m_CameraController = followCamera;
        m_shipInputController = shipInputController;
        respawnPos = spawnPoint.position;
    }

    public SpaceShip ShipPrefab
    {
        get
        {
            if (SelectedSpaceShip == null)
            {
                return m_PlayerShipPrefab;
            }
            else
            {
                return SelectedSpaceShip;
            }
        }
    }

    private void Start()
    {
        if(ActiveShip == null)
        Respawn();

        
    }
    
    public void OnShipDeath()
    {

        m_NumLives--;
        
        if (m_NumLives > 0)
            Respawn();
    }

    private void Respawn()
    {
        var newPlayerShip = Instantiate(ShipPrefab, respawnPos, transform.rotation);

        m_Ship = newPlayerShip.GetComponent<SpaceShip>();

        m_CameraController.SetTarget(m_Ship.transform);
        m_shipInputController.SetTargetShip(m_Ship);
    }



    public void AddKill()
    {
        m_NumKills++;
    }

    public void AddScore(int num)
    {
        m_Score += num;
    }

    private void Update()
    {
        if (ActiveShip.HitPoints <= 0 && m_NumLives >0 && ActiveShip == null)
            OnShipDeath();
    }
}
