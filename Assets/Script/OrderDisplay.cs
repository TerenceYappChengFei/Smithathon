using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class OrderDisplay : MonoBehaviour
{
    public Image weaponIcon;
    public Image material1Icon;
    public Image material2Icon;
    public Slider patienceBar;
    public Image patienceFill;

    public CanvasGroup canvasGroup;
    public Image resultGlow;

    public float glowDuration = 0.4f;
    public float slideDuration = 0.5f;
    public float slideInDuration = 0.5f;
    public float slideDistance = 50f;

    public bool hasOrder;
    public bool isResolving; // Pauses the timer while the order is entering, completing, or failing
    public int orderNumber;
    public GameProgressManager gameProgressManager;



    public ItemData requestedWeapon;
    public ItemData requiredMaterial1;
    public ItemData requiredMaterial2;

    private float patienceRemaining;
    private float patienceDuration;

    private RectTransform orderRectTransform;
    private Vector2 startingPosition;

    private void Awake()
    {
        orderRectTransform = GetComponent<RectTransform>();
        startingPosition = orderRectTransform.anchoredPosition;
    }

    private void Update()
    {
        if (!hasOrder || isResolving)
        {
            return;
        }

        patienceRemaining -= Time.deltaTime;

        float patiencePercent =
            patienceRemaining / patienceDuration * 100f;

        patienceBar.value = patiencePercent;
        UpdatePatienceColor(patiencePercent);

        if (patienceRemaining <= 0f)
        {
            FailOrder(FailureReason.OrderTimeout);
        }

    }

    private void UpdatePatienceColor(float patiencePercent)
    {
        if (patiencePercent > 50f)
        {
            patienceFill.color = Color.green;
        }
        else if (patiencePercent > 25f)
        {
            patienceFill.color = Color.yellow;
        }
        else
        {
            patienceFill.color = Color.red;
        }
    }

    public void ShowOrder(
        ItemData weapon,
        ItemData material1,
        ItemData material2,
        float duration
    )
    {
        requestedWeapon = weapon;
        requiredMaterial1 = material1;
        requiredMaterial2 = material2;

        weaponIcon.sprite = weapon.itemIcon;
        material1Icon.sprite = material1.itemIcon;
        material2Icon.sprite = material2.itemIcon;

        patienceDuration = duration;
        patienceRemaining = duration;

        patienceBar.value = 100f;
        UpdatePatienceColor(100f);

        hasOrder = true;
        isResolving = true;

        ResetVisuals();
        gameObject.SetActive(true);

        StartCoroutine(PlayEntranceAnimation());
    }

    public void CompleteOrder()
    {
        if (!hasOrder || isResolving)
        {
            return;
        }

        if (gameProgressManager != null)
        {
            int patienceBonus = Mathf.RoundToInt(
                patienceRemaining / patienceDuration * 100f
            );

            patienceBonus =
                Mathf.Clamp(patienceBonus, 0, 100);

            gameProgressManager.RegisterSuccess(
                patienceBonus
            );
        }

        StartCoroutine(PlayResultAnimation(Color.green));
    }

    public void FailOrder(FailureReason failureReason)
    {
        if (!hasOrder || isResolving)
        {
            return;
        }

        if (gameProgressManager != null)
        {
            gameProgressManager.RegisterFailure(failureReason);
        }

        StartCoroutine(PlayResultAnimation(Color.red));
    }


    private IEnumerator PlayEntranceAnimation()
    {
        Vector2 entrancePosition =
            startingPosition + Vector2.up * slideDistance;

        orderRectTransform.anchoredPosition =
            entrancePosition;

        canvasGroup.alpha = 0f;

        float elapsedTime = 0f;

        while (elapsedTime < slideInDuration)
        {
            elapsedTime += Time.deltaTime;

            float progress =
                elapsedTime / slideInDuration;

            orderRectTransform.anchoredPosition =
                Vector2.Lerp(
                    entrancePosition,
                    startingPosition,
                    progress
                );

            canvasGroup.alpha =
                Mathf.Lerp(0f, 1f, progress);

            yield return null;
        }

        orderRectTransform.anchoredPosition =
            startingPosition;

        canvasGroup.alpha = 1f;
        isResolving = false;
    }

    private IEnumerator PlayResultAnimation(Color resultColor)
    {
        isResolving = true;

        float elapsedTime = 0f;

        while (elapsedTime < glowDuration)
        {
            elapsedTime += Time.deltaTime;

            float progress =
                elapsedTime / glowDuration;

            float glowAlpha =
                Mathf.Lerp(0f, 0.7f, progress);

            resultGlow.color = new Color(
                resultColor.r,
                resultColor.g,
                resultColor.b,
                glowAlpha
            );

            yield return null;
        }

        elapsedTime = 0f;

        Vector2 targetPosition =
            startingPosition + Vector2.up * slideDistance;

        while (elapsedTime < slideDuration)
        {
            elapsedTime += Time.deltaTime;

            float progress =
                elapsedTime / slideDuration;

            orderRectTransform.anchoredPosition =
                Vector2.Lerp(
                    startingPosition,
                    targetPosition,
                    progress
                );

            canvasGroup.alpha =
                Mathf.Lerp(1f, 0f, progress);

            yield return null;
        }

        ClearOrder();
    }

    private void ResetVisuals()
    {
        orderRectTransform.anchoredPosition =
            startingPosition;

        canvasGroup.alpha = 1f;

        Color glowColor = resultGlow.color;
        glowColor.a = 0f;
        resultGlow.color = glowColor;
    }

    public void ClearOrder()
    {
        hasOrder = false;
        isResolving = false;
        orderNumber = 0;


        requestedWeapon = null;
        requiredMaterial1 = null;
        requiredMaterial2 = null;

        ResetVisuals();
        gameObject.SetActive(false);
    }
}
