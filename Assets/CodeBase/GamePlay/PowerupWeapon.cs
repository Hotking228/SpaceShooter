using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupWeapon : Powerup
{
    [SerializeField] private TurretProperties m_Properties;
    protected override void OnPickedUp(SpaceShip ship)
    {
        ship.AssignWeapon(m_Properties);
    }
}
