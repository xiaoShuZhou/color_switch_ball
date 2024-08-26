using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerBall : MonoBehaviour
{
    public static PlayerBall Instance;
    private void Awake()
    {
        Instance = this;
    }

    public float jumpForce = 5f;  // Force applied when jumping
    private Rigidbody2D rb;

    private Color ballColor;      // Current color of the ball
    private Color previousColor;  // Previous color of the ball

    // Predefined colors
    Color color1 = new Color(1, 98/255f, 0);
    Color color2 = new Color(0, 1, 1);
    Color color3 = new Color(1, 0, 1);
    Color color4 = new Color(0, 1, 0);

    public int score = 0;  // Player's score

    public GameObject explosionEffect;     // Explosion effect when the ball is destroyed
    public GameObject starExplosionEffect; // Explosion effect when collecting a star

    public Text scoreText;        // UI Text to display the score
    public GameObject gameOverPanel; // Game over panel

    private Camera mainCamera;
    private float cameraBottomY;

    public static bool isLost = false;

    // Audio sources
    public AudioSource clickAudio;
    public AudioSource loseAudio;
    public AudioSource colorAudio;
    public AudioSource starAudio;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Set initial ball color
        ballColor = GetRandomColor();
        previousColor = ballColor;
        GetComponent<SpriteRenderer>().color = ballColor;

        mainCamera = Camera.main;
        // Calculate the bottom Y position of the camera view
        cameraBottomY = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane)).y;
        gameOverPanel.SetActive(false); // Hide game over panel at start
    }

    void Update()
    {
        // Jump when the player clicks
        if (Input.GetMouseButtonDown(0))
        {
            clickAudio.Play();
            rb.velocity = Vector2.up * jumpForce;
        }

        // Check if the ball is below the screen
        if (transform.position.y < cameraBottomY)
        {
            loseAudio.Play();
            TriggerExplosion(transform.position, explosionEffect);
            Destroy(gameObject);
            Debug.Log("Failed: Ball went below the screen.");
            ShowGameOverPanel();
        }

        if (isLost)
        {
            if (Input.GetMouseButtonDown(0))
            {
                RestartGame();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check collision with arcs
        if (collision.CompareTag("Arc"))
        {
            SpriteRenderer collisionRenderer = collision.GetComponent<SpriteRenderer>();
            if (collisionRenderer != null)
            {
                Color collisionColor = collisionRenderer.color;

                // Check if colors match
                if (collisionColor != ballColor)
                {
                    loseAudio.Play();
                    Debug.Log("Color mismatch! " + ballColor + " + " + collisionColor);
                    TriggerExplosion(transform.position, explosionEffect);
                    Destroy(gameObject);
                    ShowGameOverPanel();
                }
            }
        }

        // Check collision with color switcher
        if (collision.CompareTag("ColorSwitcherTre"))
        {
            Color newColor;
            do
            {
                newColor = GetRandomColor();
            } while (newColor == previousColor);

            previousColor = ballColor;
            ballColor = newColor;
            GetComponent<SpriteRenderer>().color = ballColor;
            Destroy(collision.gameObject);
            colorAudio.Play();
        }

        // Check collision with star
        if (collision.CompareTag("Star"))
        {
            score += 1;
            TriggerExplosion(collision.transform.position, starExplosionEffect);
            Destroy(collision.gameObject);
            UpdateScoreText();
            starAudio.Play();
        }
    }

    private Color GetRandomColor()
    {
        Color[] colors = { color1, color2, color3, color4 };
        return colors[Random.Range(0, colors.Length)];
    }

    private void TriggerExplosion(Vector3 position, GameObject effectPrefab)
    {
        if (effectPrefab != null)
        {
            Instantiate(effectPrefab, position, Quaternion.identity);
        }
    }

    private void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
        isLost = true;
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "" + score;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}