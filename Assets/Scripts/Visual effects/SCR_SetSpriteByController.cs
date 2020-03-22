using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SCR_SetSpriteByController : MonoBehaviour
{
    // Update to include Switch in future
    public Sprite xboxSprite, playStationSprite, switchSprite;

    Image image;
    Gamepad gamepad;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void Start()
    {
        gamepad = Gamepad.current;

        if (gamepad != null)
        {
            print(gamepad.GetType());
            if (gamepad.GetType().BaseType.ToString() == "UnityEngine.InputSystem.DualShock.DualShockGamepad")
            {
                // If controller is PlayStation, set texture to PlayStation version
                image.sprite = playStationSprite;
            }
            else if (gamepad.GetType().ToString() == "UnityEngine.InputSystem.Switch.SwitchProControllerHID")
            {
                // If controller is Switch, set texture to Switch version
                image.sprite = switchSprite;
            }
            else
            {
                // Xbox version is default
                image.sprite = xboxSprite;
            }
        }
    }
}
