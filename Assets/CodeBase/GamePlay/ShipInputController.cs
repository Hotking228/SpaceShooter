using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipInputController : MonoBehaviour
{


    
    public enum ControlMode
    {
        Keyboard,
        Mobile
    }
    [SerializeField] private SpaceShip m_TargetShip;
    public void SetTargetShip(SpaceShip ship) => m_TargetShip = ship;

    [SerializeField]private ControlMode m_ControlMode;


    private VirtualGamePad m_VirtualGamePad;

    public void Construct(VirtualGamePad virtualGamePad)
    {
        m_VirtualGamePad = virtualGamePad; 
    }
    private void Start()
    {
        if (m_ControlMode == ControlMode.Keyboard)
        {
            m_VirtualGamePad.virtualJoystick.gameObject.SetActive(false);
            m_VirtualGamePad.mobileFirePrimary.gameObject.SetActive(false);
            m_VirtualGamePad.mobileFireSecondary.gameObject.SetActive(false);
        }
        else
        {
            m_VirtualGamePad.virtualJoystick.gameObject.SetActive(true);

            m_VirtualGamePad.mobileFirePrimary.gameObject.SetActive(true);
            m_VirtualGamePad.mobileFireSecondary.gameObject.SetActive(true);
        }




    }

    private void Update()
    {
        if (m_TargetShip == null) return;
        if (m_ControlMode == ControlMode.Keyboard)
        {
            ControlKeyboard();
        }

        if (m_ControlMode == ControlMode.Mobile)
        {
            ControlMobile();
        }
    }


    private void ControlMobile()
    {
        var dir = m_VirtualGamePad.virtualJoystick.Value;

        if (m_VirtualGamePad.mobileFirePrimary.IsHold)
        {
            m_TargetShip.Fire(TurretMode.Primary);
        }
        if (m_VirtualGamePad.mobileFireSecondary.IsHold)
        {
            m_TargetShip.Fire(TurretMode.Secondary);
        }
        m_TargetShip.ThrustControl = dir.y;
        m_TargetShip.TorqueControl = -dir.x;
    }

    private void ControlKeyboard()
    {
        float thrust = 0;
        float torque = 0;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            thrust = 1.0f;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            thrust = -1.0f;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            torque = 1.0f;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            torque = -1.0f;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            m_TargetShip.Fire(TurretMode.Primary);
        }
        if (Input.GetKey(KeyCode.X))
        {
            m_TargetShip.Fire(TurretMode.Secondary);
        }
        m_TargetShip.ThrustControl = thrust;
        m_TargetShip.TorqueControl = torque;
    }

}
