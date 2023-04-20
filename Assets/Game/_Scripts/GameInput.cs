using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    private PlayerInputActions playerInputActions;
    private float rotationX;
    private float rotationY;
    private bool canMoveCam = true;
    
    private void Awake(){
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update(){
        if(Input.GetKey(KeyCode.P)) canMoveCam = !canMoveCam;
    }

    public Vector2 GetMovementVectorNormalized(){
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();


        inputVector = inputVector.normalized;


        return inputVector;
    }

    public Vector2 GetMousePositionVectorNormalized(){
    
        if(!canMoveCam) return Vector2.zero;
        rotationX = Mathf.Lerp(
                rotationX, Input.GetAxisRaw("Mouse X")*2, 100 * Time.deltaTime);
        rotationY = Mathf.Clamp(
                rotationY - (Input.GetAxisRaw("Mouse Y")* 2 * 100 * Time.deltaTime),
                -30, 30);

        Vector2 inputRotation = new Vector2(rotationX, rotationY);
        return inputRotation;
    }
}

