using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransitionHandler : MonoBehaviour
{
    [Header("Level Transition Settings")]
    [Tooltip("Name of the scene to load (must be added to Build Settings). Leave blank to load the next scene in order.")]
    public string nextSceneName = "";

    [Tooltip("Delay before loading the next scene (in seconds).")]
    public float transitionDelay = 1f;

    [Tooltip("Optional fade screen or transition effect.")]
    public Animator transitionAnimator;

    private bool isTransitioning = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!isTransitioning && other.CompareTag("Player"))
        {
            StartCoroutine(HandleLevelTransition());
        }
    }

    private System.Collections.IEnumerator HandleLevelTransition()
    {
        isTransitioning = true;

        // Optional animation trigger (fade-out)
        if (transitionAnimator != null)
        {
            transitionAnimator.SetTrigger("Start");
        }

        // Wait before loading next scene
        yield return new WaitForSeconds(transitionDelay);

        // Determine next scene
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            // Load next scene in build order
            int currentIndex = SceneManager.GetActiveScene().buildIndex;
            int nextIndex = currentIndex + 1;

            if (nextIndex < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(nextIndex);
            }
            else
            {
                Debug.LogWarning("No more scenes in build order!");
            }
        }
    }
}
