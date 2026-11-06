using UnityEngine;
using UnityEngine.Events;

public class OnTriggerAction : MonoBehaviour
{
    public string triggerTagName = "MainCamera";
    public UnityEvent onEnter;
    public UnityEvent onExit;

    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag(triggerTagName))
        {
            onEnter.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(triggerTagName))
        {
            onExit.Invoke();
        }
    }
}
