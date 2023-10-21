using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyWalking : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float moveDistance = 5f;

    private float initialPositionX;
    private bool moveRight = true;

    public GameObject player;
    public List<GameObject> spawnPosition;
    public int spawnCount;

    public GameObject walkingEnemy;
    public float launchForce = 5f;
    public GameObject corpse;

    private Rigidbody2D rb;
    private Rigidbody2D enemyRb;

    public sounds s;

    private void Start()
    {
        s = gameObject.GetComponent<sounds>();
        rb = GetComponent<Rigidbody2D>();
        enemyRb = walkingEnemy.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
        }
        if (enemyRb == null)
        {
            enemyRb = walkingEnemy.AddComponent<Rigidbody2D>();
        }


        initialPositionX = transform.position.x;
    }

    private void Update()
    {
        if (moveRight)
        {
            MoveRight();
        }
        else
        {
            MoveLeft();
        }
    }

    private void MoveRight()
    {
        float newPositionX = transform.position.x + moveSpeed * Time.deltaTime;
        transform.position = new Vector3(newPositionX, transform.position.y, transform.position.z);

        if (transform.position.x >= initialPositionX + moveDistance)
        {
            moveRight = false;
        }
    }

    private void MoveLeft()
    {
        float newPositionX = transform.position.x - moveSpeed * Time.deltaTime;
        transform.position = new Vector3(newPositionX, transform.position.y, transform.position.z);

        if (transform.position.x <= initialPositionX)
        {
            moveRight = true;
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
