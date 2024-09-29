using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject m_PlayerHUDPrefab;
    [SerializeField] private GameObject m_LevelGUIPrefab;
    [SerializeField] private GameObject m_BackGroundPrefab;
    [Header("Dependencies")]
    [SerializeField] private PlayerSpawner m_PlayerSpawner;


    [SerializeField] private LevelController m_LevelController;
    private void Awake()
    {
        m_LevelController.Init();


        Player player = m_PlayerSpawner.Spawn();

        player.Init();

        Instantiate(m_PlayerHUDPrefab);
        Instantiate(m_LevelGUIPrefab);

        GameObject background = Instantiate(m_BackGroundPrefab);
        background.AddComponent<SyncTransform>().SetTarget(player.followCamera.transform);
    }
}
