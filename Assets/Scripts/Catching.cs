using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class Catching : MonoBehaviour
{
    public Renderer targetRenderer;
    public Color colorA = Color.yellow;
    public Color colorB = Color.red;
    public float flickerRate = 0.01f;
    public float requiredStayTime = 0.5f;

    private Coroutine flickerRoutine = null;
    private GameObject player;
    private bool playerInside = false;
    private float timeInside = 0f;
    public bool hidden = false;
    public Image fadePanel;
    public float fadeDuration = 1f;
    TextMeshProUGUI textElement;
    public GameObject spawnPoint;

    void Start()
    {
        player = GameObject.Find("Player"); 
        Transform textTransform = fadePanel.transform.Find("reeducated");
        textElement = textTransform.GetComponent<TextMeshProUGUI>();
        spawnPoint = GameObject.Find("playerSpawnPoint");
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!hidden){
                playerInside = true;
                flickerRoutine = StartCoroutine(FlickerTimer());
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
            timeInside = 0f;

            if (flickerRoutine != null)
            {
                StopCoroutine(flickerRoutine);
                flickerRoutine = null;
            }

            if (targetRenderer != null)
                targetRenderer.material.color = colorA; // reset color
        }
    }

    IEnumerator FlickerTimer()
    {
        //Debug.Log("Running Coroutine");
        timeInside = 0f;

        while (playerInside && timeInside < 0.2f)
        {
            if (targetRenderer != null)
            {
                Color currentColor = targetRenderer.material.color;
                targetRenderer.material.color = (currentColor == colorA) ? colorB : colorA;
            }

            yield return new WaitForSeconds(flickerRate);
            timeInside += flickerRate;
        }

        if (playerInside)
        {
            // Set to solid red
            if (targetRenderer != null)
                targetRenderer.material.color = colorB;

            // Freeze player
            if (player != null)
                Time.timeScale = 0f; 
                MonoBehaviour[] scripts = player.GetComponents<MonoBehaviour>();
                foreach (var script in scripts)
                    script.enabled = false;
                yield return new WaitForSecondsRealtime(1f);
                yield return StartCoroutine(FadeOutWhileFrozen());
                yield return StartCoroutine(FadeTextIn());
                yield return new WaitForSecondsRealtime(1f);
                player.transform.position = spawnPoint.transform.position;
                yield return StartCoroutine(FadeTextOut());
                yield return StartCoroutine(FadeInWhileFrozen());
                Time.timeScale = 1f; 
                foreach (var script in scripts)
                    script.enabled = true;
            PlayerManager.Instance.times_caught += 1;
        }
    }

     IEnumerator FadeOutWhileFrozen()
    {
        float elapsed = 0f;
        Color startColor = new Color(0, 0, 0, 0);
        Color endColor = new Color(1, 1, 1, 1);

        while (elapsed < fadeDuration)
        {
            float t = elapsed / fadeDuration;
            fadePanel.color = Color.Lerp(startColor, endColor, t);
            elapsed += Time.unscaledDeltaTime;  // Use unscaled time to keep fading
            yield return null;
        }

        fadePanel.color = endColor;
    }

    IEnumerator FadeTextIn()
    {
        float elapsed = 0f;
        Color originalColor = textElement.color;
        Color startColor = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
        Color targetColor = new Color(originalColor.r, originalColor.g, originalColor.b, 1f);

        // Start fully transparent
        textElement.color = startColor;

        while (elapsed < fadeDuration)
        {
            textElement.color = Color.Lerp(startColor, targetColor, elapsed / fadeDuration);
            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        textElement.color = targetColor;
    }

    IEnumerator FadeTextOut()
    {
        float elapsed = 0f;
        Color originalColor = textElement.color;
        Color targetColor = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);

        while (elapsed < fadeDuration)
        {
            textElement.color = Color.Lerp(originalColor, targetColor, elapsed / fadeDuration);
            elapsed += Time.unscaledDeltaTime; // So it works even when timeScale = 0
            yield return null;
        }

        textElement.color = targetColor;
    }

    IEnumerator FadeInWhileFrozen()
    {
        float elapsed = 0f;
        Color startColor = new Color(1, 1, 1, 1);
        Color endColor = new Color(1,1,1, 0);

        while (elapsed < fadeDuration)
        {
            float t = elapsed / fadeDuration;
            fadePanel.color = Color.Lerp(startColor, endColor, t);
            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        fadePanel.color = endColor;
    }

}

