using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SCR_CollectiblesHUD : MonoBehaviour
{
    public TextMeshProUGUI nutsText, seedsText;
    SCR_VarManager varManager;

    void Start()
    {
        varManager = SCR_VarManager.Instance;
    }

    void Update()
    {
        nutsText.SetText(varManager.currentGoldNuts.ToString());
        seedsText.SetText(varManager.currentSeeds.ToString());
    }
}
