using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMusic : MonoBehaviour
{
    public static bool isPlaying = false;

    [SerializeField] private AudioSource source;
    
    
    public void Start()
    {
        if(isPlaying)
            return;
        
        isPlaying = true;
        source.Play();
        DontDestroyOnLoad(this.gameObject);

    }
}
