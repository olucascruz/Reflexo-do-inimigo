using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]private Transform player; 
    [SerializeField]private float moveSpeed = 5f;

    [SerializeField]private AudioSource damage;

    public bool canMove = false;
    
    // Update is called once per frame
    void Update()
    {
        if(player && canMove){
         transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            damage.Play();
            gameObject.SetActive(false);
        }
    }

}
