using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SfxAudio
{
    Turret, Move, Explosion, LevelUp
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("BGM")]
    [SerializeField] GameObject bgmObj;
    [SerializeField] AudioClip bgmClips;
    [SerializeField] [Range(0,1)] float bgmVolum;
    [SerializeField] AudioSource bgmPlayer;

    [Header("SFX")]
    [SerializeField] GameObject sfxObj;
    [SerializeField] AudioClip[] sfxClips;
    [SerializeField] [Range(0,1)] float sfxVolum;
    public AudioSource[] sfxPlayer;

    public bool sfxPlaying = true;
    private void Awake()
    {
        instance = this;
        Init();
    }
    void Init()
    {
        bgmPlayer = bgmObj.AddComponent<AudioSource>();
        bgmPlayer.volume = bgmVolum;
        bgmPlayer.clip = bgmClips;
        bgmPlayer.Play();

        sfxPlayer = new AudioSource[sfxClips.Length];
        for (int i = 0; i < sfxClips.Length; i++)
        {
            sfxPlayer[i] = sfxObj.AddComponent<AudioSource>();
            sfxPlayer[i].clip = sfxClips[i];
            sfxPlayer[i].volume = sfxVolum;
            sfxPlayer[i].loop = false;
        }
    }
    public void PlaySfx(SfxAudio sfx)
    {
        if (sfxPlayer[(int)sfx].isPlaying || !sfxPlaying) return;
        sfxPlayer[(int)sfx].Play();
    }
    public void StopSfx(SfxAudio sfx)
    {
        if (!sfxPlayer[(int)sfx].isPlaying) return;
        sfxPlayer[(int)sfx].Stop();
    }
    public void StopAllSfx()
    {
        foreach (var item in sfxPlayer)
        {
            item.Stop();
        }
    }
}
