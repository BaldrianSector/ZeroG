using UnityEngine;

public class FadeTriggerFab : MonoBehaviour
{
    public GameObject fadePrefab; // Assign the prefab in the inspector
    public float fadeDuration = 1.0f; // Duration of the fade effect

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Instantiate the fade prefab at the player's position
            GameObject fadeInstance = Instantiate(fadePrefab, other.transform.position, Quaternion.identity);
            FadeEffect fadeEffect = fadeInstance.GetComponent<FadeEffect>();
            if (fadeEffect != null)
            {
                fadeEffect.StartFade(fadeDuration);
            }
        }
    }
}
