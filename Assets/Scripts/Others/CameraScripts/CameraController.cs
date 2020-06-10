using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public void MoveCamera(Vector3 node)
    {
        Vector3 newPos = new Vector3(node.x, node.y, transform.position.z);
        transform.position = newPos;
    }
}
