using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speaker : MonoBehaviour
{
    public AudioClip Music;
    public AudioSource MusicSource;

    // Start is called before the first frame update
    void Start()
    {
        //MusicSource = this.gameObject.AddComponent<AudioSource>();
        MusicSource.loop = true;
        MusicSource.playOnAwake = true;
        if (Music != null)
            MusicSource.clip = Music;
        MusicSource.volume = 0.5f;
        MusicSource.Play();
    }

}
