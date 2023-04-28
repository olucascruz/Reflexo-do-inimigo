using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    private PlayerInputActions playerInputActions;
    private float rotationX;
    private float rotationY;
    private GameManager gm;
    private void Awake(){
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Start(){
        gm = GameManager.instance;
    }

    void Update()
    {
        if (gm.gameState == GameManager.GameState.PLAY && Input.GetKeyDown(KeyCode.Escape))
        {
            gm.Pause();
        }
        else if (gm.gameState == GameManager.GameState.PAUSE && Input.GetKeyDown(KeyCode.Escape))
        {
            gm.Resume();
        }
    }
    public Vector2 GetMovementVectorNormalized(){
        if(gm.gameState != GameManager.GameState.PLAY) return Vector2.zero;
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();


        inputVector = inputVector.normalized;


        return inputVector;
    }

    public Vector2 GetMousePositionVectorNormalized(){
        if(gm.gameState != GameManager.GameState.PLAY) return Vector2.zero;
        rotationX = Mathf.Lerp(
                rotationX, Input.GetAxisRaw("Mouse X")*2, 100 * Time.deltaTime);
        rotationY = Mathf.Clamp(
                rotationY - (Input.GetAxisRaw("Mouse Y")* 2 * 100 * Time.deltaTime),
                -30, 30);

        Vector2 inputRotation = new Vector2(rotationX, rotationY);
        return inputRotation;
    }
}

