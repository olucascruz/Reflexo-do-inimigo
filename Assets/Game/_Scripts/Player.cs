using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float speed;
    [SerializeField] private GameInput gameInput;

    
    private float limitVelocity = 5f;
    private void Start(){
        rb = GetComponent<Rigidbody>();
    }
 
    private void Update(){
        Vector2 inputVector =  gameInput.GetMovementVectorNormalized();
        float x = inputVector.x;
        float z = inputVector.y;
        Vector3 movement = new Vector3(x, 0, z);
        

        movement = transform.forward * z + transform.right * x;
        movement = movement.normalized;
        
        rb.AddForce(movement * speed * 100 *Time.deltaTime, ForceMode.Force);

        if(rb.velocity.magnitude > limitVelocity){
             rb.velocity = rb.velocity.normalized * limitVelocity;
         }
        
    }
}
