using UnityEngine;

public class MapRePositionTrigger : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameManager.instance.mapRePosition.RePosition();
        }
    }
}
