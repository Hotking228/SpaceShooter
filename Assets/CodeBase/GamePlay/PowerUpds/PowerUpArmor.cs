using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpArmor : Powerup
{
    [SerializeField] private float indestructibleTime;
    protected override void OnPickedUp(SpaceShip ship)
    {
        ship.SetIndestructible(indestructibleTime);
    }
}
