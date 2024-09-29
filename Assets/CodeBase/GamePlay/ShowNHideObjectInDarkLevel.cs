using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowNHideObjectInDarkLevel : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        SpriteRenderer sprite = collision.GetComponent<SpriteRenderer>();
        if (sprite != null)
        {
            sprite.enabled = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        SpriteRenderer sprite = collision.GetComponent<SpriteRenderer>();

        if (sprite != null)
        {
            sprite.enabled = false;
        }
    }
}
