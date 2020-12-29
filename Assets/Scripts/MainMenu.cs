using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement ; 
using UnityEngine.UI; 
public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioManager audioManager; 
    public Button muteButton; 
    [HideInInspector]
    public static bool mute=false  ; 
    void Start() { 
        audioManager.Play("SlowPaced");
    }
    
    public void PlayGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1) ; 
    }
    public void ResumeGame() { 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
    }
    public void Quit() {
        Application.Quit() ; 
    }
    public void Mute() {
         
        if (mute) { 
            AudioListener.volume=1 ;  
        } 
        else{
            AudioListener.volume=0 ;
        }
        // muteButton.GetComponent<Image>().color=color;
        mute =!mute ;  
    }
    
}
