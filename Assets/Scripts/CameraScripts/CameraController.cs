using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Rigidbody2D rig;

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }


    public void MoveCamera(Vector3 node)
    {
        /*Vector3 newPos = new Vector3(node.x, node.y, transform.position.z);
        transform.position = newPos;*/
        Vector2 move = new Vector2(node.x, node.y);

        StartCoroutine(SmoothMovement(move));
    }

    private IEnumerator SmoothMovement(Vector2 direction)
    {
        float step = 0;
        float lerpSpeed = .02f;
        float startPosX = transform.position.x;
        float startPosY = transform.position.y;

        float finalPosX = direction.x;
        float finalPosY = direction.y;

        while (step <= 1)
        {
            step += lerpSpeed;

            rig.MovePosition(new Vector2(Mathf.Lerp(startPosX, finalPosX, step), Mathf.Lerp(startPosY, finalPosY, step)));

            yield return new WaitForFixedUpdate();
        }

    }
}
