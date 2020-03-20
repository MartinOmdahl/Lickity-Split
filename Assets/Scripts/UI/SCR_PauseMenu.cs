using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SCR_PauseMenu : MonoBehaviour
{
    public Button firstSelectedButton;
    public Transform selectorImage;

    [Header("Runtime variables")]
    public bool ResumePressed;

    CanvasGroup cGroup;
    EventSystem eventSystem;
    Mouse mouse;

    private void Awake()
    {
        cGroup = GetComponent<CanvasGroup>();

        ResumePressed = false;
    }

    void Start()
    {
        SCR_ObjectReferenceManager.Instance.pauseMenu = this;
        eventSystem = EventSystem.current;
        mouse = Mouse.current;
    }

    private void Update()
    {
        if (cGroup.interactable)
            MenuUpdate();
    }

    /// <summary>
    /// An Update function that only runs when pause menu is open.
    /// </summary>
    void MenuUpdate()
    {
        // Move selector image to highlight current selected button
        if (eventSystem.currentSelectedGameObject != null)
            selectorImage.position = Vector3.Lerp(selectorImage.position, eventSystem.currentSelectedGameObject.transform.position, 50 * Time.unscaledDeltaTime);

        // If mouse input received, unlock mouse control
        if (mouse.wasUpdatedThisFrame)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void OpenPauseMenu()
    {
        // Activate canvas group
        cGroup.alpha = 1;
        cGroup.interactable = true;
        cGroup.blocksRaycasts = true;

        // Select button
        firstSelectedButton.Select();
        selectorImage.position = firstSelectedButton.transform.position;
    }

    public void ClosePauseMenu()
    {
        // Deactivate canvas group
        cGroup.alpha = 0;
        cGroup.interactable = false;
        cGroup.blocksRaycasts = false;

        // Disable mouse control
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    /// <summary>
    /// Function called by UI button
    /// </summary>
    public void ResumeButton()
    {
        // Signal to Game Manager that game should be unpaused
        ResumePressed = true;
    }

    /// <summary>
    /// Function called by UI button
    /// </summary>
    public void QuitButton()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f;

        SceneManager.LoadScene(0);
    }
}
