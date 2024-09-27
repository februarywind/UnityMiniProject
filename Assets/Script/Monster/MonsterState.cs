using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterState : MonoBehaviour
{
    [SerializeField] float maxhp;
    [SerializeField] float ap;
    [SerializeField] float moveSpeed;

    Rigidbody rigid;
    Transform player;
    float hp;
    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
        rigid = GetComponent<Rigidbody>();
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
    }
}
