using UnityEngine;
using TMPro;
using DG.Tweening;

public class FadeTextIn : MonoBehaviour
{
    public float duration = 3f;
    public float delay = 0f;

    private TMP_Text tmpText;
    private Material tmpMaterial;

    void Awake()
    {
        tmpText = GetComponent<TMP_Text>();

        // Create a unique material instance to avoid affecting shared materials
        tmpText.fontMaterial = new Material(tmpText.fontMaterial);
        tmpMaterial = tmpText.fontMaterial;
    }

    public void FadeInText()
    {
        // Optional: Stop existing tweens on this material to avoid stacking
        DOTween.Kill(tmpMaterial);

        // Set initial alpha to 0 for face, outline, and underlay
        SetAlpha("_FaceColor", 0f);
        SetAlpha("_OutlineColor", 0f);
        if (tmpMaterial.HasProperty("_UnderlayColor"))
            SetAlpha("_UnderlayColor", 0f);

        // Tween to alpha 1
        FadeToAlpha("_FaceColor", 1f);
        FadeToAlpha("_OutlineColor", 1f);
        if (tmpMaterial.HasProperty("_UnderlayColor"))
            FadeToAlpha("_UnderlayColor", 1f);
    }

    private void SetAlpha(string property, float alpha)
    {
        Color color = tmpMaterial.GetColor(property);
        color.a = alpha;
        tmpMaterial.SetColor(property, color);
    }

    private void FadeToAlpha(string property, float targetAlpha)
    {
        Color color = tmpMaterial.GetColor(property);
        tmpMaterial.DOColor(new Color(color.r, color.g, color.b, targetAlpha), property, duration)
                   .SetEase(Ease.OutQuad)
                   .SetDelay(delay);
    }
}
