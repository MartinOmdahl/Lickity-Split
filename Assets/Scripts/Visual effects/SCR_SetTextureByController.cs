using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SCR_SetTextureByController : MonoBehaviour
{
    // Update to include Switch in future
    public Texture xboxTexture, playStationTexture, switchTexture;

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
            if (gamepad.GetType().BaseType.ToString() == "UnityEngine.InputSystem.DualShock.DualShockGamepad")
            {
                // If controller is PlayStation, set texture to PlayStation version
                meshRenderer.material.mainTexture = playStationTexture;
            }
            else if (gamepad.GetType().ToString() == "UnityEngine.InputSystem.Switch.SwitchProControllerHID")
            {
                // If controller is Switch, set texture to Switch version
                meshRenderer.material.mainTexture = switchTexture;
            }
            else
            {
                // Xbox version is default
                meshRenderer.material.mainTexture = xboxTexture;
            }
        }
    }
}
