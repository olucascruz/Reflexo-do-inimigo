using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public enum GameState {PLAY, PAUSE, QUICKEVENT,GAMEOVER};
    public GameState gameState = GameState.PLAY;

    public enum QuickEventResult {NONE, PASSED, NOTPASSED};
    public QuickEventResult quickEventResult = QuickEventResult.NONE;
    
    private string alphabet = "abcdefghijklmnopqrstuvwxyz";
    [SerializeField] private TextMeshProUGUI selectedLetterText;
    [SerializeField] private GameObject quickEventObj;

    [SerializeField] private GameObject resultVisualP;
    [SerializeField] private GameObject resultVisualN;

    [SerializeField] private GameObject gameOverObj;
    [SerializeField] private GameObject pauseObj;
    [SerializeField] private float timeQuickEvent;

    public static GameManager instance;

    private float delay = 10f;

    private bool inDelay = false;

    private bool onQuickEvent = false; 
    private bool canClick = false; 


    char selectedLetter = 'a';
    void Awake(){
        if(instance == null){
            instance = this;
        }else{
            Destroy(this);
        }

    }

    private void Start(){
        quickEventObj.SetActive(false);
    
    }

    public void QuickEvent(){
        if(inDelay) return;
        
        quickEventObj.SetActive(true);
        char selectedLetter = SelectLetter();
        gameState = GameState.QUICKEVENT;
        StartCoroutine(DelayCanClick());
        StartCoroutine(ItIsDelay());
        inDelay = true;
    }

    private char SelectLetter(){
        selectedLetter = alphabet[Random.Range(0, alphabet.Length)];
        selectedLetterText.text = selectedLetter.ToString();
        return selectedLetter;
    }

    private IEnumerator DelayCanClick(){
        yield return new WaitForSeconds(0.5f);
        canClick = true;
    }

    private IEnumerator ItIsDelay(){
        yield return new WaitForSeconds(delay);
        inDelay = false;
    }

    public void GameOver(){
        gameState = GameState.GAMEOVER;
        gameOverObj.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Pause(){
        gameState = GameState.PAUSE;
        pauseObj.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Resume(){
        gameState = GameState.PLAY;
        pauseObj.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    void Update()
    {
        if(gameState == GameState.GAMEOVER) return;
        
        if(gameState == GameState.QUICKEVENT && !onQuickEvent){
            StartCoroutine(TimeQuickEvent());
            if(Input.anyKeyDown && canClick){
                onQuickEvent = true;
                string inputString = Input.inputString.ToLower();    
                if (inputString.Length > 0)
                {
                    char pressedKey = inputString[0];
                    
                    if(pressedKey == selectedLetter){
                        Passed();
                    }
                    else{
                        NotPassed();
                    }
                }

            }
        }
        
    }

    IEnumerator TimeQuickEvent(){
        yield return new WaitForSeconds(timeQuickEvent);
        if(gameState == GameState.QUICKEVENT){
            NotPassed();
        }
    }
    
    void Passed(){
        quickEventResult = QuickEventResult.PASSED;
        StartCoroutine(ResultVisual("p"));
        EndQuickEvent();
    }

    void NotPassed(){
        quickEventResult = QuickEventResult.NOTPASSED;
        StartCoroutine(ResultVisual("n"));
        EndQuickEvent();
    }

    IEnumerator ResultVisual(string result){
        if(result == "p"){
            resultVisualP.SetActive(true);
        }

        if(result == "n"){
            resultVisualN.SetActive(true);
        }

        yield return new WaitForSeconds(3f);
        resultVisualP.SetActive(false);
        resultVisualN.SetActive(false);
    }

    void EndQuickEvent(){
        quickEventObj.SetActive(false);
        gameState = GameState.PLAY;
        onQuickEvent = false;
        canClick = false;
    }

    
}   
