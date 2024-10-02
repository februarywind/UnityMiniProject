using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadManager : MonoBehaviour
{
    public static LoadManager instance;
    private void Awake()
    {
        instance = this;
    }
    public void ToGameScene()
    {
        SceneManager.LoadScene("GameScene");
        Init();
    }
    public void ToTitleScene()
    {
        SceneManager.LoadScene("TitleScene");
        Init();
    }
    void Init()
    {
        Time.timeScale = 1;
        AudioManager.instance.sfxPlaying = true;
        AudioManager.instance.StartBgm();
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Loaded scene " + scene.name);
    }
}
