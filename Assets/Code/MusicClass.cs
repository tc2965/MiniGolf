using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicClass : MonoBehaviour
{
    AudioSource _audioSource;
    public AudioClip sound1; 
    public AudioClip sound2; 
    public AudioClip sound3; 

    private void Awake() 
    {
        if (GameObject.FindGameObjectsWithTag("Music").Length > 1) {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
        _audioSource = GetComponent<AudioSource>();
        ChangeMusic(1);
    }

    public void PlayMusic()
    {
        if (_audioSource.isPlaying) return;
        _audioSource.Play();
    }

    public void Update() 
    {
        _audioSource.loop = true;
    }

    public void ChangeMusic(int soundNum){
        if(soundNum == 1){
            _audioSource.clip = sound1; 
            _audioSource.volume = .5f;
            _audioSource.Play();
        } else if(soundNum == 2){
            _audioSource.clip = sound2; 
            _audioSource.volume = .8f;
            _audioSource.Play();
        } else if(soundNum == 3){
            _audioSource.clip = sound3; 
            _audioSource.volume = 1f;
            _audioSource.Play();
        }
    }
}
