using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_CollectibleSmall : MonoBehaviour
{
    [Tooltip("How many seeds will player get from this object?")]
    public int seedValue = 1;

    bool useTrigger = true;

    SCR_TongueTarget targetComponent;
    SCR_VarManager varManager;

    void Start()
    {
        targetComponent = GetComponent<SCR_TongueTarget>();
        varManager = SCR_VarManager.Instance;
    }

    void Update()
    {
        // Disable trigger when player starts eating coin, so it won't be collected twice
        if (targetComponent.isBeingAttacked)
        {
            useTrigger = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (useTrigger && other.CompareTag("Player"))
        {
            GetCollected();
            useTrigger = false;
        }
    }

    void GetCollected()
    {
        varManager.currentSeeds += seedValue;
        // [Play effect]
        Destroy(gameObject);
    }
}
