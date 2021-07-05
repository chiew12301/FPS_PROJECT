using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public Sound[] BGMsounds; //Store Every Sounds, Refer Inspector
    public Sound[] SFXsounds;

    public static AudioManager instance;


    public float allBGMVolume = 0.5f;
    public float allSFXVolume = 0.5f;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null) //For Multiple Scene Purpose
        {
            instance = this;
        }           
        else
        {
            Destroy(gameObject);
            return;
        }
        
        DontDestroyOnLoad(gameObject);

        foreach(Sound s in BGMsounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;

            s.source.loop = s.loop;
        }

        foreach (Sound s in SFXsounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;

            s.source.loop = s.loop;
        }
    }

    private void Start()
    {
        allBGMVolume = 0.5f;
        allSFXVolume = 0.5f;
        Play("MainMenuBGM", "BGM"); //Play BGM
    }

    private void Update()
    {
        AdjustAllVolume(allBGMVolume, "BGM");
        AdjustAllVolume(allSFXVolume, "SFX");
    }

    public bool FindIsPlaying(string name, string which)
    {
        if(which == "BGM")
        {
            Sound s = Array.Find(BGMsounds, sound => sound.name == name);
            if (s == null) //Warning Debug
            {
                Debug.LogWarning("Sounds: " + name + " not found!");
                return false;
            }
            return s.source.isPlaying;
        }
        else if (which == "SFX")
        {
            Sound s = Array.Find(SFXsounds, sound => sound.name == name);
            if (s == null) //Warning Debug
            {
                Debug.LogWarning("Sounds: " + name + " not found!");
                return false;
            }
            return s.source.isPlaying;
        }
        else { return false; }
 
    }

    public void Play(string name, string which) //Play Sound source
    {
        if(which == "BGM")
        {
            Sound s = Array.Find(BGMsounds, sound => sound.name == name);
            if (s == null) //Warning Debug
            {
                Debug.LogWarning("Sounds: " + name + " not found!");
                return;
            }

            s.source.Play();
        }
        else if(which == "SFX")
        {
            Sound s = Array.Find(SFXsounds, sound => sound.name == name);
            if (s == null) //Warning Debug
            {
                Debug.LogWarning("Sounds: " + name + " not found!");
                return;
            }
            s.source.Play();
        }

    }

    public void Pause(string name, string which) //Pause Sound source
    {
        if(which == "BGM")
        {
            Sound s = Array.Find(BGMsounds, sound => sound.name == name);
            if (s == null) //Warning Debug
            {
                Debug.LogWarning("Sounds: " + name + " not found!");
                return;
            }
            s.source.Pause();
        }
        else if(which == "SFX")
        {
            Sound s = Array.Find(SFXsounds, sound => sound.name == name);
            if (s == null) //Warning Debug
            {
                Debug.LogWarning("Sounds: " + name + " not found!");
                return;
            }
            s.source.Pause();
        }
        
    }

    public void Stop(string name,string which) //Stop Sound source
    {
        if(which == "BGM")
        {
            Sound s = Array.Find(BGMsounds, sound => sound.name == name);
            if (s == null) //Warning Debug
            {
                Debug.LogWarning("Sounds: " + name + " not found!");
                return;
            }
            s.source.Stop();
        }
        else if(which == "SFX")
        {
            Sound s = Array.Find(SFXsounds, sound => sound.name == name);
            if (s == null) //Warning Debug
            {
                Debug.LogWarning("Sounds: " + name + " not found!");
                return;
            }
            s.source.Stop();
        }
        
    }

    public void StopAll() //Stop Sound source
    {
        foreach (Sound s in BGMsounds)
        {
            s.source.Stop();
        }

        foreach (Sound s in SFXsounds)
        {
            s.source.Stop();
        }

    }


    public void AdjustVolume(string name, float amount, string which)
    {
        if(which == "BGM")
        {
            Sound s = Array.Find(BGMsounds, sound => sound.name == name);
            if (s == null) //Warning Debug
            {
                Debug.LogWarning("Sounds: " + name + " not found!");
                return;
            }
            if (amount >= 1)
            {
                amount = 1;
            }
            s.volume = amount;
            s.source.volume = s.volume;
        }
        else if (which == "SFX")
        {
            Sound s = Array.Find(SFXsounds, sound => sound.name == name);
            if (s == null) //Warning Debug
            {
                Debug.LogWarning("Sounds: " + name + " not found!");
                return;
            }
            if (amount >= 1)
            {
                amount = 1;
            }
            s.volume = amount;
            s.source.volume = s.volume;
        }
        
    }

    public void AdjustAllVolume(float amount, string which)
    {
        if (which == "BGM")
        {
            foreach (Sound s in BGMsounds)
            {
                if (amount >= 1)
                {
                    amount = 1;
                }
                else if (amount <= 0)
                {
                    amount = 0;
                }
                s.volume = amount;
                s.source.volume = s.volume;
            }
        }
        else if (which == "SFX")
        {
            foreach (Sound s in SFXsounds)
            {
                if (amount >= 1)
                {
                    amount = 1;
                }
                else if (amount <= 0)
                {
                    amount = 0;
                }
                s.volume = amount;
                s.source.volume = s.volume;
            }
        }
        else { Debug.Log("NoSound"); }
    }

}
