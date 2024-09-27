using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterState : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float maxhp;
    [SerializeField] float hp;
    [SerializeField] float ap;
    [SerializeField] float moveSpeed;
    [SerializeField] int giveEXP;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
    }
    private void OnEnable()
    {
        hp = maxhp;
    }
    private void Update()
    {
        Move();
        if (hp <= 0)
        {
            gameObject.SetActive(false);
        }
    }
    void Move()
    {
        Vector3 dir = player.position - transform.position;
        transform.Translate(dir.normalized * moveSpeed * Time.deltaTime, Space.World);
    }
    public void HitDmage(float dmg)
    {
        hp -= dmg;
    }

    private void OnParticleCollision(GameObject other)
    {
        HitDmage(GameManager.instance.tankState.TurretDmg);
    }
    private void OnDisable()
    {
        GameManager.instance.KillCount();
        GameManager.instance.tankState.EXPUpdate(giveEXP);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Plane")
        {
            transform.parent = collision.transform;
        }
    }
}
