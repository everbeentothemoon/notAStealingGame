using UnityEngine;

public class killing : MonoBehaviour
{
    public enemyWalking ew;
    public GameObject enemy;
    private bool hasInstantiatedCorpse = false;

    public sounds s;

    void Start()
    {
        ew = enemy.GetComponent<enemyWalking>();
        s = enemy.GetComponent<sounds>();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !hasInstantiatedCorpse)
        {
            hasInstantiatedCorpse = true;
            killEdward();
        }
    }

    public void killEdward()
    {
        Debug.Log("player collided");
        GameObject o = Instantiate(ew.corpse);
        s.PlayMusic();
        o.transform.position = ew.player.transform.position;

        ew.player.transform.position = ew.spawnPosition[ew.spawnCount].transform.position;
        
        if (ew.player.transform.position == ew.spawnPosition[ew.spawnCount].transform.position)
        {

            hasInstantiatedCorpse = false;
        }

    }
}
