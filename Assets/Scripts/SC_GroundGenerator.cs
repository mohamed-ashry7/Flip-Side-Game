using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
public class SC_GroundGenerator : MonoBehaviour
{

    public Camera mainCamera;
    public Combined_Tile tilePrefab;
    public Transform startPoint; 
    float movingSpeed=5;
    public int tilesToPreSpawn=15;


    List<Combined_Tile> spawnedTiles = new List<Combined_Tile>(); 
    [HideInInspector]
    public static bool gameIsOver = false;
    int  score=0 ; 
    bool flipped = false ;
    public GameObject gameOverMenu; 
    bool cameraIsNormal=true;
    // Start is called before the first frame update
    void Start()
    {
        
        Vector3 spawnPosition = startPoint.position;

        for(int i = 0 ; i< tilesToPreSpawn; i++ ){
            spawnPosition -= tilePrefab.startPoint.localPosition; 
            
            Combined_Tile createdTile =Instantiate(tilePrefab,spawnPosition,Quaternion.identity) as Combined_Tile ;

            createdTile.DeactivateAllObjects();

            spawnPosition=createdTile.endPoint.position;

            createdTile.transform.SetParent(transform);

            spawnedTiles.Add(createdTile);
        }
    }

    // Update is called once per frame
    void Update()
    {
      
        if (Input.GetKeyUp(KeyCode.V)){
            gameIsOver=true;
        }
        if (!gameIsOver)
        {
            transform.Translate(-spawnedTiles[0].transform.forward * Time.deltaTime * (movingSpeed + (score/500)), Space.World);
        }
        else {
            gameOverMenu.SetActive(true) ;
        }

        if (mainCamera.WorldToViewportPoint(spawnedTiles[0].endPoint.position).z +10 < 0)
        {
            //Move the tile to the front if it's behind the Camera
            Combined_Tile tileTmp = spawnedTiles[0];
            spawnedTiles.RemoveAt(0);
            tileTmp.transform.position = spawnedTiles[spawnedTiles.Count - 1].endPoint.position - tileTmp.startPoint.localPosition;
            tileTmp.ActivateRandomObjects();
            spawnedTiles.Add(tileTmp);

        }
         
        if (Input.GetKeyUp(KeyCode.Space)){
             switchPlatforms() ; 
        }
        if (Input.GetKeyUp(KeyCode.C)){
             switchCameras() ; 
        }   
        
    }
    public void switchPlatforms() { 
        Vector3 camPos= mainCamera.gameObject.transform.position; 
        if (flipped)
            mainCamera.gameObject.transform.position=new Vector3(camPos.x,camPos.y-1.0f,camPos.z); 
        else
            mainCamera.gameObject.transform.position=new Vector3(camPos.x,camPos.y+1,camPos.z); 
        flipped=!flipped;
    }
    public void switchCameras() { 
        if(cameraIsNormal){
                setCameraSidePerspective() ; 
        }
        else {
            setCameraThirdPerson() ; 
        }
        cameraIsNormal=!cameraIsNormal;
    }
    public void setCameraThirdPerson() { 
        int yy=1 ; 
        if (flipped)
            yy++; 
        mainCamera.gameObject.transform.position=new Vector3(0,yy,-5) ; 
        mainCamera.gameObject.transform.rotation= Quaternion.Euler(10,4,0.001f) ;
    }
    public void setCameraSidePerspective(){
        int yy= 1 ; 
        if (flipped)
            yy++;
        mainCamera.gameObject.transform.position=new Vector3(5,yy,-5) ;
        mainCamera.gameObject.transform.rotation= Quaternion.Euler(10,-60,0.001f) ;
    }
    public void UpdateScore(int scoreUpdated, bool flippedVar){
        if (scoreUpdated%50 ==0 )
            score = scoreUpdated*5;
        flipped= flippedVar;
    }
}




