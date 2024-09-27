using UnityEngine;

public class MapRePosition : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] GameObject[] planes;
    public void RePosition()
    {
        foreach (var item in planes)
        {
            if (player.transform.position.x - item.transform.position.x > 150)
            {
                item.transform.position += new Vector3(300, 0, 0);
            }
            else if (player.transform.position.x - item.transform.position.x < -150)
            {
                item.transform.position += new Vector3(-300, 0, 0);
            }
            if (player.transform.position.z - item.transform.position.z > 150)
            {
                item.transform.position += new Vector3(0, 0, 300);
            }
            else if (player.transform.position.z - item.transform.position.z < -150)
            {
                item.transform.position += new Vector3(0, 0, -300);
            }
        }
        transform.position = player.transform.position;
    }
}
