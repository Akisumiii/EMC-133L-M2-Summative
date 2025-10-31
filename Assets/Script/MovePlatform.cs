using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    [Header("Movement Settings")]
    public Vector3 targetOffset = new Vector3(0f, 0f, 5f); // Offset from the start position
    public float speed = 2f; // Movement speed
    public bool useSineMotion = true; // Smooth back-and-forth motion

    private Vector3 startPos;
    private Vector3 targetPos;
    private float t = 0f;

    void Start()
    {
        // Record the initial position
        startPos = transform.position;
        // Determine the target position relative to the start
        targetPos = startPos + targetOffset;
    }

    void Update()
    {
        if (useSineMotion)
        {
            // Smooth back-and-forth using sine wave
            float sineValue = (Mathf.Sin(Time.time * speed) + 1f) / 2f; // 0 → 1 → 0
            transform.position = Vector3.Lerp(startPos, targetPos, sineValue);
        }
        else
        {
            // Linear ping-pong movement
            t += Time.deltaTime * speed;
            float pingpong = Mathf.PingPong(t, 1f);
            transform.position = Vector3.Lerp(startPos, targetPos, pingpong);
        }
    }

    // Visualize the path in the editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Vector3 currentStart = Application.isPlaying ? startPos : transform.position;
        Vector3 currentTarget = currentStart + targetOffset;
        Gizmos.DrawLine(currentStart, currentTarget);
        Gizmos.DrawSphere(currentTarget, 0.1f);
    }
}
