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
    float VerticalSmoothVelocity;
    bool useCameraControl = true;
    float yawCenterSmoothVelocity, pitchCenterSmoothVelocity;
    IEnumerator centerCamera;
    #endregion

    #region References
    SCR_VarManager varManager;
    #endregion


    private void Awake()
    {
        controls = new InputControls();
        transform.SetParent(null);
    }

    private void Start()
    {
        SCR_ObjectReferenceManager.Instance.playerCamera = GetComponent<Camera>();
        varManager = SCR_VarManager.Instance;

        // Set starting position and distance
        targetDistance = variables.camDistanceMinMax.y;
        targetPosition = lookTarget.position + Vector3.up * variables.camVerticalOffset;
        transform.position = targetPosition - transform.forward * targetDistance;

        // Set starting rotation
        pitch = variables.camDefaultPitch;
        yaw = lookTarget.eulerAngles.y;
        currentRotation = new Vector3(pitch, yaw, 0);
        transform.eulerAngles = currentRotation;
    }


    private void FixedUpdate()
	{
        if(lookTarget == null)
        {
            useCameraControl = false;
            Destroy(gameObject);
        }

        // Set position camera will be looking at
        float targetYpos = Mathf.SmoothDamp(targetPosition.y, lookTarget.position.y + variables.camVerticalOffset, ref VerticalSmoothVelocity, 0.1f, Mathf.Infinity, Time.fixedDeltaTime);
        targetPosition = new Vector3(lookTarget.position.x, targetYpos, lookTarget.position.z);

        if (useCameraControl)
        {
            // Set yaw and pitch based on input
            Vector2 input = controls.Player.Camera.ReadValue<Vector2>();
            yaw += input.x * variables.camSensitivity * Time.fixedDeltaTime;
            pitch -= input.y * variables.camSensitivity * Time.fixedDeltaTime;
            pitch = Mathf.Clamp(pitch, variables.camPitchMinMax.x, variables.camPitchMinMax.y);

            currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref RotationSmoothVelocity, variables.camTurnSpeed, Mathf.Infinity, Time.fixedDeltaTime);

            transform.eulerAngles = currentRotation;
        }

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

    private void Update()
    {
        if (controls.Player.CenterCamera.triggered
            && lookTarget != null)
        {
            // If Center camera coroutine is already running, stop it and start a new one
            if (centerCamera != null)
            {
                StopCoroutine(centerCamera);
            }

            centerCamera = CenterCamera();
            StartCoroutine(centerCamera);
        }
    }
    /// <summary>
    /// This function runs in FixedUpdate. 
    /// It prevents camera from clipping into solid objects, by moving it closer to player if necessary.
    /// </summary>
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

    IEnumerator CenterCamera()
    {
        useCameraControl = false;

        float newYaw = lookTarget.eulerAngles.y;

        //while (yaw - newYaw >= 360)
        //{
        //    newYaw += 360;
        //}

        //while (yaw - newYaw <= -360)
        //{
        //    newYaw -= 360;
        //}

        while (Mathf.Abs(transform.eulerAngles.y - newYaw) > 1)
        {
            yield return new WaitForFixedUpdate();

            pitch = Mathf.SmoothDampAngle(pitch, variables.camDefaultPitch, ref pitchCenterSmoothVelocity, .1f, Mathf.Infinity, Time.fixedDeltaTime);
            yaw = Mathf.SmoothDampAngle(yaw, newYaw, ref yawCenterSmoothVelocity, .1f, Mathf.Infinity, Time.fixedDeltaTime);

            transform.eulerAngles = new Vector3(pitch, yaw, 0);
        }

        pitch = variables.camDefaultPitch;
        yaw = newYaw;
        currentRotation = new Vector3(pitch, yaw, 0);

        useCameraControl = true;
        centerCamera = null;
    }
}
