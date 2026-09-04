using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject settingsPanel;


    private bool isPaused;

    private void Start()
    {
        Time.timeScale = 1f;

        pausePanel.SetActive(false);
        settingsPanel.SetActive(false);
        isPaused = false;

    }

    public void PauseGame()
    {
        pausePanel.SetActive(true);

        //Places the pause panel above all other UI objects
        pausePanel.transform.SetAsLastSibling();

        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ContinueGame()
    {
        Time.timeScale = 1f;

        pausePanel.SetActive(false);
        isPaused = false;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().name
        );
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void OpenSettings()
    {
        pausePanel.SetActive(false);

        settingsPanel.SetActive(true);
        settingsPanel.transform.SetAsLastSibling();
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);

        pausePanel.SetActive(true);
        pausePanel.transform.SetAsLastSibling();
    }

}
