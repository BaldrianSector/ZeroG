using UnityEngine;
using TMPro;
using DG.Tweening;

public class FadeTextOut : MonoBehaviour
{
    public float duration = 2f;
    public float delay = 0f;

    private TMP_Text tmpText;
    private Material tmpMaterial;

    void Awake()
    {
        tmpText = GetComponent<TMP_Text>();

        // Create a unique instance of the material to avoid shared material issues
        tmpText.fontMaterial = new Material(tmpText.fontMaterial);
        tmpMaterial = tmpText.fontMaterial;
    }

    public void FadeOutText()
    {
        // Optional: Stop existing tweens on this material to avoid stacking
        DOTween.Kill(tmpMaterial);

        // Fade out face color
        Color faceColor = tmpMaterial.GetColor("_FaceColor");
        tmpMaterial.DOColor(new Color(faceColor.r, faceColor.g, faceColor.b, 0f), "_FaceColor", duration)
                   .SetEase(Ease.OutQuad)
                   .SetDelay(delay);

        // Fade out outline color
        Color outlineColor = tmpMaterial.GetColor("_OutlineColor");
        tmpMaterial.DOColor(new Color(outlineColor.r, outlineColor.g, outlineColor.b, 0f), "_OutlineColor", duration)
                   .SetEase(Ease.OutQuad)
                   .SetDelay(delay);

        // Fade out underlay color if present
        if (tmpMaterial.HasProperty("_UnderlayColor"))
        {
            Color underlayColor = tmpMaterial.GetColor("_UnderlayColor");
            tmpMaterial.DOColor(new Color(underlayColor.r, underlayColor.g, underlayColor.b, 0f), "_UnderlayColor", duration)
                       .SetEase(Ease.OutQuad)
                       .SetDelay(delay);
        }
    }
}
