using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Player : MonoBehaviour
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
	CharacterController charController;
	#endregion

	#region Local Variables
	bool running;
	float currentSpeed;
	float rotationVelocity, speedVelocity;
	Transform camT;
	float yVelocity;
	#endregion

	private void Awake()
	{
		//REF:
		camT = GameObject.FindGameObjectWithTag("PlayerCam").transform;
		controls = new InputControls();
		charController = GetComponent<CharacterController>();

		//Functions:
		InputActions();
	}

	private void Update()
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
			transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref rotationVelocity, ModifiedSmoothTime(variables.playerTurnSpeed));
		}

		//Check if Running & Set Speed Accordingly:
		float targetSpeed = (running ? variables.runSpeed : variables.walkSpeed) * input.magnitude;
		currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedVelocity, ModifiedSmoothTime(variables.Acceleration));

		//Add Gravity:
		yVelocity += Time.deltaTime * variables.gravity;

		//Move the Player:
		Vector3 velocity = transform.forward * currentSpeed + Vector3.up * yVelocity;
		charController.Move(velocity * Time.deltaTime);

		//Reset Y Velocity When Grounded:
		if(charController.isGrounded)
		{
			yVelocity = 0;
		}
	}

	void Jump()
	{
		if(charController.isGrounded)
		{
			float jumpVelocity = Mathf.Sqrt(-2 * variables.gravity * variables.jumpHeight);
			yVelocity = jumpVelocity;
		}
	}

	float ModifiedSmoothTime(float smoothTime)
	{
		if (charController.isGrounded)
			return smoothTime;
		if (variables.airControlPercent == 0)
			return float.MaxValue;
		return smoothTime / variables.airControlPercent;
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
