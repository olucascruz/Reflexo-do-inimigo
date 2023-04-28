using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationController : MonoBehaviour
{
    [SerializeField] private AudioSource[] phrases;
    private GameManager gm;
    void Start()
    {
        gm = GameManager.instance;
        StartCoroutine(StartConversations());
    }

    IEnumerator StartConversations(){
        yield return new WaitForSeconds(30f);
        StartCoroutine(Conversations());
    }

    IEnumerator Conversations(){
    while(true){
            if(gm.gameState == GameManager.GameState.PLAY){
                int randomInt = Random.Range(0, 3);
                phrases[randomInt].Play();
            }
            yield return new WaitForSeconds(30f);
        }
    }
}
