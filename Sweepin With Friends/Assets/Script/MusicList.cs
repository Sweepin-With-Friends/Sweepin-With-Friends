using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicList : MonoBehaviour
{
    public AudioClip[] playList;
    private AudioSource banana;

    // Start is called before the first frame update
    void Start()
    {
        
        banana = gameObject.AddComponent<AudioSource>();
        banana.loop= true;
        banana.volume= 0.33f;
        
    }

    private AudioClip GetRandomClip()
    {
        return playList[Random.Range(0, playList.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!banana.isPlaying)
        {
            banana.clip = GetRandomClip();
            banana.Play();
            
        }


    }
}
