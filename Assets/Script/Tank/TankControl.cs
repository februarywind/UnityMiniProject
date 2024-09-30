using System.Collections;
using UnityEngine;

public class TankControl : MonoBehaviour
{
    [SerializeField] float rotateSpeed;
    [SerializeField] Transform Cannon;
    public GameObject FirePoint;
    [SerializeField] GameObject ExplosionPoint;
    Transform FirePoinTr;
    Rigidbody rigid;
    float YRot;
    bool cannonLoad = true;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        FirePoinTr = FirePoint.transform;
        FirePoint.SetActive(false);
    }
    private void Update()
    {
        Move();
        CannonRot();
    }
    void Move()
    {
        float ZMove = Input.GetAxisRaw("Vertical");
        transform.Translate(Vector3.forward * ZMove * GameManager.instance.tankState.moveSpeed * Time.deltaTime);

        YRot += Input.GetAxisRaw("Horizontal") * rotateSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(new Vector3(0, YRot, 0));
        Camera.main.transform.position = new Vector3(transform.position.x, 25, transform.position.z - 13);

    }
    void CannonRot()
    {
        if (Input.GetMouseButton(1))
        {
            FirePoint.SetActive(true);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
                FirePoinTr.position = hit.point;

            FirePoinTr.position = new Vector3(FirePoinTr.position.x, 1.5f, FirePoinTr.position.z);

            Quaternion targetRotation = Quaternion.LookRotation(FirePoinTr.position - Cannon.transform.position);

            Quaternion rotateAmount = Quaternion.RotateTowards(Cannon.transform.rotation, targetRotation, 2 * rotateSpeed * Time.deltaTime);

            Cannon.transform.rotation = rotateAmount;
            if (Input.GetKeyDown(KeyCode.Space) && cannonLoad)
            {
                cannonLoad = false;
                Instantiate(ExplosionPoint, FirePoinTr.position, Quaternion.Euler(Vector3.zero));
                StartCoroutine(CannonCoolTime());
            }
        }
        else
        {
            FirePoint.SetActive(false);

            Quaternion targetRotation = Quaternion.LookRotation(transform.forward);

            Quaternion rotateAmount = Quaternion.RotateTowards(Cannon.transform.rotation, targetRotation, 2 * rotateSpeed * Time.deltaTime);

            Cannon.transform.rotation = rotateAmount;
        }
    }
    IEnumerator CannonCoolTime()
    {
        yield return GameManager.instance.tankState._cannonCoolTime;
        cannonLoad = true;
    }
}
