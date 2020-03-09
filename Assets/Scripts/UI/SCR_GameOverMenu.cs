using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SCR_GameOverMenu : MonoBehaviour
{
    public Button retryButton;
    public Transform selectorImage;

    CanvasGroup cGroup;
    SCR_ObjectReferenceManager objectRefs;
    SCR_VarManager varManager;
    EventSystem eventSystem;


    void Start()
    {
        cGroup = GetComponent<CanvasGroup>();
        objectRefs = SCR_ObjectReferenceManager.Instance;
        varManager = SCR_VarManager.Instance;
        eventSystem = EventSystem.current;

        objectRefs.gameOverMenu = cGroup;
    }

    void Update()
    {
        if (varManager.gameOver && Time.timeScale == 0 && !cGroup.interactable)
            OpenGameOverMenu();

        if (cGroup.interactable && eventSystem.currentSelectedGameObject != null)
        {
            selectorImage.position = eventSystem.currentSelectedGameObject.transform.position;
        }
    }

    public void OpenGameOverMenu()
    {
        cGroup.alpha = 1;
        cGroup.interactable = true;
        cGroup.blocksRaycasts = true;

        retryButton.Select();
    }

    public void Respawn()
    {
        StartCoroutine(DelayRespawn());
    }

    IEnumerator DelayRespawn()
    {
        yield return new WaitForSecondsRealtime(.1f);

        varManager.gameOver = false;

        // change this!!
        varManager.currentHealth = 3;

        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        StartCoroutine(DelayQuit());
    }

    IEnumerator DelayQuit()
    {
        yield return new WaitForSecondsRealtime(.1f);

        Destroy(varManager.gameObject);

        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f;

        SceneManager.LoadScene(0);
    }
}
