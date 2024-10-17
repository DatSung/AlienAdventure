using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpSpeed = 5f;
    private float direction = 0f;
    public Transform groundCheck;
    private Rigidbody2D player;
    public float groundCheckRadius;
    public LayerMask groundPlayer;
    private bool isTouchingGround;
    private Animator playerAnimation;
    private float playerScaleX = 0.2f;
    private Vector3 respawnPoint;
    public GameObject fallDetector;
    private int score = 0;
    public Text scoreText; 
    public HealthBarController healthBarController;


    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<Animator>();
        respawnPoint = transform.position;
        scoreText.text = "Score: " + ScoringController.totalScore;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMove();
        
    }

    private void HandleMove()
    {
        isTouchingGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundPlayer);
        direction = Input.GetAxis("Horizontal");

        if (direction > 0f)
        {
            player.velocity = new Vector2(direction * moveSpeed, player.velocity.y);
            transform.localScale = new Vector2(playerScaleX, transform.localScale.y);
        }
        else if (direction < 0f)
        {
            player.velocity = new Vector2(direction * moveSpeed, player.velocity.y);
            transform.localScale = new Vector2(-playerScaleX, transform.localScale.y);
        }
        else
        {
            player.velocity = new Vector2(0, player.velocity.y);
        }

        if (Input.GetButtonDown("Jump") && isTouchingGround)
        {
            player.velocity = new Vector2(player.velocity.x, jumpSpeed);
        }

        playerAnimation.SetFloat("Speed", Mathf.Abs(player.velocity.x));
        playerAnimation.SetBool("OnGround", isTouchingGround);

        fallDetector.transform.position = new Vector2(transform.position.x, fallDetector.transform.position.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "FallDetector")
        {
            transform.position = respawnPoint;
        }
        if (collision.tag == "Checkpoint")
        {
            respawnPoint = transform.position;
        }
        if (collision.tag == "NextLevel")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            respawnPoint = transform.position;
        }
        if (collision.tag == "PreviousLevel")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            respawnPoint = transform.position;
        } 
        if (collision.tag == "Crystal")
        {
            ScoringController.totalScore += 1;
            scoreText.text = "Score " + ScoringController.totalScore;
            collision.gameObject.SetActive(false);
        }
    }

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.tag == "Spike")
        {
            var health = healthBarController.Damage(0.005f);
            if (health <= 0)
            {
				transform.position = respawnPoint;
                HealthController.totalHealth = 1f;
                healthBarController.SetSize(health);
			}
        }
	}
}