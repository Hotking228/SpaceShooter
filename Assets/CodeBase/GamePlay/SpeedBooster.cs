using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBooster : Powerup
{
    [SerializeField] private float boostTime;
    [SerializeField] private float newThrust;
    protected override void OnPickedUp(SpaceShip ship)
    {
        ship.SetSpeedBoost(newThrust, boostTime);
    }
}
