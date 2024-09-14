using System.Collections;
using System.Collections.Generic;
using System.Net.WebSockets;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Vector2 movementInput;
    public float movementSpeed = 0.1f;
    public Cinemachine.CinemachineFreeLook playerCamera;
    public float gravity = 9.8f;
    public float jumpSpeed = 20f;
    public float verticalSpeed = 0;
    public float rayLength = 5f;
    public void IAMovement(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue <Vector2>();
    }

    public void IAJump(InputAction.CallbackContext context)
    {

        if(context.started == true && GroundCheck())
        {
            Debug.Log("Jump press");
            verticalSpeed = jumpSpeed;
        }
    }
      
    public void IAInteraction(InputAction.CallbackContext context)
    {
        InteractionRayCast();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GroundCheck() == true && verticalSpeed <= 0)
        {
            verticalSpeed = 0; 
        }
        else
        {
            verticalSpeed = verticalSpeed - gravity * Time.deltaTime;
        }
        transform.Translate(movementInput.x * movementSpeed * Time.deltaTime, verticalSpeed * Time.deltaTime, movementInput.y * movementSpeed *Time.deltaTime);
    }

    public bool GroundCheck()
    {
        return Physics.Raycast(transform.position, transform.up * -1, 1.1f);
    }

    public void InteractionRayCast()
    {
        
        Vector3 CameraDirection = Camera.main.transform.forward;

        Ray Interaction = new Ray(transform.transform.position, CameraDirection);

        RaycastHit targetInfo;

        if(Physics.Raycast(Interaction, out targetInfo, rayLength) == true)
        {
            Debug.DrawLine(playerCamera.transform.position, playerCamera.transform.position + (CameraDirection * rayLength), Color.red, 2f);

            if(targetInfo.transform.gameObject.GetComponent<InteractionComponent>() == true)
            {
                targetInfo.transform.gameObject.GetComponent<InteractionComponent>().InteractionExecuted();
            }
            else
            {
                Debug.Log("Hit object does not contain an interaction component");
            }
        }
        else 
        {
            Debug.DrawLine(playerCamera.transform.position, playerCamera.transform.position + (CameraDirection * rayLength), Color.green, 2f);
        }
        
    }
}

