using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicList : MonoBehaviour
{
    public AudioClip[] playList;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        
        audioSource = FindAnyObjectByType<AudioSource>();
        audioSource.loop= true;

        
    }

    private AudioClip GetRandomClip()
    {
        return playList[Random.Range(0, playList.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!audioSource.isPlaying)
        {
            audioSource.clip = GetRandomClip();
            audioSource.Play();
        }


    }
}
