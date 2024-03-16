using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Script to send invoke events when different phases of the gameplay loop end
* so other scripts can respond accordingly.
*
* Main Menu -> [UpgradeScreen -> GameplayScreen -> Repeat]
*/
public class PhaseManager : MonoBehaviour
{
    public static Action MainMenuPhaseEnded = delegate { };
    public static Action UpgradePhaseEnded = delegate { };
    public static Action SurvivedGameplayPhase = delegate { };
    public static Action DiedGameplayPhase = delegate { };

    public void EndMainMenuPhase()
    {
        MainMenuPhaseEnded.Invoke();
    }
    public void EndUpgradePhase()
    {
        UpgradePhaseEnded.Invoke();
    }
    public void EndGameplayPhaseSurvive()
    {
        Time.timeScale = 1;
        SurvivedGameplayPhase.Invoke();
    }
    public void EndGameplayPhaseDied()
    {
        Time.timeScale = 1;
        DiedGameplayPhase.Invoke();
    }
}
