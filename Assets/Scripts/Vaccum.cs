using UnityEngine;

public class Vaccum : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Object"))
        {

            Debug.Log("obiekt!!");
        }
    }
}
