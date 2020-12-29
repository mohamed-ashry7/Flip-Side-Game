using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combined_Tile : MonoBehaviour
{


    public Transform startPoint;
    public Transform endPoint;
    public SC_Tile regularTile;
    public SC_Tile reversedTile;
    
    // Start is called before the first frame update
    public void DeactivateAllObjects(){
        regularTile.DeactivateAllObjects(); 
        reversedTile.DeactivateAllObjects(); 
    }
    public void ActivateRandomObjects(){
        System.Random r1 = new System.Random() ;
        regularTile.ActivateRandomObject(r1); 
        reversedTile.ActivateRandomObject(r1) ; 
    }
}
