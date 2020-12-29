using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Tile : MonoBehaviour
{
    // Start is called before the first frame update
    
    
    public GameObject[] collectablesObstacles;
    
    public Material[] colors; 
    public void ActivateRandomObject(System.Random random){
        DeactivateAllObjects() ; 
        int  [] arrWeightedBiased = {0,0,0,0,0,1,2,2,2,2,2,3,3,3,3,3,4,4,4,4} ;  
        int randomIndex = random.Next(0,arrWeightedBiased.Length);
        int randomInt = arrWeightedBiased[randomIndex] ; 
        GameObject chosen = collectablesObstacles[randomInt]; 
        if (chosen.name=="Obstacle1" || chosen.name=="HP" || chosen.name=="ScorePoint" ){
            int posX= random.Next(-1,2) ; 
            chosen.transform.position= new Vector3(posX,chosen.transform.position.y,chosen.transform.position.z);
        }
        else if (chosen.name=="Obstacle2"){
            float posX=(float)random.Next(0,2);
            float posXf= posX-0.5f;
            chosen.transform.position= new Vector3(posXf,chosen.transform.position.y,chosen.transform.position.z);
        }
        if (chosen.name=="ScorePoint") { 

            
            chosen.transform.GetChild(0).GetComponent<Renderer>().material.color=colors[random.Next(0,colors.Length)].color; 
        }
        chosen.SetActive(true); 
    }
    public void DeactivateAllObjects() { 
        for (int i = 0 ; i < collectablesObstacles.Length ; i++) { 
            collectablesObstacles[i].SetActive(false); 
        }
    }
}
