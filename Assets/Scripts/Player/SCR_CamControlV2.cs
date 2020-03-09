using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_CamControlV2 : MonoBehaviour
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

    #region Local Variables
    float yaw, pitch;
    Vector3 smoothVelocity, currentRotation;
    #endregion

    private void Awake()
    {
        controls = new InputControls();
    }

    private void LateUpdate()
    {
        Vector2 input = controls.Player.Camera.ReadValue<Vector2>();
        yaw += input.x * variables.camSensitivity;
        pitch -= input.y * variables.camSensitivity;
        pitch = Mathf.Clamp(pitch, variables.camPitchMinMax.x, variables.camPitchMinMax.y);

        currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref smoothVelocity, variables.camTurnSpeed);
        transform.eulerAngles = currentRotation;

        transform.position = lookTarget.position - transform.forward * variables.camDistanceMinMax.y;
    }

}
