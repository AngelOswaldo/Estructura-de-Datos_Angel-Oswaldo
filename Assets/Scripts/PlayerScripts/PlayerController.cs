using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputAsset;
    private InputAction movement;

    //private Rigidbody2D rig;
    public bool canMove = true;
    public Detector[] detectors;
    public CameraController mainCamera;

    private void Awake()
    {
        //rig = this.GetComponent<Rigidbody2D>();
        movement = inputAsset.FindAction("Movement");
    }

    private void OnEnable()
    {
        movement.performed += Movement;
        movement.Enable();
    }

    private void OnDisable()
    {
        movement.performed -= Movement;
        movement.Disable();
    }

    private void Movement(InputAction.CallbackContext ctx)
    {

        if(canMove)
        {
            canMove = false;
            Vector2 direction = ctx.ReadValue<Vector2>();
            CheckDirection(direction);
            //StartCoroutine(SmoothMovement(direction));
        }
        canMove = true;

    }

    private void CheckDirection(Vector2 inputDirection)
    {
        for(int i = 0; i < detectors.Length; i ++)
        {
            if(inputDirection == detectors[i].direction)
            {
                if(detectors[i].wallHit == true)
                {
                    ChangeDirection(inputDirection, false);
                }
                else if(detectors[i].wallHit == false)
                {
                    ChangeDirection(inputDirection, true);
                }
            }
        }
    }

    private void ChangeDirection(Vector2 direction, bool move)
    {
        if(move == true)
        {
            Vector2 newPosition = new Vector2(transform.position.x + direction.x, transform.position.y + direction.y);
            transform.position = newPosition;
        }
        else if(move == false)
        {
            transform.position = transform.position;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Rooms")
        {
            Room newRoom = collision.GetComponent<Room>();
            mainCamera.MoveCamera(newRoom.node.transform.position);
        }
    }

    /*  private void CheckDirection(InputAction.CallbackContext ctx)
      {
          Vector2 direction = ctx.ReadValue<Vector2>();
          if(direction.x == 1)
          {
              actualDirection = 1;
          }
          else if(direction.x == -1)
          {
              actualDirection = 2;
          }
          else if(direction.y == 1)
          {
              actualDirection = 3;
          }
          else if(direction.y == -1)
          {

          }
      }

      private IEnumerator SmoothMovement(Vector2 direction)
      {
          float step = 0;
          float lerpSpeed = .1f;
          float startPosX = transform.position.x;
          float startPosY = transform.position.y;

          float finalPosX = startPosX + direction.x;
          float finalPosY = startPosY + direction.y;

          while (step<=1)
          {
              step += lerpSpeed;

              rig.MovePosition(new Vector2(Mathf.Lerp(startPosX, finalPosX, step), Mathf.Lerp(startPosY, finalPosY, step)));

              yield return new WaitForFixedUpdate();
          }

          canMove = true;
      }*/


}
