using UnityEngine;
using TMPro;

// Counts the length of the current run and displays it as minutes and seconds.
public class GameTimer : MonoBehaviour
{
    public TMP_Text timerText;

    private float elapsedTime;

    private void Start()
    {
        elapsedTime = 0f;
        UpdateTimerDisplay();
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        UpdateTimerDisplay();
    }

    private void UpdateTimerDisplay()
    {
        int minutes =
            Mathf.FloorToInt(elapsedTime / 60f);

        int seconds =
            Mathf.FloorToInt(elapsedTime % 60f);

        timerText.text =
            minutes.ToString("00") +
            ":" +
            seconds.ToString("00");
    }
}
