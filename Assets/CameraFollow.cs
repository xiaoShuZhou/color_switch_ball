using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraFollow : MonoBehaviour
{
    public Transform player;  // Reference to the player's Transform component
    public float followSpeed = 2f;  // Speed at which the camera follows the player

    private float lastY;  // Stores the last Y position of the camera

    void Start()
    {
        if (player != null)
        {
            lastY = transform.position.y;  // Initialize lastY with the camera's starting Y position
        }
    }

    void Update()
    {
        // Check if the player has lost
        if (PlayerBall.isLost)
        {
            // If the player clicks the left mouse button
            if (Input.GetMouseButtonDown(0))
            {
                PlayerBall.isLost = false;  // Reset the lost state
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // Reload the current scene
            }
        }

        // If the player object exists
        if (player != null)
        {
            float playerY = player.position.y;  // Get the current Y position of the player

            // Only update the camera position when the player moves upward
            if (playerY > lastY)
            {
                // Set the target position, only changing the Y axis
                Vector3 targetPosition = new Vector3(transform.position.x, playerY, transform.position.z);

                // Smoothly move the camera using Vector3.Lerp
                transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

                // Update the last Y position
                lastY = playerY;
            }
        }
    }
}