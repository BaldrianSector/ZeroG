using UnityEngine;

public class FadeEndTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Trigger entered by: {other.name}");

        if (other.CompareTag("Player")) // Ensure your player GameObject has this tag
        {
            Debug.Log("Player tag detected.");

            FadeTextOut fadeScript = GetComponentInParent<FadeTextOut>();
            if (fadeScript != null)
            {
                Debug.Log("FadeTextOut script found. Triggering fade...");
                fadeScript.FadeOutText();
            }
            else
            {
                Debug.LogWarning("FadeTextOut script NOT found on parent!");
            }
        }
        else
        {
            Debug.Log("Entered by something that is NOT tagged 'Player'. Ignored.");
        }
    }
}
