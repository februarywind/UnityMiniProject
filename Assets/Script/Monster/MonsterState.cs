using System;
using System.Collections;
using UnityEngine;
[Serializable]
public struct LevelStet
{
    public float maxHp;
    public float ap;
    public float moveSpeed;
    public int giveEXP;
}

public class MonsterState : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float maxhp;
    [SerializeField] float hp;
    [SerializeField] float ap;
    [SerializeField] float moveSpeed;
    [SerializeField] int giveEXP;

    [SerializeField] GameObject[] MonsterModel;
    [SerializeField] LevelStet[] levelStets;

    [SerializeField] BoxCollider boxCollider;

    Coroutine DeadCoroutine;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
    }
    private void OnEnable()
    {
        foreach (var item in MonsterModel)
        {
            item.SetActive(false);
        }
        int level = GameManager.instance.timer[1] / 2;

        MonsterModel[level].SetActive(true);

        moveSpeed = levelStets[level].moveSpeed;
        giveEXP = levelStets[level].giveEXP;
        maxhp = levelStets[level].maxHp;
        ap = levelStets[level].ap;

        hp = maxhp;
    }
    private void Update()
    {
        Move();
        if (hp <= 0 && DeadCoroutine == null)
        {
            DeadCoroutine = StartCoroutine(Dead());
        }
    }
    void Move()
    {
        transform.LookAt(player.transform.position);
        transform.Translate(transform.forward * moveSpeed * Time.deltaTime, Space.World);
    }
    public void HitDmage(float dmg)
    {
        hp -= dmg;
    }

    private void OnParticleCollision(GameObject other)
    {
        HitDmage(GameManager.instance.tankState.turretDmg);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Plane")
        {
            transform.parent = collision.transform;
        }
    }
    IEnumerator Dead()
    {
        GameManager.instance.KillCount();
        GameManager.instance.tankState.EXPUpdate(giveEXP);
        boxCollider.enabled = false;
        yield return new WaitForSeconds(0.5f);
        boxCollider.enabled = true;
        gameObject.SetActive(false);
        DeadCoroutine = null;
    }
}
