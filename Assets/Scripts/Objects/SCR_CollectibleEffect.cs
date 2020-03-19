using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_CollectibleEffect : MonoBehaviour
{
    public float timeUntilDestroyed = 3;

    void Start()
    {
        StartCoroutine(Despawn()); 

        // [Play particle effect?]
    }

    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(timeUntilDestroyed);
        Destroy(gameObject);
    }
}
