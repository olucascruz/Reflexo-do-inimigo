using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class Player : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float speed;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private float maxSanity = 1f;
    [SerializeField] private float sanity = 1f;    
    [SerializeField] private Image sanityBar;
    private float limitVelocity = 5f;
    private bool canTakeDamage = true;

    private GameManager gm;
    public float GetSanityAsFraction() {
        return Mathf.Clamp01((float) sanity / maxSanity);
    }

    private void Start(){
        gm = GameManager.instance;
        sanity = maxSanity;
        rb = GetComponent<Rigidbody>();
        sanityBar.fillAmount = GetSanityAsFraction();
    }
 
    private void Update(){
        sanityBar.fillAmount = GetSanityAsFraction();
        if(sanity <= 0){
            gm.GameOver();
        }
        Movement();
        
        if(rb.velocity.magnitude > limitVelocity){
             rb.velocity = rb.velocity.normalized * limitVelocity;
         }
        if(gm.quickEventResult == GameManager.QuickEventResult.NOTPASSED && canTakeDamage){
            canTakeDamage = false;
            StartCoroutine(TakeDamage());
        }
    }

    private void Movement(){
        Vector2 inputVector =  gameInput.GetMovementVectorNormalized();
        float x = inputVector.x;
        float z = inputVector.y;
        Vector3 movement = new Vector3(x, 0, z);
        

        movement = transform.forward * z + transform.right * x;
        movement = movement.normalized;
        
        rb.AddForce(movement * speed * 100 *Time.deltaTime, ForceMode.Force);
    }

    IEnumerator TakeDamage(){
        sanity--;
        yield return new WaitForSeconds(5f);
        canTakeDamage = true;
    }

}
