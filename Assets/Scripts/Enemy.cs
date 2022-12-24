using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _speed = 2.5f;
    private float x;
    private GameManager _gameManager;
    private Animator animator;
    [SerializeField]
    private Player player;
    [SerializeField]
    private Player player2;
    [SerializeField]
    private AudioSource explosion_sound;
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        explosion_sound = GameObject.Find("Explosion").transform.GetComponent<AudioSource>();
        if(_gameManager.isSingle == true)
        {
            player = GameObject.FindWithTag("Player").transform.GetComponent<Player>();
        }
        else
        {
            /*player = GameObject.Find("Player_1").transform.GetComponent<Player>();
            player2 = GameObject.Find("Player_2").transform.GetComponent<Player>();*/
        }
        //col = gameObject.GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("animator is NULL.");
        }
        /*if (col == null)
        {
            Debug.LogError("collider is NULL.");
        }*/
        x = Random.Range(-8, 8);
        transform.position = new Vector3(x, 7.5f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if(_gameManager.isSingle == true)
        {
            if (player == null)
            {
                animator.SetTrigger("OnEnemyDeath");
                explosion_sound.Play(0);
                Destroy(this.gameObject, 2.3f);
            }
            if (transform.position.y <= -5.5f)
            {
                if (player != null && player._lives != 0)
                {
                    explosion_sound.Play(0);
                    player.Damage();
                }
                Destroy(this.gameObject);
            }
        }
        else
        {
            Player _player = GameObject.FindWithTag("Player").transform.GetComponent<Player>();
            if (_player == null)
            {
                //animator.SetTrigger("OnEnemyDeath");
                explosion_sound.Play(0);
                Destroy(this.gameObject, 2.3f);
            }
            if (transform.position.y <= -5.5f)
            {
                /*if (player != null)
                {
                    if (player._lives != 0)
                    {
                        explosion_sound.Play(0);
                        player.Damage();
                    }
                }
                else if (player2 != null)
                {
                    if (player2._lives != 0)
                    {
                        explosion_sound.Play(0);
                        player2.Damage();
                    }
                }*/
                if(_player != null)
                {
                    if(_player._lives != 0)
                    {
                        explosion_sound.Play(0);
                        _player.Damage();
                    }
                }
                Destroy(this.gameObject);
            }
        }
        //logic for losing life if miss a target
    }

    //Collision method
    private void OnTriggerEnter2D(Collider2D other)
    {
        //destroy logic
        if (other.tag == "Player")
        {
            Player _player = other.gameObject.transform.GetComponent<Player>();
            //null checking(to see if the object actually exists)
            if (_gameManager.isSingle == true)
            {
                if (player._ammo == 0)
                {
                    player._ammo = 10;
                }
                if (player != null)
                {
                    player.Damage();
                }
            }
            else
            {
                /*if(other.name == "Player_1" && player != null)
                {
                    if(player != null)
                    {
                        player.Damage();
                    }
                }
                else if (other.name == "Player_2" && player2 != null)
                {
                    if(player2 != null)
                    {
                        player2.Damage();
                    }
                }*/
                if (_player != null)
                {
                    _player.Damage();
                }
            }
            EnemyDeath();
        }
        else if (other.tag == "Laser")
        {
            Player _player = other.gameObject.transform.GetComponent<Player>();
            Destroy(other.gameObject);
            if(_gameManager.isSingle == true)
            {
                if (Random.Range(1, 5) == 3)
                {
                    if(player != null)
                    {
                        player._ammo = player._ammo + 5;
                    }
                }
                int point = Random.Range(5, 16);
                if (player != null)
                {
                    player.AddScore(point);
                }
            }
            else
            {
                int point = Random.Range(5, 16);
                if(_player != null)
                {
                    _player.AddScore(point);
                }
            }
            EnemyDeath();
        }
        //shield logic
        else if (other.tag == "Player_Shield")
        {
            Player _player = other.gameObject.transform.GetComponent<Player>();
            if (_gameManager.isSingle == true)
            {
                if (player != null)
                {
                    player.AddScore(5);
                }
            }
            else
            {
                if(_player != null)
                {
                    _player.AddScore(5);
                }
            }
            EnemyDeath();
        }
    }

    private void EnemyDeath()
    {
        Destroy(GetComponent<Collider2D>());
        animator.SetTrigger("OnEnemyDeath");
        explosion_sound.Play(0);
        _speed = 0;
        Destroy(this.gameObject, 2.3f);
    }
}
