﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class SCR_LevelSettings : MonoBehaviour
{
    [Header("Settings")]
    public AudioClip levelMusic;
    public float musicVolume = 1;
    public AudioClip levelAmbience;
    public float ambienceVolume = 1;

    [Header("References")]
    public AudioSource musicSource;
    public AudioSource ambienceSource;

    SCR_ObjectReferenceManager objectRefs;

    void Start()
    {
        objectRefs = SCR_ObjectReferenceManager.Instance;

        objectRefs.levelSettings = this;
        objectRefs.music = musicSource;
        objectRefs.ambience = ambienceSource;

        musicSource.clip = levelMusic;
        musicSource.volume = musicVolume;
        musicSource.Play();
        ambienceSource.clip = levelAmbience;
        ambienceSource.volume = ambienceVolume;
        ambienceSource.Play();
    }

    void Update()
    {
        
    }
}