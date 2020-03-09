using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_PlayerV2 : MonoBehaviour
{
	#region Enable & Disable
	private void OnEnable()
	{
		controls.Enable();
	}
	private void OnDisable()
	{
		controls.Disable();
	}
	#endregion
	#region References
	public SCR_Variables variables;
	InputControls controls;
	Rigidbody rb;
	#endregion

	#region Local Variables
	bool running;
	float currentSpeed;
	float rotationVelocity, speedVelocity;
	Transform camT;
	#endregion

	private void Awake()
	{
		//REF:
		camT = GameObject.FindGameObjectWithTag("PlayerCam").transform;
		controls = new InputControls();
		rb = GetComponent<Rigidbody>();

		//Functions:
		InputActions();
	}

	private void FixedUpdate()
	{
		//Functions
		Move();
	}


	void Move()
	{
		//Get Player Input:
		Vector2 input = controls.Player.Movement.ReadValue<Vector2>();

		//If Input then set Target Rotation & Smoothly Rotate in Degrees:
		if (input != Vector2.zero)
		{
			float targetRotation = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg + camT.eulerAngles.y;
			transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref rotationVelocity, variables.playerTurnSpeed);
		}

		//Check if Running & Set Speed Accordingly:
		float targetSpeed = (running ? variables.runSpeed : variables.walkSpeed) * input.magnitude;
		currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedVelocity, variables.Acceleration);

		//Move the Player:
		Vector3 velocity = transform.forward * currentSpeed;
		rb.velocity = new Vector3(velocity.x, rb.velocity.y, velocity.z);
	}

	void Jump()
	{
		rb.velocity = new Vector3(0, 6, 0);
	}

	void InputActions()
	{
		//Set Jump:
		controls.Player.Jump.performed += ctx => Jump();

		//Check and Set Running:
		if (variables.holdToRun)
			controls.Player.HoldtoRun.performed += ctx => running = !running;
		else
			controls.Player.PresstoRun.performed += ctx => running = !running;
	}
}
