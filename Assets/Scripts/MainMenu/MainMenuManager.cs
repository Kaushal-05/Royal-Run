
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("UI Panels")]
    [SerializeField] private GameObject mainMenuContent;
    [SerializeField] private GameObject settingsPanel;

    private void Start()
    {
        Time.timeScale = 1f;

        mainMenuContent.SetActive(true);
        settingsPanel.SetActive(false);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("GamePlay");
    }

    public void OpenSettings()
    {
        mainMenuContent.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
        mainMenuContent.SetActive(true);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Royal Run...");
        Application.Quit();
    }
}
