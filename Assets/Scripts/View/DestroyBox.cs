using UnityEngine;

public class DestroyBox : MonoBehaviour
{

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Entity")) {
            Destroy(other.gameObject);
        }
        
    }

}
