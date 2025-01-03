using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    public AudioClip[] SEClips; // SE用のAudioClip配列
    public AudioClip[] BGMClips; // BGM用のAudioClip配列

    [SerializeField] public AudioSource SEAudioSource;
    [SerializeField] public AudioSource BGMAudioSource;
    
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
    
    // 指定したファイル名をSEClips内で探し再生.
    public void PlaySound(string fileName)
    {
        AudioClip clip = SEClips.FirstOrDefault(x => x.name == fileName);
        if (clip != null)
        {
            SEAudioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogError("再生'{fileName}'はSEClipsの配列に存在しない");
        }
    }


    // 指定したファイル名のBGMClips内で探し再生.
    public void PlayLoop(string fileName)
    {
        AudioClip clip = BGMClips.FirstOrDefault(x => x.name == fileName);
        if (clip != null)
        {
            BGMAudioSource.clip = clip;
            BGMAudioSource.loop = true;
            BGMAudioSource.Play();
        }
        else
        {
            Debug.LogError("ループ'{fileName}'はBGMClipsの配列に存在しないためできない");
        }
    }


    // 現在再生中の音を停止.
    public void StopSound()
    {
        if (BGMAudioSource.isPlaying)
        {
            BGMAudioSource.Stop();
        }
        
    }
}