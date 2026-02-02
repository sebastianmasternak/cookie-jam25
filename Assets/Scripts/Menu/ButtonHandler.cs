using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{

    public GameObject menuPanel;
    public GameObject creditsPanel;
    public GameObject tutorialPanel;
    public Button startButton;
    public Button quitButton;
    public Button showCreditsButton;
    public Button hideCreditsButton;
    public Button showTutorialButton;
    public Button backToMenu;

    public void Start()
    {
        menuPanel.SetActive(true);
        creditsPanel.SetActive(false);
        startButton.onClick.AddListener(StartGame);
        quitButton.onClick.AddListener(QuitGame);
        showCreditsButton.onClick.AddListener(ShowCredits);
        hideCreditsButton.onClick.AddListener(HideCredits);
        backToMenu.onClick.AddListener(hideTutorial);
        showTutorialButton.onClick.AddListener(showTutorial);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("MainGameplayScene");   
    }

    public void QuitGame()
    {
        Application.Quit();   
    }

    public void ShowCredits()
    {
        menuPanel.SetActive(false);
        creditsPanel.SetActive(true);
        tutorialPanel.SetActive(false);
    }

    public void HideCredits()
    {
        menuPanel.SetActive(true);
        creditsPanel.SetActive(false);
        tutorialPanel.SetActive(false);
    }

    public void showTutorial()
    {
        tutorialPanel.SetActive(true);
        creditsPanel.SetActive(false);
        menuPanel.SetActive(false);
    }

     public void hideTutorial()
    {
        tutorialPanel.SetActive(false);
        creditsPanel.SetActive(false);
        menuPanel.SetActive(true);
    }


}
