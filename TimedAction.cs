using UnityEngine;
using UnityEngine.Events;

public class TimedAction : MonoBehaviour
{
    [Tooltip("Length of the countdown in seconds.")]
    public float duration = 5f;

    [Tooltip("If checked, the timer starts when the scene loads.")]
    public bool startOnAwake = true;

    [Tooltip("If checked, the timer restarts after completing.")]
    public bool repeat = false; 

    [Tooltip("Number of additional repeats. 0 means repeat forever")]
    public int repeatCount = 0;

    public UnityEvent onTimerComplete;

    private float elapsedTime = 0f;
    private bool isRunning = false;
    private int repeatsCompleted = 0;

    private void Awake()
    {
        if (startOnAwake)
        {
            StartTimer();
        }
    }

    public void StartTimer()
    {
        elapsedTime = 0f;
        repeatsCompleted = 0;
        isRunning = true;
    }

    public void StopTimer()
    {
        isRunning = false;
        elapsedTime = 0f;
    }

    private void Update()
    {
        if (!isRunning)
        {
            return;
        }
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= duration)
        {
            onTimerComplete.Invoke();
            if (repeat && (repeatCount == 0 || repeatsCompleted < repeatCount))
            {
                repeatsCompleted++;
                elapsedTime = 0f;
            }
            else 
            {
                isRunning = false;
            }
        }
    }
}
