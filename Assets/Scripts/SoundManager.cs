using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;
    public static SoundManager instance
    {
        get
        {
            if (_instance == null) Debug.LogError("Sound Manager is null");
            return _instance;
        }
    }    
    [SerializeField] AudioClip buildingBuilded = null;
    [SerializeField] AudioClip buildingDestroyed = null;
    [SerializeField] AudioClip upgradeSound = null;


    AudioSource audio;
    private void Awake()
    {
        _instance=this;
    }
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    public void PlayBuildSound()
    {
        audio.PlayOneShot(buildingBuilded);
    }

    public void PlayDestroySound()
    {
        audio.PlayOneShot(buildingDestroyed);
    }
    public void PlayUpgradeSound()
    {
        audio.PlayOneShot(upgradeSound);
    }
}

