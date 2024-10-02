using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum SfxAudio
{
    Turret, Move, Explosion, LevelUp, GameOver, GameClear
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("BGM")]
    [SerializeField] GameObject bgmObj;
    [SerializeField] AudioClip bgmClips;
    [Range(0,1)] public float bgmVolum;
    [SerializeField] AudioSource bgmPlayer;

    [Header("SFX")]
    [SerializeField] GameObject sfxObj;
    [SerializeField] AudioClip[] sfxClips;
    [SerializeField] [Range(0,1)] float sfxVolum;
    public AudioSource[] sfxPlayer;

    public bool sfxPlaying = true;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        Init();
    }
    void Init()
    {
        bgmPlayer = bgmObj.AddComponent<AudioSource>();
        bgmPlayer.volume = bgmVolum;
        bgmPlayer.clip = bgmClips;
        bgmPlayer.loop = true;
        bgmPlayer.Play();

        sfxPlayer = new AudioSource[sfxClips.Length];
        for (int i = 0; i < sfxClips.Length; i++)
        {
            sfxPlayer[i] = sfxObj.AddComponent<AudioSource>();
            sfxPlayer[i].clip = sfxClips[i];
            sfxPlayer[i].volume = sfxVolum;
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
    public void StartBgm()
    {
        bgmPlayer.Play();
    }
    public void StopBgm()
    {
        bgmPlayer.Stop();
    }
    public void StopAllSfx()
    {
        foreach (var item in sfxPlayer)
        {
            item.Stop();
        }
    }
    public void CangeBgmVolume(Slider slider)
    {
        bgmPlayer.volume = slider.value;
    }
    public void CangeSfxVolume(Slider slider)
    {
        for (int i = 0; i < sfxClips.Length; i++)
        {
            sfxPlayer[i].volume = slider.value;
        }
    }
}
