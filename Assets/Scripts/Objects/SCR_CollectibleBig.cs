using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_CollectibleBig : MonoBehaviour
{
    [Tooltip("This Gold Nut's unique ID")]
    public int starIndex;
    public GameObject normalMesh, collectedMesh;

    bool useTrigger = true;
    bool alreadyCollected = false;

    SCR_TongueTarget targetComponent;
    SCR_VarManager varManager;

    void Start()
    {
        targetComponent = GetComponent<SCR_TongueTarget>();
        varManager = SCR_VarManager.Instance;

        // Check if this Gold Nut has been collected before
        foreach(var collIndex in varManager.CollectedGoldNuts)
        {
            if(collIndex == starIndex)
            {
                alreadyCollected = true;
            }
        }

        // Set correct mesh to visible
        if (alreadyCollected)
        {
            normalMesh.SetActive(false);
            collectedMesh.SetActive(true);
        }
        else
        {
            normalMesh.SetActive(true);
            collectedMesh.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (useTrigger && other.CompareTag("Player"))
        {
            StartCoroutine(GetCollected());
            useTrigger = false;
        }
    }

    IEnumerator GetCollected()
    {
        if (!alreadyCollected)
        {
            varManager.currentGoldNuts ++;
            varManager.CollectedGoldNuts.Add(starIndex);
        }

        // [Play effect]
        yield return null;

        Destroy(gameObject);
    }
}
