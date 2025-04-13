using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class Caught : MonoBehaviour
{
    public bool hidden;
    public GameObject spawnPoint;
    public GameObject player; 
    public Image fadePanel;
    public float fadeDuration = 1f;
    TextMeshProUGUI textElement;
    Transform textTransform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnPoint = GameObject.Find("playerSpawnPoint");
        player = GameObject.Find("Player");
        textTransform = fadePanel.transform.Find("reeducated");
        textElement = textTransform.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.CompareTag("Player")){
            if(!hidden){
            StartCoroutine(gameOver());
            }
        }
    }

    public void OnTriggerStay2D(Collider2D collider){
        if(collider.gameObject.CompareTag("Player")){
            if(!hidden){
            StartCoroutine(gameOver());
            }
        }
    }

    IEnumerator gameOver(){

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
    }

    IEnumerator FadeOutWhileFrozen()
    {
        float elapsed = 0f;
        Color startColor = new Color(0, 0, 0, 0);
        Color endColor = new Color(0, 0, 0, 1);

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
        Color startColor = new Color(0, 0, 0, 1);
        Color endColor = new Color(0, 0, 0, 0);

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
