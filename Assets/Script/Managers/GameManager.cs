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
    int killCount = 0;

    [SerializeField] GameObject GameOverUI;
    private void Awake()
    {
        instance = this;
        TimerCoroutine = StartCoroutine(Timer());
    }
    IEnumerator Timer()
    {
        while (true)
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
                GameOver();
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
    public void GameOver()
    {
        GameOverUI.SetActive(true);
        Time.timeScale = 0;
    }
    public void GameReStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }
}
