using UnityEngine;

public class GreyScript : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    public void SetGreyscaleLevel(int level)
    {
        level = Mathf.Clamp(level, 0, 5);
        float t = level / 5f;

        // Convert original color to grayscale
        float grey = originalColor.r * 0.3f + originalColor.g * 0.59f + originalColor.b * 0.11f;
        Color greyscaleColor = new Color(grey, grey, grey, originalColor.a);

        // Lerp between original color and greyscale
        spriteRenderer.color = Color.Lerp(originalColor, greyscaleColor, t);
        //Debug.Log(spriteRenderer.color);
    }
}

