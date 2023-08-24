using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject startMenu;
    [SerializeField] private GameObject gameMenu;
    [SerializeField] private GameObject nextLevelMenu;
    [SerializeField] private GameObject endGameMenu;
    [SerializeField] private GameObject gameInformation;
    [SerializeField] private GameObject defeateMenu;
    [SerializeField] private GameObject defeateResumeMenu;

    [SerializeField] private Grid grid;

    [SerializeField] private LevelRule[] levelRules;

    private int correctLevel;

    public void CloseAllMenu()
    {
        mainMenu.SetActive(false);
        gameMenu.SetActive(false);
        startMenu.SetActive(false);
        nextLevelMenu.SetActive(false);
        endGameMenu.SetActive(false);
        gameInformation.SetActive(false);
        defeateMenu.SetActive(false);
        defeateResumeMenu.SetActive(false);
    }

    public void OpenMainMenu()
    {
        CloseAllMenu();
        mainMenu.SetActive(true);
    }

    public void OpenStartMenu()
    {
        CloseAllMenu();
        startMenu.SetActive(true);
    }

    public void OpenLevel(int level)
    {
        OpenInformationStatus();
        correctLevel = level;
        if (correctLevel < levelRules.Length)
        {
            grid.SetLevelRule(levelRules[correctLevel]);
        }
        else
        {
            OpenEndGameMenu();
        }
    }

    public void OpenEndGameMenu()
    {
        CloseAllMenu();
        endGameMenu.SetActive(true);
    }

    public void OpenNextLevel()
    {
        OpenLevel(correctLevel + 1);
    }

    public void OpenNextLevelMenu()
    {
        CloseAllMenu();
        nextLevelMenu.SetActive(true);
    }

    public void RestartLevel()
    {
        OpenLevel(correctLevel);
    }

    public void OpenDefeatMenu()
    {
        CloseAllMenu();
        defeateMenu.SetActive(true);
    }

    public void OpenDefeatResumeMenu()
    {
        CloseAllMenu();
        defeateResumeMenu.SetActive(true);
    }

    public bool GetGameInformationStatus()
    {
        return gameInformation.activeSelf;
    }

    public void OpenInformationStatus()
    {
        CloseAllMenu();
        gameInformation.SetActive(true);
    }

    public void OpenGameMenu()
    {
        CloseAllMenu();
        gameMenu.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
