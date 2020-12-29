using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System ; 
public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Sound[] sounds ; 
    
    void Awake()
    {
        // DontDestroyOnLoad(gameObject) ; 
        foreach(Sound s in sounds) { 
            s.source = gameObject.AddComponent<AudioSource>() ;     
            s.source.clip = s.clip ; 
            s.source.volume=s.volume; 
            s.source.loop=s.loop; 
        }
    }

    // Update is called once per frame
    // void Start() { 

    // }
    public void Play (string name ){ 
        Sound sound= Array.Find(sounds,s=>s.name == name) ; 
        if (sound == null ){
            Debug.LogWarning("THERE IS NO SOUND WITH THAT NAME") ;  
            return; 
        }
        Debug.Log(sound.source);
        sound.source.Play() ; 
    }
    public void Pause(string name) { 
        Sound sound= Array.Find(sounds,s=>s.name == name) ; 
        if (sound == null ){
            Debug.LogWarning("THERE IS NO SOUND WITH THAT NAME") ;  
            return; 
        }
        sound.source.Pause() ;
    }
    public void UnPause(string name) { 
        Sound sound= Array.Find(sounds,s=>s.name == name) ; 
        if (sound == null ){
            Debug.LogWarning("THERE IS NO SOUND WITH THAT NAME") ;  
            return; 
        }
        sound.source.UnPause() ;
    }
}
