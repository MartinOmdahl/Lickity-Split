using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Temporary script to visualize tongue stretching. Eventually, this wil be done through animation.
public class SCR_TongueStretchProto : MonoBehaviour
{
    public Transform tongueCollider;
    Transform tongueParent;

    void Start()
    {
        tongueParent = transform.parent;
        //transform.SetParent(null);
    }

    void LateUpdate()
    {
        if (SCR_ObjectReferenceManager.Instance.player == null)
        {
            Destroy(gameObject);
        }
        else
        {
            //transform.position = tongueParent.position;
            transform.LookAt(tongueCollider.position, Vector3.up);
            transform.localScale = new Vector3(1, 1, Vector3.Distance(transform.position, tongueCollider.position));
        }
    }
}
