using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SCR_SetSpriteByController : MonoBehaviour
{
    // Update to include Switch in future
    public Sprite xboxSprite, playStationSprite;

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
            string gamepadType = gamepad.GetType().BaseType.ToString();
            if (gamepadType == "UnityEngine.InputSystem.DualShock.DualShockGamepad")
            {
                // If controller is PlayStation, set texture to PlayStation version
                image.sprite = playStationSprite;
            }
            else
            {
                // Xbox version is default
                image.sprite = xboxSprite;
            }
        }
    }
}
