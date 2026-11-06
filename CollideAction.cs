using UnityEngine;
using UnityEngine.Events;

public class CollideAction : MonoBehaviour
{
    public string colliderTagName = "Grabbable";
    public UnityEvent onCollision;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(colliderTagName))
        {
            onCollision.Invoke();
        }
    }
}
