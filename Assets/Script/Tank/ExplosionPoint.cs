using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionPoint : MonoBehaviour
{
    SphereCollider sphereCollider;
    private void Awake()
    {
        sphereCollider = GetComponent<SphereCollider>();
    }
    private void OnEnable()
    {
        sphereCollider.enabled = true;
        StartCoroutine(ColliderUnenable());
    }
    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<MonsterState>().HitDmage(GameManager.instance.tankState.CannonDmg);
    }
    IEnumerator ColliderUnenable()
    {
        yield return new WaitForSeconds(0.1f);
        sphereCollider.enabled = false;
    }
}
