using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource[] musics;
    public AudioSource[] Sfxs;
    public int levelMusic;
    public AudioMixerGroup musicMixer, sfxMixer;

    private void Awake()
    {

        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        //playMusic(levelMusic);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void playMusic(int musicIndex)
    {
        for (int i = 0; i < musics.Length; i++)
        {
            musics[i].Stop();
        }
        musics[Random.Range(0, musics.Length)].Play();
        return;

        if (musicIndex >= musics.Length)
        {
            musics[Random.Range(0, musics.Length)].Play();
        }
        else
        {
            musics[musicIndex].Play();
        }
    }

    public void playSFX(int SfxIndex)
    {
        Sfxs[SfxIndex].Play();
    }

    public void changeMusicVol()
    {
        //musicMixer.audioMixer.SetFloat("MusicVol", UIManager.instance.musicSlider.value);
    }

    public void changeSFXVol()
    {
        //sfxMixer.audioMixer.SetFloat("SFXVol", UIManager.instance.sfxSlider.value);
    }

    public void setMusicLevel()
    {
        musicMixer.audioMixer.SetFloat("MusicVolume", UIManager.instance.musicSlider.value);
    }

    public void setSFXLevel()
    {
        sfxMixer.audioMixer.SetFloat("SFXVolume", UIManager.instance.SFXSlider.value);
    }
}
