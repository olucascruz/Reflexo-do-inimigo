using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameInput gameInput;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform cam;
    private float velCam = 5f;
    
    private void Update(){
        Vector2 mousePosition =  gameInput.GetMousePositionVectorNormalized();

         if (playerTransform){
           
            playerTransform.Rotate(0, mousePosition.x, 0, Space.World);
            cam.rotation = Quaternion.Lerp(
                cam.rotation,
                Quaternion.Euler(mousePosition.y * 2, playerTransform.eulerAngles.y, 0),
                100 * Time.deltaTime);
            
        }
    }
}
