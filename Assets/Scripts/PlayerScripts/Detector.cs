using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    public Vector2 direction;
    public bool wallHit;

    private void Start()
    {
        wallHit = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Wall")
        {
            wallHit = true;
            Debug.Log(direction);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Wall")
        {
            wallHit = false;
        }
    }

}
