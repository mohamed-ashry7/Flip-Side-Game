using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro; 
public class Player_SC : MonoBehaviour
{
    // Start is called before the first frame update

    public Material[] colors; 
    public TextMeshProUGUI textScore ; 
    public TextMeshProUGUI textHealth; 
    public SC_GroundGenerator GG ; 
    public AudioManager audioManager; 
    // float turningRate=0.01f;
    // float planeCoordinates=1.5f;
    bool flipped =false; 
    float timeRecorded;
    float waitingTime=15.0f; 
    int scoreCounter=0; 
    int HPCounter=3; 
    System.Random random = new System.Random() ; 
    bool playGameOverScreen=false ;

   
    void Start()
    {
        audioManager.Play("Upbeat") ; 

        textScore.text = "Score Points: 0" ; 
        timeRecorded= 0 ;
        changeBallColorNoSound(); 

    }

    private void changeBallColorNoSound(){ 
        transform.GetChild(0).GetComponent<Renderer>().material.color=colors[random.Next(0,colors.Length)].color;

    }
    private void changeBallColor(){
        audioManager.Play("ColorChange");
        changeBallColorNoSound() ; 
    }
    // Update is called once per frame
    void Update()
    {
        float androidX = Input.acceleration.x ; 
        timeRecorded+=Time.deltaTime; 
        if (timeRecorded>=waitingTime){
            changeBallColor() ; 
            timeRecorded-=waitingTime;
        }

        float hx = Input.GetAxis("Horizontal") ; 
        if(hx>0 || androidX>0.2){
            float posX = this.gameObject.transform.position.x; 
            float newX = Math.Min(posX+1,1);
            if(transform.position.x<newX){
                this.gameObject.transform.position += new Vector3(Time.deltaTime*0.01f,0,0);
            } 
        }
        if (hx<0 || androidX<-0.2){
            float posX = this.gameObject.transform.position.x; 
            float newX = Math.Max(posX-1,-1);
            if(transform.position.x>newX){
                this.gameObject.transform.position -= new Vector3(Time.deltaTime*0.01f,0,0);
            }
        }
        if (Input.GetKeyUp(KeyCode.Space)){
             
            moveThePlayer() ;  
        }
        if (Input.GetKeyUp(KeyCode.R)){
            changeBallColor(); 
        }
        if (Input.GetKeyUp(KeyCode.E)){
            HPCounter = Math.Min(HPCounter+1,3) ; 
        }
        if (Input.GetKeyUp(KeyCode.Q)){
            scoreCounter+=10 ; 
        }
        // if (Input.GetKeyUp(KeyCode.Escape)){
        //     if(PauseMenu.GameIsPaused){
        //         audioManager.Pause("Upbeat");
        //         audioManager.Play("SlowPaced");
        //     }
        //     else { 
        //         audioManager.UnPause("Upbeat");
        //         audioManager.Pause("SlowPaced");
        //     }
        // }
        if (SC_GroundGenerator.gameIsOver && !playGameOverScreen){
            playGameOverScreen= true ;
            Debug.Log("YEEESSS") ; 
            audioManager.Pause("Upbeat");
            audioManager.Play("SlowPaced");
        }
        
        
        textScore.text = "Score: "+ scoreCounter.ToString() ;
        textHealth.text = "Health: " + HPCounter.ToString() ; 
        GG.UpdateScore(scoreCounter,flipped);
    }
    public void moveThePlayer(){
        if (flipped) { 
                this.gameObject.transform.position= new Vector3(transform.position.x,0.25f,transform.position.z);
            }
            else {
                this.gameObject.transform.position= new Vector3(transform.position.x,2.75f,transform.position.z);
            }
            audioManager.Play("Swoop");
            flipped = !flipped ;
    }
    private void OnTriggerEnter(Collider other) {

        if (other.gameObject.CompareTag("ScorePoint")){ 
            Color playerColor = this.transform.GetChild(0).GetComponent<Renderer>().material.color ;  
            Color otherColor =other.gameObject.transform.GetChild(0).GetComponent<Renderer>().material.color ; 
            if (flipped){
                if( otherColor==playerColor) { 
                    scoreCounter-=5 ;
                    audioManager.Play("Wrong");
                }
                else {
                    scoreCounter+=10; 
                    audioManager.Play("ScorePoint");
                }
            }
            else {
                if( otherColor==playerColor) { 
                    scoreCounter+=10 ;
                    audioManager.Play("ScorePoint");
                    }
                else {
                    scoreCounter-=5 ; 
                    audioManager.Play("Wrong");
                }
            }
            Debug.Log(scoreCounter);
            other.gameObject.SetActive(false);
        }
        else if (other.gameObject.CompareTag("HP"))  { 


            audioManager.Play("Heal");
            HPCounter = Math.Min(HPCounter+1,3) ; 
            other.gameObject.SetActive(false) ;
        }
        else if (other.gameObject.CompareTag("Obstacle")) {
            audioManager.Play("Bricks");
            HPCounter--; 
            other.gameObject.SetActive(false);
            if (HPCounter<0) {
                SC_GroundGenerator.gameIsOver=true;
                Destroy(this.gameObject);
            }

        }
        
    }
}
