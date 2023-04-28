using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{   
    private GameManager gm;

    private GameObject enemy;
    
    private void Start()
    {   
        gm = GameManager.instance;
        enemy = GameObject.FindGameObjectWithTag("Enemy");
    }
    
    void OnTriggerEnter(Collider other)
    {   

       if (other.CompareTag("Player"))
        {
            gm.QuickEvent();
            enemy.SetActive(true);
            enemy.GetComponent<Enemy>().canMove = true;
            enemy.transform.position = transform.position;
            enemy.SetActive(false); 
            StartCoroutine(AttackPlayer());
        }
    }

    IEnumerator AttackPlayer(){
        while (gm.quickEventResult == GameManager.QuickEventResult.NONE){
            yield return new WaitForSeconds(1f);
            if(gm.quickEventResult == GameManager.QuickEventResult.PASSED){
                enemy.SetActive(false);
                EndQuickEvent();
            }
            if(gm.quickEventResult == GameManager.QuickEventResult.NOTPASSED){
                EndQuickEvent();
                enemy.SetActive(true);
            }
        }
    
    }

    void EndQuickEvent(){
        gm.quickEventResult = GameManager.QuickEventResult.NONE;
    }

}
