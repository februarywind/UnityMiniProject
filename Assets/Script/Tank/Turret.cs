using UnityEngine;

public class Turret : MonoBehaviour
{
    public float radius;
    public LayerMask layer;
    public Collider[] colliders;
    public Collider short_enemy;
    public GameObject particle;
    void Update()
    {
        colliders = Physics.OverlapSphere(transform.position, radius, layer);
        //Debug.Log(Physics.OverlapSphereNonAlloc(transform.position, radius, colliders, layer));

        if (colliders.Length > 0)
        {
            float short_distance = 1000;
            foreach (Collider col in colliders)
            {
                float short_distance2 = (transform.position - col.transform.position).sqrMagnitude;
                if (short_distance >= short_distance2)
                {
                    short_distance = short_distance2;
                    short_enemy = col;
                    transform.LookAt(col.transform.position);
                    particle.SetActive(true);
                }
            }
        }
        else
            particle.SetActive(false);

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
