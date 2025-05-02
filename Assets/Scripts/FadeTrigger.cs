using UnityEngine;

public class FadeTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Trigger entered by: {other.name}");

        if (!other.CompareTag("Player"))
        {
            Debug.Log("Entered by something that is NOT tagged 'Player'. Ignored.");
            return;
        }

        Debug.Log("Player tag detected.");

        string triggerName = gameObject.name;

        // Access parent GameObject
        GameObject parent = transform.parent.gameObject;

        if (triggerName.Contains("Start"))
        {
            FadeTextIn fadeIn = parent.GetComponent<FadeTextIn>();
            if (fadeIn != null)
            {
                Debug.Log("FadeTextIn script found. Triggering fade in...");
                fadeIn.FadeInText();
            }
            else
            {
                Debug.LogWarning("FadeTextIn script NOT found on parent!");
            }
        }
        else if (triggerName.Contains("End"))
        {
            FadeTextOut fadeOut = parent.GetComponent<FadeTextOut>();
            if (fadeOut != null)
            {
                Debug.Log("FadeTextOut script found. Triggering fade out...");
                fadeOut.FadeOutText();
            }
            else
            {
                Debug.LogWarning("FadeTextOut script NOT found on parent!");
            }
        }
        else
        {
            Debug.Log("Trigger name didn't match expected 'Start' or 'End'. No action taken.");
        }
    }
}
