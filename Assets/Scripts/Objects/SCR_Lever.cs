using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Lever : MonoBehaviour
{
    public enum LeverType { Timer, TwoWay, SingleUse}
    public LeverType leverType = LeverType.Timer;
    [Tooltip("Seconds lever waits before flipping back.\n" +
        "Only used if Lever Type is \"Timer\".")]
    public float timerDuration;
    public Transform stickTransform;

    public bool activated;
    bool animationPlaying;

    SCR_TongueTarget targetComponent;
    Animator anim;

    void Awake()
    {
        targetComponent = GetComponentInChildren<SCR_TongueTarget>();
        anim = GetComponent<Animator>();

        targetComponent.transform.eulerAngles = Vector3.zero;
        activated = false;
    }

    void Update()
    {
        if (targetComponent != null
            && targetComponent.eventTriggered)
        {
            switch (leverType)
            {
                case LeverType.Timer:
                    StartCoroutine(TimerActivate());
                    break;
                case LeverType.TwoWay:
                    StartCoroutine(TwoWayActivate());
                    break;
                case LeverType.SingleUse:
                    StartCoroutine(SingleUseActivate());
                    break;
            }
        }
    }

    IEnumerator SingleUseActivate()
    {
        // Disable tongue target
        SCR_ObjectReferenceManager.Instance.player.GetComponent<SCR_Tongue>().TerminateGrab();
        targetComponent.eventTriggered = false;
        targetComponent.enabled = false;

        // Play flip animation
        anim.SetTrigger("FlipA");
        animationPlaying = true;
        yield return new WaitUntil(()=> animationPlaying == false);

        activated = true;

        // Break stick by activating rigidbody
        Rigidbody stickRb = stickTransform.GetComponent<Rigidbody>();
        stickRb.isKinematic = false;
        stickRb.AddRelativeForce(0, 0, -3, ForceMode.Impulse);
        stickRb.angularVelocity = new Vector3(88, 0, 0);

        // Despawn stick
        yield return new WaitForSeconds(3);
        Destroy(stickTransform.gameObject);
    }

    IEnumerator TimerActivate()
    {
        // Disable tongue target
        SCR_ObjectReferenceManager.Instance.player.GetComponent<SCR_Tongue>().TerminateGrab();
        targetComponent.eventTriggered = false;
        targetComponent.enabled = false;

        // Play flip animation A
        anim.SetTrigger("FlipA");
        animationPlaying = true;
        yield return new WaitUntil(()=> animationPlaying == false);
        stickTransform.localEulerAngles = new Vector3(45, 0, 0);

        activated = true;

        // Wait for timer to finish
        float t = 0;
        while (t < timerDuration)
        {
            yield return null;
            float tRoundedDown = Mathf.Floor(t);
            t += Time.deltaTime;

            if(Mathf.Floor(t) > tRoundedDown)
            {
                // [play tick sound every time a whole second has passed]
            }
        }

        // Play flip animation B
        anim.SetTrigger("FlipB");
        animationPlaying = true;
        yield return new WaitUntil(() => animationPlaying == false);

        activated = false;

        // Enable tongue target
        targetComponent.enabled = true;
        targetComponent.transform.eulerAngles = Vector3.zero;
    }

    IEnumerator TwoWayActivate()
    {
        // Disable tongue target
        SCR_ObjectReferenceManager.Instance.player.GetComponent<SCR_Tongue>().TerminateGrab();
        targetComponent.eventTriggered = false;
        targetComponent.targetType = SCR_TongueTarget.TargetType.Deflect;

        // Check which way lever should flip
        if (activated)
        {
            // Play flip animation B
            anim.SetTrigger("FlipB");
            animationPlaying = true;
            yield return new WaitUntil(() => animationPlaying == false);

            activated = false;
        }
        else
        {
            // Play flip animation A
            anim.SetTrigger("FlipA");
            animationPlaying = true;
            yield return new WaitUntil(() => animationPlaying == false);
            stickTransform.localEulerAngles = new Vector3(45, 0, 0);

            activated = true;
        }

        // Enable tongue target
        targetComponent.targetType = SCR_TongueTarget.TargetType.Grab;
        targetComponent.transform.eulerAngles = Vector3.zero;
    }

    public void flipDone()
    {
        // Function called by animation trigger

        animationPlaying = false;
    }
}
