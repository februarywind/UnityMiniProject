using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TankState : MonoBehaviour
{
    [SerializeField] GameObject TurretParticle;

    [SerializeField] float maxHp;
    [SerializeField] float hp;
    public float turretDmg;
    public float turretRange = 10f;
    public float cannonDmg;
    public float cannonCoolTime;
    public WaitForSeconds _cannonCoolTime = new WaitForSeconds(5f);
    public float cannonRange = 4.5f;
    public float moveSpeed;

    [SerializeField] int EXP;
    [SerializeField] int levelUpEXP = 100;
    [SerializeField] int upgradeSelectCount = 3;
    [SerializeField] GameObject buttons;
    [SerializeField] Button[] button;
    private void Awake()
    {
        hp = maxHp;
        button = buttons.GetComponentsInChildren<Button>();
    }
    public void TankMaxHpUpdate(float value)
    {
        maxHp += value;
        hp += value;
        GameManager.instance.gameUI.HpChange(maxHp, hp);
    }
    public void TankSpeedUpdate(float value)
    {
        moveSpeed += value;
    }
    public void TurretDmgUpdate(float value)
    {
        turretDmg += value;
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
        turretRange += value;
    }
    public void CannonDmgUpdate(float value)
    {
        cannonDmg += value;
    }
    public void CannonCoolTimeUpdate(float value)
    {
        cannonCoolTime -= value;
        _cannonCoolTime = new WaitForSeconds(cannonCoolTime);
    }
    public void CannonRangeUpdate(float value)
    {
        cannonRange += value;
        GameManager.instance.tankControl.FirePoint.transform.localScale = new Vector3(cannonRange, cannonRange, cannonRange) * 2;
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
        if (buttons == null)
            return;
        AudioManager.instance.StopAllSfx();
        AudioManager.instance.PlaySfx(SfxAudio.LevelUp);
        AudioManager.instance.sfxPlaying = false;
        buttons.SetActive(true);
        for (int i = 0; i < button.Length - upgradeSelectCount; i++)
        {
            int temp = Random.Range(0, button.Length);
            if (!button[temp].gameObject.activeSelf)
            {
                i--;
                continue;
            }
            button[temp].gameObject.SetActive(false);
        }
        Time.timeScale = 0;
    }
    public void LevelUpEndEvent()
    {
        for (int i = 0; i < button.Length; i++)
            button[i].gameObject.SetActive(true);
        buttons.SetActive(false);
        AudioManager.instance.sfxPlaying = true;
        Time.timeScale = 1;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Monster")
        {
            hp -= 10 * Time.deltaTime;
            GameManager.instance.gameUI.HpChange(maxHp, hp);
            if (hp <= 0)
            {
                GameManager.instance.GameOver(false);
                gameObject.GetComponent<BoxCollider>().enabled = false;
            }
        }
    }
}
