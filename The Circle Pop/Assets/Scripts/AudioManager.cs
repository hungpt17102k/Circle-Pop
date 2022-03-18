using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null) {
            instance = this;
        }
        else {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (var s in sounds)
        {
            s.source =  gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    private void Start() {
        if(MenuManager.musicIsOff == false) {
            Play("Theme");
        }
    }

    public void Play(string name) {
        Sound sound = Array.Find(sounds, s => s.name == name);
        if(sound == null) {
            Debug.LogWarning("Sound " + name + " not found!");
            return;
        }
        sound.source.Play();
    }
}
