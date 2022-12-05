using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ToolTip : MonoBehaviour
{
    public TMP_Text toolTipText;
    public GameControllerAnimated gameController;

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (gameController.spawnedPlayerScript.playerInput.currentControlScheme == "Controller")
        {
            toolTipText.text = "Press Right Trigger to Respawn";
        }
        else
        {
            toolTipText.text = "Press the R Key to Respawn";

        }
    }
}
