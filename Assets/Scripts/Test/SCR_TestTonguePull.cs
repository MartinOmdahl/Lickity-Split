using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_TestTonguePull : MonoBehaviour
{
    SCR_TongueTarget targetComponent;
    Rigidbody rb;
    Color defaultColor;
    MeshRenderer rend;

    void Awake()
    {
        targetComponent = GetComponent<SCR_TongueTarget>();
        rb = GetComponent<Rigidbody>();
        rend = GetComponentInChildren<MeshRenderer>();
        defaultColor = rend.material.color;
    }

    void Update()
    {
        if (targetComponent.isBeingPulled)
        {
            rend.material.color = Color.Lerp(defaultColor, Color.white, 0.4f);
        }
        else
        {
            rend.material.color = defaultColor;
        }

        if (targetComponent.eventTriggered)
        {
            JumpOnPull();
        }
    }

    void PrintOnPull()
    {
        targetComponent.eventTriggered = false;
        targetComponent.eventOnPull = false;
        targetComponent.enabled = false;

        print("Grab pull event triggered. Time: " + Time.time);
    }

    void JumpOnPull()
    {
        targetComponent.eventTriggered = false;
        targetComponent.eventOnPull = false;
        targetComponent.enabled = false;

        SCR_ObjectReferenceManager.Instance.player.GetComponent<SCR_Tongue>().TerminateGrab();

        rend.material.color = defaultColor;

        rb.isKinematic = false;
        rb.constraints = RigidbodyConstraints.None;
        rb.AddForce(Random.Range(-1, 1), 7, Random.Range(-1, 1), ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!rb.isKinematic)
        {
            Destroy(gameObject);
        }
    }

}
