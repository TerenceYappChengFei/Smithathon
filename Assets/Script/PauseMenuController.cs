using UnityEngine;
using UnityEngine.SceneManagement;

// Controls pausing, settings, confirmation panels, and scene navigation.
public class PauseMenuController : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject settingsPanel;
    public GameObject quitConfirmationPanel;


    private bool isPaused;

    private void Start()
    {
        Time.timeScale = 1f;

        pausePanel.SetActive(false);
        settingsPanel.SetActive(false);
        quitConfirmationPanel.SetActive(false);
        isPaused = false;

    }

    // Time scale zero freezes gameplay physics and timers.
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

        if (PersistentMusic.instance != null)
        {
            PersistentMusic.instance.PlayMainMusic();
        }

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().name
        );
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;

        if (PersistentMusic.instance != null)
        {
            PersistentMusic.instance.PlayMainMusic();
        }

        SceneManager.LoadScene("MainMenu");
    }

    public void OpenQuitConfirmation()
    {
        quitConfirmationPanel.SetActive(true);
        quitConfirmationPanel.transform.SetAsLastSibling();
    }

    public void CloseQuitConfirmation()
    {
        quitConfirmationPanel.SetActive(false);
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
