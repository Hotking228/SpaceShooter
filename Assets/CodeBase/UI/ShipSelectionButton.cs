using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShipSelectionButton : MonoBehaviour
{


    [SerializeField] private SpaceShip m_Prefab;
    [SerializeField] private MainMenu m_MainMenu;
    [SerializeField] private TextMeshProUGUI m_ShipName;
    [SerializeField] private TextMeshProUGUI m_HitPoints;
    [SerializeField] private TextMeshProUGUI m_Speed;
    [SerializeField] private TextMeshProUGUI m_Agility;

    [SerializeField] private Image m_Preview;

    private void Start()
    {
        if (m_Prefab == null) return;

        m_ShipName.text = m_Prefab.Nickname;
        m_HitPoints.text = "HitPoints : " + m_Prefab.MaxHitPoints;
        m_Speed.text = "Velocity : " + m_Prefab.MaxLinearVelocity.ToString();
        m_Agility.text = "Agility : " + m_Prefab.MaxAngularVelocity.ToString();
        m_Preview.sprite = m_Prefab.PreviewImage;
    }

    public void SelectShipPlayer()
    {
        Player.SelectedSpaceShip = m_Prefab;
        m_MainMenu.ShowMainPanel();
    }

   
}
