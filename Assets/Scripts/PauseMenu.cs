using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool GameIsPaused=false ; 
    public GameObject PauseMenuUI; 
    public AudioManager audioManager; 
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape)){
            checkPause() ; 
        }
    }
    public void checkPause() { 
        if(GameIsPaused){
                Resume();
                
            }
        else {
                Pause() ; 
                
            }
    }
    public void Resume() { 
        audioManager.UnPause("Upbeat");
        audioManager.Pause("SlowPaced");
        GameIsPaused=false; 
        Time.timeScale=1f;
        PauseMenuUI.SetActive(false);
    }
    public void Pause() { 
        audioManager.Pause("Upbeat");
        audioManager.Play("SlowPaced");
        GameIsPaused=true; 
        Time.timeScale=0;
        PauseMenuUI.SetActive(true); 
    }
    public void Restart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
        Resume(); 
    }
    public void Quit() {
        Application.Quit(); 
    }
    
}
