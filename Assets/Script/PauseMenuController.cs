using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    public GameObject pausePanel;

    private bool isPaused;

    private void Start()
    {
        Time.timeScale = 1f;

        pausePanel.SetActive(false);
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
}
