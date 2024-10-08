using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [SerializeField] GameObject[] prefabs;
    [SerializeField] Transform[] spwanPoint;
    List<GameObject> pools = new List<GameObject> {};
    [SerializeField] float[] spwanTime;
    WaitForSeconds MonsterSpwanTime = new WaitForSeconds(3);
    Coroutine Coroutine;
    private void Start()
    {
        Coroutine = StartCoroutine(SpwanStart());
    }
    public void CreateObj(int index)
    {
        foreach (var item in pools)
        {
            if(!item.activeSelf)
            {
                item.gameObject.transform.position = spwanPoint[Random.Range(0, spwanPoint.Length)].position;
                item.gameObject.SetActive(true);
                return;
            }
        }
        pools.Add(Instantiate(prefabs[index], spwanPoint[Random.Range(0, spwanPoint.Length)].position, transform.rotation));
    }
    IEnumerator SpwanStart()
    {
        while (true)
        {
            yield return MonsterSpwanTime;
            CreateObj(0);
        }
    }
    public void MonsterSpwanTimeUpdate(int value)
    {
        MonsterSpwanTime = new WaitForSeconds(spwanTime[value]);
    }
}
