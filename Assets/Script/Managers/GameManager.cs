using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        }
    }
    public void KillCount()
    {
        killCount++;
        gameUI.KillCounterUpdate(killCount);
    }
}
