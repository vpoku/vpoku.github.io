using UnityEngine;
using UnityEngine.Events;

public class SequenceAction : MonoBehaviour
{
    [Tooltip("The actions to execute, in order.")]
    public UnityEvent[] steps;

    [Tooltip("Seconds to wait before each step fires. " +
             "Step 0 with a delay of 0 fires immediately. " +
             "Missing delays are treated as 0.")]
    public float[] stepDelays;

    private int currentStep = 0;
    private float elapsedTime = 0f;
    private bool isRunning = false;

    public void StartSequence()
    {
        currentStep = 0;
        elapsedTime = 0f;
        isRunning = steps != null && steps.Length > 0;

        // Fire the first step immediately if its delay is 0.
        if (isRunning)
        {
            FireStepIfReady();
        }
    }

    public void StopSequence()
    {
        isRunning = false;
        currentStep = 0;
        elapsedTime = 0f;
    }

    private void Update()
    {
        if (!isRunning)
        {
            return;
        }

        elapsedTime += Time.deltaTime;
        FireStepIfReady();
    }

    private void FireStepIfReady()
    {
        while (isRunning && currentStep < steps.Length)
        {
            float delay = 0f;

            if (stepDelays != null &&
                currentStep < stepDelays.Length)
            {
                delay = Mathf.Max(0f, stepDelays[currentStep]);
            }

            if (elapsedTime < delay)
            {
                return;
            }

            // Preserve any time beyond the delay.
            elapsedTime -= delay;

            UnityEvent currentEvent = steps[currentStep];
            currentStep++;

            if (currentEvent != null)
            {
                currentEvent.Invoke();
            }

            if (currentStep >= steps.Length)
            {
                isRunning = false;
            }
        }
    }
}
