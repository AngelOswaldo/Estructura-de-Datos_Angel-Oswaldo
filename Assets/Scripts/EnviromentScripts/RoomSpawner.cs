using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public int openingDirection;
    //1--> need bottom door, 2--> need top door, 3--> need left door, 4--> need right door

    private RoomTemplates templates;
    private int rand;
    private bool spawned = false;

    public float waitTime = 1f;

    private void Start()
    {
        Destroy(gameObject, waitTime);
        templates = GameObject.FindGameObjectWithTag("RoomsTemplates").GetComponent<RoomTemplates>();
        Invoke("Spawn", 0.1f);
    }

    private void Spawn()
    {
        if(spawned==false)
        {
            if (openingDirection == 1)
            {
                //1--> need to spawn a room with a bottom door
                rand = Random.Range(0, templates.bottomRooms.Length);
                Instantiate(templates.bottomRooms[rand], transform.position, Quaternion.identity);
            }
            else if (openingDirection == 2)
            {
                //2--> need to spawn a room with a top door
                rand = Random.Range(0, templates.topRooms.Length);
                Instantiate(templates.topRooms[rand], transform.position, Quaternion.identity);
            }
            else if (openingDirection == 3)
            {
                //3--> need to spawn a room with a left door
                rand = Random.Range(0, templates.leftRooms.Length);
                Instantiate(templates.leftRooms[rand], transform.position, Quaternion.identity);
            }
            else if (openingDirection == 4)
            {
                //4--> need to spawn a room with a right door
                rand = Random.Range(0, templates.rightRooms.Length);
                Instantiate(templates.rightRooms[rand], transform.position, Quaternion.identity);
            }

            spawned = true;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("SpawnPoint"))
        {
            if(!collision.GetComponent<RoomSpawner>().spawned && !spawned)
            {
                Instantiate(templates.closedRoom, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }

            spawned = true;
        }
    }
}
