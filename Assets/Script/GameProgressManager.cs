using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum FailureReason
{
    WrongOrder,
    OrderTimeout
}
public class GameProgressManager : MonoBehaviour
{
    public int reputation = 50;
    public int successfulOrders;
    public int failedOrders;
    public int score;

    public bool practiceMode;

    public Slider reputationBar;
    public TMP_Text reputationText;
    public int difficultyLevel = 1;
    public OrderManager orderManager;
    public TMP_Text difficultyText;
    public Image reputationFill;
    public TMP_Text scoreText;
    public GameObject gameOverPanel;
    public GameObject closedDownEnding;
    public GameObject overwhelmedEnding;
    public GameOverAnimation gameOverAnimation;
    public TMP_Text finalScoreText;

    [Header("Mode UI")]
    public GameObject reputationUI;
    public GameObject difficultyUI;
    public GameObject practiceModeLabel;


    public float gameOverDelay = 1f; // Delay before showing the game over panel so won't feel too sudden

    private bool gameOverStarted;

    private void Start()
    {
        practiceMode = GameModeSettings.isPracticeMode;

        UpdateModeUI();
        UpdateReputationUI();
        UpdateScoreUI();
        UpdateDifficulty();

        gameOverPanel.SetActive(false);
        closedDownEnding.SetActive(false);
        overwhelmedEnding.SetActive(false);
        gameOverStarted = false;

    }

    private void UpdateModeUI()
    {
        reputationUI.SetActive(!practiceMode);
        difficultyUI.SetActive(!practiceMode);
        practiceModeLabel.SetActive(practiceMode);
    }

    public void RegisterSuccess(int patienceBonus)
    {
        successfulOrders++;

        score += 100 + patienceBonus;
        UpdateScoreUI();

        if (!practiceMode)
        {
            ChangeReputation(5);
        }

        UpdateDifficulty();

    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text =
                "Score: " + score;
        }
    }

    public void RegisterFailure(FailureReason failureReason)
    {
        if (gameOverStarted)
        {
            return;
        }

        failedOrders++;

        if (!practiceMode)
        {
            ChangeReputation(-10);

            if (reputation <= 0)
            {
                gameOverStarted = true;
                StartCoroutine(
                    ShowGameOverAfterDelay(failureReason)
                );
            }
        }
    }

    private IEnumerator ShowGameOverAfterDelay(
    FailureReason failureReason
)
    {
        yield return new WaitForSeconds(gameOverDelay);

        gameOverPanel.SetActive(true);
        gameOverPanel.transform.SetAsLastSibling();
        finalScoreText.text = "Score: " + score;
        gameOverAnimation.PlayAnimation();
        if (PersistentMusic.instance != null)
        {
            PersistentMusic.instance.PlayGameOverMusic();
        }

        if (PersistentMusic.instance != null)
        {
            PersistentMusic.instance.PlayGameOverMusic();
        }



        if (failureReason == FailureReason.WrongOrder)
        {
            closedDownEnding.SetActive(true);
            overwhelmedEnding.SetActive(false);
        }
        else
        {
            closedDownEnding.SetActive(false);
            overwhelmedEnding.SetActive(true);
        }

        Time.timeScale = 0f;
    }



    private void ChangeReputation(int amount)
    {
        reputation += amount;
        reputation = Mathf.Clamp(reputation, 0, 100);

        UpdateReputationUI();
    }

    private void UpdateReputationUI()
    {
        reputationBar.value = reputation;

        if (reputationText != null)
        {
            reputationText.text =
                "Reputation: " + reputation;
        }
        //use >= 50 if want start as green
        if (reputation > 50)
        {
            reputationFill.color = Color.green;
        }
        else if (reputation > 25)
        {
            reputationFill.color = Color.yellow;
        }
        else
        {
            reputationFill.color = Color.red;
        }

    }

    private void UpdateDifficulty()
    {
        if (practiceMode)
        {
            difficultyLevel = 1;
        }
        else if (successfulOrders >= 9)
        {
            difficultyLevel = 4;
        }
        else if (successfulOrders >= 6)
        {
            difficultyLevel = 3;
        }
        else if (successfulOrders >= 3)
        {
            difficultyLevel = 2;
        }
        else
        {
            difficultyLevel = 1;
        }

        if (orderManager != null)
        {
            orderManager.SetDifficulty(difficultyLevel);
        }

        if (difficultyText != null)
        {
            difficultyText.text =
                "Difficulty: " + difficultyLevel;
        }
    }
}
