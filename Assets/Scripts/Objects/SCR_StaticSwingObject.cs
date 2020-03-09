using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_StaticSwingObject : MonoBehaviour
{
    public Transform mesh;

    ConfigurableJoint joint;
    SCR_TongueTarget targetComponent;

    float randomAnimationOffset;
    Vector3 defaultPosition;
    float sineTime;

    void Awake()
    {
        joint = GetComponentInChildren<ConfigurableJoint>();
        targetComponent = GetComponent<SCR_TongueTarget>();

        randomAnimationOffset = Random.Range(-4, 4);
        defaultPosition = transform.position;
    }

    void Update()
    {
        if (targetComponent.isBeingSwung)
        {
            transform.position = Vector3.Lerp(transform.position, defaultPosition, 10 * Time.deltaTime);
        }
        else
        {
            float YPos = defaultPosition.y + Mathf.Sin(Time.time * 4 + randomAnimationOffset) * 0.08f;
            transform.position = new Vector3(defaultPosition.x, YPos, defaultPosition.z);
        }
    }
}
