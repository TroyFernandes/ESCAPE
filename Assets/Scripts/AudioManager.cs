using UnityEngine.Audio;
using System;
using UnityEngine;

/*
 * AudioManager which manages all the sounds clips in the game. You can play any sound from any
 * script by placing this line of code: FindObjectOfType<AudioManager>().Play("CLIP NAME")
 */
public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioManager instance;

    void Awake()
    {
        //Make AudioManager persist between scenes
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }
    //Play a sound clip
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            return;
        }
        s.source.Play();
    }
    //Place any sounds you want to start when the game is loaded here
    void Start()
    {
        Play("ForestSounds");

    }
}
