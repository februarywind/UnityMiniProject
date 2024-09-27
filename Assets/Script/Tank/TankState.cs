using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] GameObject buttons;
    [SerializeField] Button[] button;
    int[] ints = { 1, 2, 3 };
    private void Awake()
    {
        hp = maxHp;
        button = buttons.GetComponentsInChildren<Button>();
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
            EXP -= levelUpEXP;
            levelUpEXP = (int)(levelUpEXP * 1.2);
            LevelUp();
        }
        GameManager.instance.gameUI.EXPUpdate(levelUpEXP, EXP);
    }
    void LevelUp()
    {
        buttons.SetActive(true);
        int temp = 0;
        foreach (var item in button)
        {
            switch (ints[temp])
            {
                case 1:
                    item.onClick.AddListener(() => TurretDmgUpdate(1));
                    break;
                case 2:
                    item.onClick.AddListener(() => TurretFireSpeedUpdate(1));
                    break;
                case 3:
                    item.onClick.AddListener(() => TurretRangeUpdate(1));
                    break;
            }
            temp++;
        }
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
