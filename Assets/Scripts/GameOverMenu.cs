using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOverMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject GameOverMenuUI; 

    // Update is called once per frame
    public void loadMainMenu() { 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1); 
        resetGame(); 
    }
    public void Restart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
        resetGame() ;     
    }
    private void resetGame(){
        SC_GroundGenerator.gameIsOver=false; 
        Time.timeScale=1f;
        GameOverMenuUI.SetActive(false);
    }
}
