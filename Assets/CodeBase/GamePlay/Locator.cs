using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locator : MonoBehaviour
{
    [SerializeField] private GameObject pointPrefab;
    [SerializeField] private WorldLocator worldLocator;
    private Transform shipTransform;
    [SerializeField] private int locatorRadiusSize;
    [SerializeField] private float worldLocatorRadiusSize;
    [SerializeField] private Transform baseLocator;

    private void Start()
    {
        worldLocator.collidePoint.AddListener(OnEnterLocator);
        shipTransform = worldLocator.transform.root.transform;
    }


    private void OnEnterLocator(Collider2D collision)
    {
        Vector2 relativeCoord = collision.transform.position - shipTransform.position;
        Vector2 pointCoord = TransformCoordToLocator(relativeCoord);
        var point = Instantiate(pointPrefab, pointCoord, Quaternion.identity, baseLocator);
        point.transform.position = pointCoord + new Vector2(baseLocator.position.x, baseLocator.position.y) ;
    }

    private Vector2 TransformCoordToLocator(Vector2 coord)
    {
        Vector2 locatorCoord = coord/ worldLocatorRadiusSize * locatorRadiusSize;
        return locatorCoord;
    }

}
