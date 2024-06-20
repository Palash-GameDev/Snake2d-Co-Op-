using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public bool isGame2Player = false;
    public bool isMute = false;

    public SoundType[] Sounds;

    public AudioSource soundEffect;
    public AudioSource soundMusic;
    public float _volume = 1f;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        SetVolume(0.75f);
        PlayMusic(global::Sounds.BGM);

    }

    public void Mute(bool status)
    {
        isMute = status;
    }

    public void SetVolume(float volume)
    {
        _volume = volume;
        soundEffect.volume = _volume;
        soundMusic.volume = _volume;
    }
    public void PlayMusic(Sounds sound)
    {
        if (isMute)
            return;

        AudioClip clip = GetSoundClip(sound);
        if (clip != null)
        {
            soundMusic.clip = clip;
            soundMusic.Play();

        }
        else
        {
            Debug.LogError("Sound " + sound + " not found!");
        }
    }

    public void PlaySfx(Sounds sound)
    {
        if (isMute)
            return;

        AudioClip clip = GetSoundClip(sound);
        if (clip != null)
        {
            soundEffect.PlayOneShot(clip);

        }
        else
        {
            Debug.LogError("Sound " + sound + " not found!");
        }
    }


    private AudioClip GetSoundClip(Sounds sound)
    {
        SoundType item = Array.Find(Sounds, i => i.soundType == sound);
        if (item != null)
        {
            return item.soundClip;
        }
        return null;
    }

   
    


}//

[Serializable]
public class SoundType
{
    public Sounds soundType;
    public AudioClip soundClip;
}

public enum Sounds
{
    BGM,
    BUTTON_CLICK,
    FOOD_PICKUP,
    POWER_PICKUP,
    GameOver,
    Gethit
}


