using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_CamControl : MonoBehaviour
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
	public Transform lookTarget;
	InputControls controls;
    #endregion

    #region Public variables
    [Tooltip("Layers camera can collide with")]
    public LayerMask clippingDetectMask;
    #endregion

    #region Local Variables
    float yaw, pitch;
    float targetDistance;
    float distanceSmoothVelocity;
    Vector3 RotationSmoothVelocity, currentRotation;
    Vector3 targetPosition;
    #endregion

    #region References
    SCR_VarManager varManager;
    #endregion


    private void Awake()
    {
        controls = new InputControls();
        transform.SetParent(null);


        //currentRotation = transform.eulerAngles;
        // [yaw & pitch = (starting camera angle)]
    }

    private void Start()
    {
        SCR_ObjectReferenceManager.Instance.playerCamera = GetComponent<Camera>();
        varManager = SCR_VarManager.Instance;

        targetDistance = variables.camDistanceMinMax.y;
        targetPosition = lookTarget.position + Vector3.up * variables.camVerticalOffset;

        transform.position = targetPosition - transform.forward * targetDistance;
    }


    float svel;
    private void FixedUpdate()
	{
        float targetYpos = Mathf.SmoothDamp(targetPosition.y, lookTarget.position.y + variables.camVerticalOffset, ref svel, 0.1f, Mathf.Infinity, Time.fixedDeltaTime);

        // Set position camera will be looking at
        targetPosition = new Vector3(lookTarget.position.x, targetYpos, lookTarget.position.z);

        // Set yaw and pitch based on input
		Vector2 input = controls.Player.Camera.ReadValue<Vector2>();
		yaw += input.x * variables.camSensitivity * Time.fixedDeltaTime;
		pitch -= input.y * variables.camSensitivity * Time.fixedDeltaTime;
		pitch = Mathf.Clamp(pitch, variables.camPitchMinMax.x, variables.camPitchMinMax.y);

		currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref RotationSmoothVelocity, variables.camTurnSpeed, Mathf.Infinity, Time.fixedDeltaTime);
		transform.eulerAngles = currentRotation;

        AvoidClipping();
        float smoothDistance = Mathf.SmoothDamp(Vector3.Distance(transform.position, targetPosition), targetDistance, ref distanceSmoothVelocity, Time.fixedDeltaTime, 60);

		transform.position = targetPosition - transform.forward * smoothDistance;




        // Disable camera movement if player dies. Be careful with this, may need to refactor in the future...
        if (varManager.gameOver)
        {
            transform.SetParent(null);
            this.enabled = false;
        }
	}

    void AvoidClipping()
    {
        // Do a raycast from player to camera to find any obstructions

        bool hit = false;
        Vector3 clipRayDirection = (transform.position - targetPosition).normalized;
        if (Physics.Raycast(targetPosition, clipRayDirection, out RaycastHit clipHit, variables.camDistanceMinMax.y, clippingDetectMask, QueryTriggerInteraction.Ignore))
        {
            hit = true;
        }
        else
        {
            targetDistance = variables.camDistanceMinMax.y;
        }

        // Do a raycast from camera to player

        float hitPointsDistance = variables.camDistanceMinMax.y;
        if (Physics.Raycast(transform.position, -clipRayDirection, out RaycastHit clipHitB, Vector3.Distance(transform.position, targetPosition), clippingDetectMask, QueryTriggerInteraction.Ignore))
        {
            // compare hit points of raycasts to find width of obstruction
            hitPointsDistance = Vector3.Distance(clipHitB.point, clipHit.point);
        }

        // If obstruction is wide enough, move camera closer to player.
        if (hitPointsDistance > 0.5f && hit)
            targetDistance = Vector3.Distance(targetPosition, clipHit.point) - 0.5f;

        targetDistance = Mathf.Clamp(targetDistance, variables.camDistanceMinMax.x, variables.camDistanceMinMax.y);
    }
}
