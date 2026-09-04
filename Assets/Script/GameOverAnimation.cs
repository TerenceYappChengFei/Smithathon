using System.Collections;
using UnityEngine;

// Animates the Game Over overlay, content, final score, and buttons.
public class GameOverAnimation : MonoBehaviour
{
    public CanvasGroup blackPanel;
    public CanvasGroup endings;
    public CanvasGroup score;
    public CanvasGroup retryButton;
    public CanvasGroup mainMenuButton;

    public RectTransform endingsTransform;
    public RectTransform scoreTransform;
    public RectTransform retryButtonTransform;
    public RectTransform mainMenuButtonTransform;

    public float fadeDuration = 0.5f;
    public float floatDuration = 0.5f;
    public float buttonDelay = 0.15f;
    public float floatDistance = 50f;

    private Vector2 endingsPosition;
    private Vector2 scorePosition;
    private Vector2 retryPosition;
    private Vector2 mainMenuPosition;

    private void Awake()
    {
        endingsPosition =
            endingsTransform.anchoredPosition;

        scorePosition =
            scoreTransform.anchoredPosition;

        retryPosition =
            retryButtonTransform.anchoredPosition;

        mainMenuPosition =
            mainMenuButtonTransform.anchoredPosition;
    }

    public void PlayAnimation()
    {
        StopAllCoroutines();
        StartCoroutine(AnimateGameOver());
    }

    // Coroutines play each part of the entrance sequence in order.
    private IEnumerator AnimateGameOver()
    {
        blackPanel.alpha = 0f;
        endings.alpha = 0f;
        score.alpha = 0f;
        retryButton.alpha = 0f;
        mainMenuButton.alpha = 0f;

        retryButton.interactable = false;
        mainMenuButton.interactable = false;

        endingsTransform.anchoredPosition =
            endingsPosition - Vector2.up * floatDistance;

        scoreTransform.anchoredPosition =
            scorePosition - Vector2.up * floatDistance;

        retryButtonTransform.anchoredPosition =
            retryPosition - Vector2.up * floatDistance;

        mainMenuButtonTransform.anchoredPosition =
            mainMenuPosition - Vector2.up * floatDistance;

        yield return FadeIn(
            blackPanel,
            fadeDuration
        );

        yield return FloatIn(
            endings,
            endingsTransform,
            endingsPosition
        );

        yield return FloatIn(
            score,
            scoreTransform,
            scorePosition
        );

        yield return new WaitForSecondsRealtime(buttonDelay);

        yield return FloatIn(
            retryButton,
            retryButtonTransform,
            retryPosition
        );

        retryButton.interactable = true;

        yield return new WaitForSecondsRealtime(buttonDelay);

        yield return FloatIn(
            mainMenuButton,
            mainMenuButtonTransform,
            mainMenuPosition
        );

        mainMenuButton.interactable = true;
    }

    private IEnumerator FadeIn(
        CanvasGroup canvasGroup,
        float duration
    )
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.unscaledDeltaTime;

            float progress =
                Mathf.Clamp01(elapsedTime / duration);

            canvasGroup.alpha = progress;

            yield return null;
        }

        canvasGroup.alpha = 1f;
    }

    private IEnumerator FloatIn(
        CanvasGroup canvasGroup,
        RectTransform objectTransform,
        Vector2 finalPosition
    )
    {
        Vector2 startingPosition =
            finalPosition - Vector2.up * floatDistance;

        float elapsedTime = 0f;

        while (elapsedTime < floatDuration)
        {
            elapsedTime += Time.unscaledDeltaTime;

            float progress =
                Mathf.Clamp01(elapsedTime / floatDuration);

            float easedProgress =
                1f - Mathf.Pow(1f - progress, 3f);

            objectTransform.anchoredPosition =
                Vector2.Lerp(
                    startingPosition,
                    finalPosition,
                    easedProgress
                );

            canvasGroup.alpha = easedProgress;

            yield return null;
        }

        objectTransform.anchoredPosition =
            finalPosition;

        canvasGroup.alpha = 1f;
    }
}
