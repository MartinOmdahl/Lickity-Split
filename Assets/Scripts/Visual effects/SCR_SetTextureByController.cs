using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SCR_SetTextureByController : MonoBehaviour
{
    // Update to include Switch in future
    public Texture xboxTexture, playStationTexture;

    MeshRenderer meshRenderer;
    Gamepad gamepad;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        gamepad = Gamepad.current;

        if (gamepad != null)
        {
            string gamepadType = gamepad.GetType().BaseType.ToString();
            if (gamepadType == "UnityEngine.InputSystem.DualShock.DualShockGamepad")
            {
                // If controller is PlayStation, set texture to PlayStation version
                meshRenderer.material.mainTexture = playStationTexture;
            }
            else
            {
                // Xbox version is default
                meshRenderer.material.mainTexture = xboxTexture;
            }
        }
    }
}
