using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_PauseMenu : MonoBehaviour
{
    void Start()
    {
        SCR_ObjectReferenceManager.Instance.pauseMenu = GetComponent<CanvasGroup>();
    }

    public void ResumeGame()
    {

    }
}
