using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovement : MonoBehaviour
{
    public float movementRadius = 5f;
    public float movementSpeed = 2f;

    private Vector3 leftPosition;
    private Vector3 rightPosition;
    private bool isMovingRight = true;


    public GameObject player;
    public List<GameObject> spawnPosition;
    public int spawnCount;

    public GameObject walkingEnemy;
    public float launchForce = 5f;
    public GameObject corpse;

    private Rigidbody2D rb;
    private Rigidbody2D enemyRb;

    public GameObject enemy;
    public sounds s;

    private void Start()
    {
        s = enemy.GetComponent<sounds>();
        rb = GetComponent<Rigidbody2D>();
        enemyRb = GetComponent<Rigidbody2D>();
        // Calculate the initial left and right positions based on the movement radius
        leftPosition = transform.position - new Vector3(movementRadius, 0f, 0f);
        rightPosition = transform.position + new Vector3(movementRadius, 0f, 0f);
    }

    private void Update()
    {

        foreach (GameObject spawnPoint in spawnPosition)
        {
            // Check if the current spawn point has a GameObject with the "crate" tag
            Collider2D[] colliders = Physics2D.OverlapCircleAll(spawnPoint.transform.position, 1f); // Adjust the radius as needed
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("crate"))
                {
                    // Destroy the GameObject
                    Destroy(collider.gameObject);
                    Debug.Log("destroyed");
                }
            }
        }

        if (isMovingRight)
        {
            // Move towards the right position
            transform.position = Vector3.MoveTowards(transform.position, rightPosition, movementSpeed * Time.deltaTime);

            // Check if the GameObject has reached the right position
            if (Vector3.Distance(transform.position, rightPosition) < 0.01f)
            {
                isMovingRight = false;
            }
        }
        else
        {
            // Move towards the left position
            transform.position = Vector3.MoveTowards(transform.position, leftPosition, movementSpeed * Time.deltaTime);

            // Check if the GameObject has reached the left position
            if (Vector3.Distance(transform.position, leftPosition) < 0.01f)
            {
                isMovingRight = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") == player)
        {
            Debug.Log("playerCollided");
            LaunchPlayer();
            LaunchEnemy();

            Debug.Log("player collided");
            GameObject o = Instantiate(corpse);
            s.PlayMusic();
            o.transform.position = player.transform.position;

            player.transform.position = spawnPosition[spawnCount].transform.position;

            if (gameObject.CompareTag("flyingEnemy") == collision.gameObject.CompareTag("Player"))
            {
                Debug.Log("money & sex");
                Destroy(gameObject);
            }
        }
    }

    public void LaunchPlayer()
    {
        Vector2 launchDirection = (transform.position - walkingEnemy.transform.position).normalized;

        rb.AddForce(launchDirection * launchForce, ForceMode2D.Impulse);
    }

    public void LaunchEnemy()
    {
        Vector2 launchDirection = (walkingEnemy.transform.position - transform.position).normalized;

        enemyRb.AddForce(launchDirection * launchForce, ForceMode2D.Impulse);
    }
}
