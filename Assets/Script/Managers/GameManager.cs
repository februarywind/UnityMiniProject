using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PoolManager poolManager;
    public TankState tankState;
    public TankControl tankControl;
    public GameUI gameUI;
    public MapRePosition mapRePosition;

    public int[] timer;
    Coroutine TimerCoroutine;
    WaitForSeconds oneSecond = new WaitForSeconds(1);
    public bool GameState = true;
    int killCount = 0;

    [SerializeField] GameObject GameOverUI;
    private void Awake()
    {
        instance = this;
        TimerCoroutine = StartCoroutine(Timer());
    }
    IEnumerator Timer()
    {
        while (GameState)
        {
            yield return oneSecond;
            timer[0]++;
            if (timer[0] == 60)
            {
                timer[0] = 0;
                timer[1]++;
            }
            gameUI.TimeUpdate(timer[0], timer[1]);
            if (timer[1] == 10)
            {
                GameOver(true);
                break;
            }
            if (timer[1] % 2 == 0)
            {
                poolManager.MonsterSpwanTimeUpdate(timer[1] / 2);
            }
        }
    }
    public void KillCount()
    {
        killCount++;
        gameUI.KillCounterUpdate(killCount);
    }
    public void GameOver(bool clear)
    {
        GameState = false;
        GameOverUI.SetActive(true);
        AudioManager.instance.StopAllSfx();
        AudioManager.instance.StopBgm();
        AudioManager.instance.PlaySfx(!clear ? SfxAudio.GameOver : SfxAudio.GameClear);
        AudioManager.instance.sfxPlaying = false;
        Time.timeScale = 0;
    }
    public void ToTitle()
    {
        LoadManager.instance.ToTitleScene();
    }
    public void GameReStart()
    {
        LoadManager.instance.ToGameScene();
    }
}
