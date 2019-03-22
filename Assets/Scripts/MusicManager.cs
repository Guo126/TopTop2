using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

    private AudioSource audioSource;
    public static MusicManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Play(AudioClip ac,float time)
    {
        audioSource.clip = ac;
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.volume = Random.Range(0.9f, 1.2f);
        audioSource.time = time;
        audioSource.Play();
    }
    public void PlayMusic(AudioClip ac)
    {
        audioSource.clip = ac;
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.volume = Random.Range(0.9f, 1.2f);
        audioSource.Play();
    }
}
