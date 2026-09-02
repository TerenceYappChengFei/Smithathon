using System.Collections;
using UnityEngine;

public class MainMenuFlow : MonoBehaviour
{
    public CanvasGroup onboardingPanel;
    public CanvasGroup logoGroup;
    public CanvasGroup titlePanel;
    public CanvasGroup mainMenuPanel;

    public float fadeDuration = 1f;
    public float logoDisplayDuration = 2f;
    public float blackScreenDuration = 0.5f;

    private bool transitionRunning;

    private void Start()
    {
        Time.timeScale = 1f;

        SetPanel(onboardingPanel, 1f, false);
        SetPanel(logoGroup, 0f, false);
        SetPanel(titlePanel, 0f, false);
        SetPanel(mainMenuPanel, 0f, false);

        StartCoroutine(PlayBootSequence());
    }

    private IEnumerator PlayBootSequence()
    {
        transitionRunning = true;

        //Fade both logos in together
        yield return FadeCanvasGroup(
            logoGroup,
            0f,
            1f
        );

        //Keep both logos visible
        yield return new WaitForSecondsRealtime(
            logoDisplayDuration
        );

        //Fade the logos back to black
        yield return FadeCanvasGroup(
            logoGroup,
            1f,
            0f
        );

        //Remain completely black for a moment
        yield return new WaitForSecondsRealtime(
            blackScreenDuration
        );

        //Place the title above the onboarding screen
        titlePanel.transform.SetAsLastSibling();

        //Fade the title screen in
        yield return FadeCanvasGroup(
            titlePanel,
            0f,
            1f
        );

        SetPanel(titlePanel, 1f, true);

        //The title now covers the onboarding screen
        onboardingPanel.gameObject.SetActive(false);

        transitionRunning = false;
    }

    public void ShowMainMenu()
    {
        if (transitionRunning)
        {
            return;
        }

        StartCoroutine(ShowMainMenuSequence());
    }

    private IEnumerator ShowMainMenuSequence()
    {
        transitionRunning = true;

        SetPanel(titlePanel, 1f, false);
        SetPanel(mainMenuPanel, 0f, false);

        mainMenuPanel.transform.SetAsLastSibling();

        //Fade the menu in over the title
        yield return FadeCanvasGroup(
            mainMenuPanel,
            0f,
            1f
        );

        SetPanel(mainMenuPanel, 1f, true);
        titlePanel.gameObject.SetActive(false);

        transitionRunning = false;
    }

    private IEnumerator FadeCanvasGroup(
        CanvasGroup group,
        float startAlpha,
        float endAlpha
    )
    {
        float elapsedTime = 0f;

        group.alpha = startAlpha;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.unscaledDeltaTime;

            group.alpha = Mathf.Lerp(
                startAlpha,
                endAlpha,
                elapsedTime / fadeDuration
            );

            yield return null;
        }

        group.alpha = endAlpha;
    }

    private void SetPanel(
        CanvasGroup group,
        float alpha,
        bool allowInput
    )
    {
        group.gameObject.SetActive(true);
        group.alpha = alpha;
        group.interactable = allowInput;
        group.blocksRaycasts = allowInput;
    }
}
