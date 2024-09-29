using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteLevel : MonoBehaviour
{
    [SerializeField] private GameObject completeLevelPanel;
    [SerializeField] private ShipInputController movementController;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        SpaceShip ship = collision.transform.root.GetComponent<SpaceShip>();
        if (ship != null)
        {
            movementController.enabled = false;
            completeLevelPanel.SetActive(true);
        }
    }
}
