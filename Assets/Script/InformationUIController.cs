using UnityEngine;
using UnityEngine.UI;

public class InformationUIController : MonoBehaviour
{
    [Header("Lore")]
    public GameObject lorePanel;
    public Toggle neverShowAgainToggle;

    [Header("Recipe Book")]
    public bool recipeBookPracticeOnly = true;

    public GameObject recipeButton;
    public GameObject recipePanel;
    public GameObject[] recipePages;
    public Button previousPageButton;
    public Button nextPageButton;

    private const string LorePreferenceKey = "NeverShowLore";
    private int currentPage;

    private void Start()
    {
        recipePanel.SetActive(false);

        if (recipeBookPracticeOnly)
        {
            recipeButton.SetActive(
                GameModeSettings.isPracticeMode
            );
        }
        else
        {
            recipeButton.SetActive(true);
        }


        bool shouldHideLore =
            PlayerPrefs.GetInt(LorePreferenceKey, 0) == 1;

        if (shouldHideLore)
        {
            lorePanel.SetActive(false);
        }
        else
        {
            OpenLore();
        }
    }

    private void OpenLore()
    {
        lorePanel.SetActive(true);
        lorePanel.transform.SetAsLastSibling();

        Time.timeScale = 0f;
    }

    public void CloseLore()
    {
        if (neverShowAgainToggle.isOn)
        {
            PlayerPrefs.SetInt(LorePreferenceKey, 1);
            PlayerPrefs.Save();
        }

        lorePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void OpenRecipe()
    {
        recipePanel.SetActive(true);
        recipePanel.transform.SetAsLastSibling();

        currentPage = 0;
        ShowCurrentPage();
    }


    public void CloseRecipe()
    {
        recipePanel.SetActive(false);
    }

    public void NextPage()
    {
        if (currentPage < recipePages.Length - 1)
        {
            currentPage++;
            ShowCurrentPage();
        }
    }

    public void PreviousPage()
    {
        if (currentPage > 0)
        {
            currentPage--;
            ShowCurrentPage();
        }
    }

    private void ShowCurrentPage()
    {
        for (int i = 0; i < recipePages.Length; i++)
        {
            recipePages[i].SetActive(i == currentPage);
        }

        previousPageButton.interactable =
            currentPage > 0;

        nextPageButton.interactable =
            currentPage < recipePages.Length - 1;
    }
}
