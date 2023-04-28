using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsGameOver : MonoBehaviour
{
   public void Retry(){
        SceneManager.LoadScene("GameScene");
   }

   public void Quit(){
        SceneManager.LoadScene("MenuScene");
   }
}
