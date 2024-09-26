using UnityEngine;

public class TankControl : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float rotateSpeed;
    [SerializeField] Transform Cannon;
    [SerializeField] GameObject FirePoint;
    Transform FirePoinTr;
    Rigidbody rigid;
    float YRot;
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
        transform.Translate(Vector3.forward * ZMove * moveSpeed * Time.deltaTime);

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

            //(시작값, 목표값, 회전 속도)를 인자로 받아 회전 값을 연산해주는 메서드
            Quaternion rotateAmount = Quaternion.RotateTowards(Cannon.transform.rotation, targetRotation, 2 * rotateSpeed * Time.deltaTime);

            //회전값 적용
            Cannon.transform.rotation = rotateAmount;
        }
        else
        {
            FirePoint.SetActive(false);

            Quaternion targetRotation = Quaternion.LookRotation(transform.forward);

            //(시작값, 목표값, 회전 속도)를 인자로 받아 회전 값을 연산해주는 메서드
            Quaternion rotateAmount = Quaternion.RotateTowards(Cannon.transform.rotation, targetRotation, 2 * rotateSpeed * Time.deltaTime);

            //회전값 적용
            Cannon.transform.rotation = rotateAmount;

        }
    }
}
