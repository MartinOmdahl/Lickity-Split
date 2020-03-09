using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class SCR_InactivityReset : MonoBehaviour
{
    public float secondsToReset = 60;
    public float secondsToShowTimer = 40;
    public TextMeshProUGUI timerText;

    float idleTime;
    bool startedReset;

    CanvasGroup cGroup;
    Gamepad gamepad;

    void Start()
    {
        cGroup = GetComponent<CanvasGroup>();
        gamepad = Gamepad.current;

        cGroup.alpha = 0;
    }

    void Update()
    {
        if (gamepad.wasUpdatedThisFrame || Input.anyKeyDown)
            idleTime = 0;
        else
            idleTime += Time.deltaTime;

        //timerText.color = Color.Lerp(Color.white, Color.red, colorLerpValue);

        if (idleTime >= secondsToShowTimer && !startedReset)
        {
            timerText.SetText("Game will reset in " + ((int)secondsToReset - (int)idleTime) + " seconds due to inactivity." +
                "\nPress any button to cancel.");

            cGroup.alpha = Mathf.Lerp(cGroup.alpha, 1, 0.5f * Time.deltaTime);
        }
        else if(!startedReset)
        {
            cGroup.alpha = 0;
        }

        if (idleTime >= secondsToReset && !startedReset)
        {
            StartCoroutine(ResetGame());
        }
    }

    IEnumerator ResetGame()
    {
        startedReset = true;

        timerText.fontSize *= 0.5f;
        timerText.SetText("I don't feel so good");
        yield return new WaitForSeconds(0.3f);

        print("Game was reset due to " + secondsToReset + " seconds of inactivity.");
        Destroy(SCR_VarManager.Instance.gameObject);
        SceneManager.LoadScene(0);
    }
}
