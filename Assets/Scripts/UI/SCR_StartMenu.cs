using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class SCR_StartMenu : MonoBehaviour
{
    public Transform buttonHighlighter;

    EventSystem eventSystem;


    private void Start()
    {
        eventSystem = EventSystem.current;
    }

    private void Update()
    {
        HighlightSelectedButton();
    }

    void HighlightSelectedButton()
    {
        if (eventSystem.currentSelectedGameObject != null)
            buttonHighlighter.position = Vector3.Lerp(buttonHighlighter.position, eventSystem.currentSelectedGameObject.transform.position, 50 * Time.deltaTime);
    }

    public void StartGameButton()
    {
        SceneManager.LoadScene(1);
    }

    public void CreditsButton()
    {
        SceneManager.LoadScene("SCE_Credits");
    }

    public void QuitGameButton()
    {
        Application.Quit();
    }


}
