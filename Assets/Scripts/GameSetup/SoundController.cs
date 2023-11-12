using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    [Header("Volume control")]
    [SerializeField] public AudioMixer mixer;
    [SerializeField] AudioMixerGroup musicMixerGroup, effectsMixerGroup, masterMixerGroup;
    float sfxVolume, musicVolume;
    bool isMasterVolumeEnabled;
    [Header("All sounds")]  
    [SerializeField] Sound[] sounds;
    [Header("UI")]
    [SerializeField] Slider sfxSlider;
    [SerializeField] Slider musicSlider;
    [SerializeField] Image pauseSoundImage;


    private void Awake()
    {
        foreach (Sound s in sounds)
        {                 
            s.audioSource = gameObject.AddComponent<AudioSource>();
            s.audioSource.clip = s.clip;
            s.audioSource.volume = s.volume;
            s.audioSource.pitch = s.pitch;
            s.audioSource.loop = s.loop;
            s.audioSource.playOnAwake = s.isPlayOnAwake;
            switch (s.typeOfSound)
            {
                case TypeOfSound.Music:
                    s.audioSource.outputAudioMixerGroup = musicMixerGroup;
                    break;
                case TypeOfSound.SFX:
                    s.audioSource.outputAudioMixerGroup = effectsMixerGroup;
                    break;
            }
        }
        isMasterVolumeEnabled = true;
    }

    void Start()
    {

        //effectsSlider.value = Progress.Instance.playerInfo.effectsVolume;
        //musicSlider.value = Progress.Instance.playerInfo.musicVolume;
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.audioSource.Play();
    }
    public void StopPlay(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.audioSource.Stop();
    }

    public Sound GetSound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        return s;
    }
    //По кнопке
    public void MakeClickSound()
    {
        Play("ButtonClick");
    }

    public void SetSFXLevel()
    {      
        mixer.SetFloat("SFXVolume", Mathf.Log10(sfxSlider.value) * 40);
        sfxVolume = sfxSlider.value;
        //Progress.Instance.playerInfo.effectsVolume = effectsSlider.value;
    }
    public void SetMusicLevel()
    {
        mixer.SetFloat("MusicVolume", Mathf.Log10(musicSlider.value) * 40);       
        musicVolume = musicSlider.value;
        //Progress.Instance.playerInfo.musicVolume = musicSlider.value;
    }

    public void ToggleMasterVolume()
    {
        isMasterVolumeEnabled = !isMasterVolumeEnabled;
        if (isMasterVolumeEnabled)
        {
            mixer.SetFloat("MusicVolume", 0);
            mixer.SetFloat("SFXVolume", 0);
        }
        else
        {
            mixer.SetFloat("MusicVolume", -80);
            mixer.SetFloat("SFXVolume", -80);
            sfxSlider.value = 0;
            musicSlider.value = 0;
        }

    }
    //По кнопке Закрыть
    public void SaveVolumeSetting()
    {
        //YandexSDK.Save();
    }

    private void OnApplicationFocus(bool focus)
    {       
        Silence(!focus);       
    }
    private void OnApplicationPause(bool pause)
    {
        Silence(pause);
    }
    void Silence(bool silence)
    {
        AudioListener.pause = silence;
    }

    public void MuteGame()
    {
        AudioListener.volume = 0;
    }
    public void UnmuteGame()
    {
        AudioListener.volume = 1;
    }

}
