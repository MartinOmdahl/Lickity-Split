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
    [Tooltip("Collision layers camera tries to avoid clipping into")]
    public LayerMask clippingDetectMask;
    #endregion

    #region Local Variables
    float yaw, pitch;
    float targetDistance;
	Vector3 RotationSmoothVelocity, currentRotation;
    float distanceSmoothVelocity;
    #endregion

    #region References
    SCR_VarManager varManager;
    #endregion


    private void Awake()
	{
		controls = new InputControls();
        //transform.SetParent(null);

        //currentRotation = transform.eulerAngles;
        // [yaw & pitch = (starting camera angle)]
	}

    private void Start()
    {
        SCR_ObjectReferenceManager.Instance.playerCamera = GetComponent<Camera>();
        varManager = SCR_VarManager.Instance;

        targetDistance = variables.camDistanceMinMax.y;
        transform.position = lookTarget.position - transform.forward * targetDistance;
    }

    private void LateUpdate()
	{
		Vector2 input = controls.Player.Camera.ReadValue<Vector2>();
		yaw += input.x * variables.camSensitivity * Time.deltaTime;
		pitch -= input.y * variables.camSensitivity * Time.deltaTime;
		pitch = Mathf.Clamp(pitch, variables.camPitchMinMax.x, variables.camPitchMinMax.y);

		currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref RotationSmoothVelocity, variables.camTurnSpeed);
		transform.eulerAngles = currentRotation;

        AvoidClipping();

        float smoothDistance = Mathf.SmoothDamp(Vector3.Distance(transform.position, lookTarget.position), targetDistance, ref distanceSmoothVelocity, Time.deltaTime, 60);
		transform.position = lookTarget.position - transform.forward * smoothDistance;




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
        Vector3 clipRayDirection = (transform.position - lookTarget.position).normalized;
        if (Physics.Raycast(lookTarget.position, clipRayDirection, out RaycastHit clipHit, variables.camDistanceMinMax.y, clippingDetectMask, QueryTriggerInteraction.Ignore))
        {
            hit = true;
        }
        else
        {
            targetDistance = variables.camDistanceMinMax.y;
        }

        // Do a raycast from camera to player

        float pointsDistance = variables.camDistanceMinMax.y;
        if (Physics.Raycast(transform.position, -clipRayDirection, out RaycastHit clipHitB, Vector3.Distance(transform.position, lookTarget.position), clippingDetectMask, QueryTriggerInteraction.Ignore))
        {
            // compare hit points of raycasts to find width of obstruction
            pointsDistance = Vector3.Distance(clipHitB.point, clipHit.point);

        }

        // If obstruction is wide enough, move camera closer to player.
        if (pointsDistance > 0.5f && hit)
            targetDistance = Vector3.Distance(lookTarget.position, clipHit.point) - 0.5f;

        targetDistance = Mathf.Clamp(targetDistance, variables.camDistanceMinMax.x, variables.camDistanceMinMax.y);
    }
}
