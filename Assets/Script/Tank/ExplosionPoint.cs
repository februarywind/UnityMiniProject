using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionPoint : MonoBehaviour
{
    SphereCollider sphereCollider;
    private void Awake()
    {
        sphereCollider = GetComponent<SphereCollider>();
        sphereCollider.radius = GameManager.instance.tankState.cannonRange;
    }
    private void OnEnable()
    {
        sphereCollider.enabled = true;
        StartCoroutine(ColliderUnEnable());
        Destroy(gameObject, 5);
    }
    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<MonsterState>().HitDmage(GameManager.instance.tankState.cannonDmg);
    }
    IEnumerator ColliderUnEnable()
    {
        yield return new WaitForSeconds(0.1f);
        sphereCollider.enabled = false;
    }
}
