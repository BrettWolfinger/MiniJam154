using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    void OnEnable()
    {
        PhaseManager.MainMenuPhaseEnded += LoadUpgradeScreen;
        PhaseManager.UpgradePhaseEnded += StartDay;
        PhaseManager.SurvivedGameplayPhase += LoadUpgradeScreen;
        PhaseManager.DiedGameplayPhase += LoadMainMenu;
    }

    void OnDisable()
    {
        PhaseManager.MainMenuPhaseEnded -= LoadUpgradeScreen;
        PhaseManager.UpgradePhaseEnded -= StartDay;
        PhaseManager.SurvivedGameplayPhase -= LoadUpgradeScreen;
        PhaseManager.DiedGameplayPhase -= LoadMainMenu;
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("TitleScreen");
    }

    public void LoadUpgradeScreen()
    {
        SceneManager.LoadScene("UpgradeScreen");
    }
    
    public void StartDay()
    {
        SceneManager.LoadScene("GameplayScreen");
    }
}
