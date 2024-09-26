using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankState : MonoBehaviour
{
    [SerializeField] float maxHp;
    [SerializeField] float hp;
    [SerializeField] GameObject TurretParticle;
    public float TurretDmg;
    public float CannonDmg;
    public float CannonCoolTime;
    private void Awake()
    {
        hp = maxHp;
    }
    public void TurretDmgUpdate(float value)
    {
        TurretDmg += value;
    }
    public void TurretFireSpeedUpdate(float value)
    {
        foreach (var item in TurretParticle.GetComponentsInChildren<ParticleSystem>())
        {
            var emission = item.emission;
            emission.rateOverTime = item.emission.rateOverTime.constant + value;
        }

    }
    public void CannonDmgUpdate()
    {

    }
    public void CannonCoolTimeUpdate()
    {

    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Monster")
        {
            hp -= 10 * Time.deltaTime;
            GameManager.instance.gameUI.HpChange(maxHp, hp);
        }
    }
}
