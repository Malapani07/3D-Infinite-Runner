using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip Clip;

    [Range(0f, 1f)]
    public float volume;
    [Range(.1f, 3f)]
    public float Pitch;

    public bool loop;

    [HideInInspector]
    public AudioSource Source;

}

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioManager instance;

    private void Awake()
    {
        instance = this;

        foreach (Sound s in sounds)
        {

            s.Source = gameObject.AddComponent<AudioSource>();
            s.Source.clip = s.Clip;
            s.Source.volume = s.volume;
            s.Source.pitch = s.Pitch;
            s.Source.loop = s.loop;
        }
    }

    private void Start()
    {
        GameManager.instance.ScoreIncrement += ScoredPoint;
        Play("BackGround");
    }
    void ScoredPoint()
    {
        AudioManager.instance.Play("Coin");
    }

    public void Play(string name)
    {
        Sound S = Array.Find(sounds, sound => sound.name == name);
        if (S == null) { return; };
        S.Source.Play();
    }
    public void Pause(string name)
    {
        Sound S = Array.Find(sounds, sound => sound.name == name);
        if (S == null) { return; };
        S.Source.Pause();
    }
}
