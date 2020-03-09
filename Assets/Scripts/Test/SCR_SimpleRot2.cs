using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_SimpleRot2 : MonoBehaviour
{
	Rigidbody rb;

    [Tooltip("Degrees to rotate per second (in EulerAngles)")]
    public Vector3 rotation;
    [Tooltip("Rotate in world or local space")]
    public Space rotationSpace = Space.Self;

	private void Awake()
	{
		rb = GetComponent<Rigidbody>();
	}

	void FixedUpdate()
    {
		Quaternion deltaRotation = Quaternion.Euler(rotation * Time.deltaTime);
		rb.MoveRotation(transform.rotation * deltaRotation);
        //transform.Rotate(rotation * Time.deltaTime, rotationSpace);
    }
}
