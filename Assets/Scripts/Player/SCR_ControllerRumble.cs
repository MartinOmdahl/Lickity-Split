using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SCR_ControllerRumble : MonoBehaviour
{
    // Priority level of currently running rumble coroutine
    int currentRumblePriority;
    Gamepad gamepad;

    private void OnEnable()
    {
        gamepad = Gamepad.current;
        gamepad.ResumeHaptics();
    }

    private void OnDisable()
    {
        gamepad.ResetHaptics();
    }

    /// <summary>
    /// Rumble the controller for a specified amount of time.
    /// A rumble can only be interrupted by another rumble with higher priority.
    /// </summary>
    /// <param name="priority"> A higher priority means the rumble will interrupt any lower-priority rumbles. </param>
    /// <param name="duration"> Duration of the rumble in seconds. </param>
    /// <param name="leftMotorSpeed"> Speed of the left (low-freq) motor </param>
    /// <param name="rightMotorSpeed"> Speed of the right (high-freq) motor </param>
    public void StartRumble(int priority, float duration, float leftMotorSpeed, float rightMotorSpeed)
    {
        // If rumble with higher priority isn't already running, start new rumble
        if (priority >= currentRumblePriority)
        {
            currentRumblePriority = priority;
            StopAllCoroutines();
            StartCoroutine(Rumble(duration, leftMotorSpeed, rightMotorSpeed));
        }
    }

    IEnumerator Rumble(float duration, float leftMotorSpeed, float rightMotorSpeed)
    {
        yield return null;

        // Start rumble motors
        gamepad.SetMotorSpeeds(leftMotorSpeed, rightMotorSpeed);

        // Wait for designated duration
        for(float time = 0; time < duration; time += Time.deltaTime)
        {
            yield return null;
        }

        // Stop rumble motors
        gamepad.SetMotorSpeeds(0, 0);

        currentRumblePriority = 0;
    }

}
