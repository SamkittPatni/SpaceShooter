using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Manager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] powerups;
    [SerializeField]
    private GameObject _enemyprefab;
    [SerializeField]
    private float time = 4f;
    private bool isAlive = true;
    private GameManager _gameManager;
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        StartCoroutine(SpawnEnemy());
        StartCoroutine(SpawnPowerup());
    }

    // Update is called once per frame
    void Update()
    {
        if (time > 1.25f)
        {
            time = time - 0.0005f;
        }
    }
    //coroutine to spawn enemy objects
    IEnumerator SpawnEnemy()
    {
        while (isAlive)
        {
            yield return new WaitForSeconds(time);
            Vector3 spawnposition = new Vector3(Random.Range(-8f, 8f), 7.5f, 0);
            Instantiate(_enemyprefab, spawnposition, Quaternion.identity);
        }
    }
    //coroutine for spawning of powerups
    IEnumerator SpawnPowerup()
    {
        //Randomizing from 1 to 3 to randomize the powerup
        while (isAlive)
        {
            float spawntime = Random.Range(10f, 15f);
            yield return new WaitForSeconds(spawntime);
            Vector3 powerupPos = new Vector3(Random.Range(-8f, 8f), 7.5f, 0);
            int select = Random.Range(0, 3);
            //generation of the powerups
            Instantiate(powerups[select], powerupPos, Quaternion.identity);
        }
    }
    //player death check
    public void onPlayerDeath()
    {
        if(_gameManager.isSingle == true)
        {
            isAlive = false;
        }
        else
        {
            int lives = _gameManager.Playerlives();
            if(lives == 0)
            {
                isAlive = false;
            }
        }
    }
}
