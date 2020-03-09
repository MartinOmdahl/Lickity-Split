using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_KillPlayerOnTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SCR_VarManager.Instance.currentHealth = 0;
        }
    }
}
