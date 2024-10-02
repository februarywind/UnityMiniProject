using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;

public class TankControl : MonoBehaviour
{
    [SerializeField] float rotateSpeed;
    [SerializeField] Transform Cannon;
    public GameObject FirePoint;
    [SerializeField] GameObject ExplosionPoint;
    [SerializeField] Slider Reload;
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
        if (!cannonLoad)
        {
            Reload.value += 1 * Time.deltaTime / GameManager.instance.tankState.cannonCoolTime;
        }
    }
    void Move()
    {

        YRot += Input.GetAxisRaw("Horizontal") * rotateSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(new Vector3(0, YRot, 0));
        Camera.main.transform.position = new Vector3(transform.position.x, 25, transform.position.z - 13);

        float ZMove = Input.GetAxisRaw("Vertical");
        if (ZMove == 0)
        {
            AudioManager.instance.StopSfx(SfxAudio.Move);
            return;
        }
        transform.Translate(Vector3.forward * ZMove * GameManager.instance.tankState.moveSpeed * Time.deltaTime);
        AudioManager.instance.PlaySfx(SfxAudio.Move);
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
                Reload.value = 0;
                cannonLoad = false;
                Instantiate(ExplosionPoint, FirePoinTr.position, Quaternion.Euler(Vector3.zero));
                AudioManager.instance.PlaySfx(SfxAudio.Explosion);
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
