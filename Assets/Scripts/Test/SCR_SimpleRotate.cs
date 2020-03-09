using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_SimpleRotate : MonoBehaviour
{
    [Tooltip("Degrees to rotate per second (in EulerAngles)")]
    public Vector3 rotation;
    [Tooltip("Rotate in world or local space")]
    public Space rotationSpace = Space.Self;

	void FixedUpdate()
    {
        transform.Rotate(rotation * Time.deltaTime, rotationSpace);
    }
}
