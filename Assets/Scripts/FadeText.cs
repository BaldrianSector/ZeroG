using UnityEngine;
using DG.Tweening;
using TMPro;

public class FadeText : MonoBehaviour
{
    // Tween text fade in
    public float duration = 3f;
    public float delay = 0f;

    // method onFadeIn()
    public void OnFadeIn()
    {
        // Get the TextMeshPro component
        var textMeshPro = GetComponent<TMPro.TMP_Text>();
        if (textMeshPro == null)
        {
            Debug.LogError("TextMeshPro component not found!");
            return;
        }

        // Set the initial alpha to 0
        textMeshPro.alpha = 0f;

        // Tween the alpha to 1
        textMeshPro.DOFade(1f, duration).SetDelay(delay);
    }
}
