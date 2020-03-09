using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SCR_RepositionPlayerObject : MonoBehaviour
{
    private void OnEnable()
    {
        controls.Enable();
    }
    private void OnDisable()
    {
        controls.Disable();
    }

    public float horizontalSpeed = 1;
    public float verticalSpeed = 1;
    public GameObject playerPrefab;

    InputControls controls;

    private void Awake()
    {
        controls = new InputControls();
    }

    private void FixedUpdate()
    {
        HorizontalMovement();
        VerticalMovement();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton6))
        {
            GoToOrigin();
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton0))
        {
            InstantiatePlayer();
        }
    }

    void HorizontalMovement()
    {
        Vector3 movementVector = controls.Player.Movement.ReadValue<Vector2>();

        transform.Translate(movementVector.x * horizontalSpeed, 0, movementVector.y * horizontalSpeed);
    }

    void VerticalMovement()
    {
        Vector3 movementVector = controls.Player.Camera.ReadValue<Vector2>();

        transform.Translate(0, movementVector.y * verticalSpeed, 0);
    }

    void GoToOrigin()
    {
        transform.position = Vector3.zero;
    }

    void InstantiatePlayer()
    {
        Instantiate(playerPrefab, transform.position, playerPrefab.transform.rotation);
        Destroy(gameObject);
    }
}
