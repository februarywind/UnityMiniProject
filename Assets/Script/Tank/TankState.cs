using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankState : MonoBehaviour
{
    [SerializeField] float maxHp;
    [SerializeField] float hp;
    private void Awake()
    {
        hp = maxHp;
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
