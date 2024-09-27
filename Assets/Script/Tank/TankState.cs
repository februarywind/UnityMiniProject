using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankState : MonoBehaviour
{
    [SerializeField] GameObject TurretParticle;

    [SerializeField] float maxHp;
    [SerializeField] float hp;
    public float TurretDmg;
    public float TurretRange = 10f;
    public float CannonDmg;
    public float CannonCoolTime;

    [SerializeField] int EXP;
    [SerializeField] int levelUpEXP = 100;
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
    public void TurretRangeUpdate(float value)
    {
        TurretRange += value;
    }
    public void CannonDmgUpdate(float value)
    {
        CannonDmg += value;
    }
    public void CannonCoolTimeUpdate()
    {

    }
    public void EXPUpdate(int value)
    {
        EXP += value;
        if (EXP >= levelUpEXP)
        {
            LevelUp();
        }
        GameManager.instance.gameUI.EXPUpdate(levelUpEXP, EXP);
    }
    void LevelUp()
    {
        EXP -= levelUpEXP;
        levelUpEXP = (int)(levelUpEXP * 1.2);
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
