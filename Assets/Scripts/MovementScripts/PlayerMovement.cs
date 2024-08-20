using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Vector2 movementInput;
    public float movementSpeed = 0.1f;
    public void IAMovement(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue <Vector2>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(movementInput.x * movementSpeed * Time.deltaTime, 0, movementInput.y * movementSpeed *Time.deltaTime);
    }
}
