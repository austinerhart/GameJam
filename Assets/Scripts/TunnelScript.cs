using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TunnelScript : MonoBehaviour
{
    public float fadeDuration = 1f;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    public GameObject player;
    public GameObject tunnelOpening; 
    public bool isTouchingTunnel;
    
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = player.GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTouchingTunnel && Input.GetKeyDown(KeyCode.S)){
            Debug.Log("Tunnelling");
            StartCoroutine(Telaport());
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isTouchingTunnel = true;
            Debug.Log("Collided with tunnel");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isTouchingTunnel = false;
        }
    }

    IEnumerator Telaport(){
        //fade out
        yield return StartCoroutine(Fade(1f, 0f, fadeDuration));

        //teleport time
        yield return new WaitForSeconds(1f);

        Vector3 playerSpawn = tunnelOpening.transform.position + new Vector3(0f, 0f, -2f);
        player.transform.position = playerSpawn;

        //fade in
        yield return StartCoroutine(Fade(0f, 1f, fadeDuration));
    }

    IEnumerator Fade(float startAlpha, float endAlpha, float duration)
    {
        Color color = spriteRenderer.color;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);
            spriteRenderer.color = new Color(color.r, color.g, color.b, alpha);
            elapsed += Time.deltaTime;
            yield return null;
        }

        spriteRenderer.color = new Color(color.r, color.g, color.b, endAlpha);
    }

}
