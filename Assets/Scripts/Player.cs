using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool isPlayerOne = false;
    public bool isPlayerTwo = false;
    [SerializeField]
    private float _speed = 8f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _trippleShotPrefab;
    [SerializeField]
    private GameObject _shieldPrefab;
    private GameObject shield;
    private float _fireRate = 0.2f;
    private float _nextFire = 0.0f;
    [SerializeField]
    public int _lives = 3;
    private Spawn_Manager spawnManager;
    [SerializeField]
    public bool isTrippleShot = false;
    [SerializeField]
    public bool isSpeed = false;
    [SerializeField]
    public bool isShield = false;
    [SerializeField]
    private GameObject hurt1;
    [SerializeField]
    private GameObject hurt2;
    [SerializeField]
    private GameObject thruster;
    public int _score = 0;
    private Animator animator;
    public int _ammo = 40;
    [SerializeField]
    private AudioSource laser_sound;
    [SerializeField]
    private AudioSource explosion_sound;
    private GameManager _gameManager;
    // Start is called before the first frame update
    void Start()
    {
        hurt1.SetActive(false);
        hurt2.SetActive(false);
        thruster.SetActive(true);
        animator = GetComponent<Animator>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        spawnManager = GameObject.Find("Spawn_Manager").GetComponent<Spawn_Manager>();
        if (spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is NULL.");
        }
        if (_gameManager.isSingle == true)
        {
            transform.position = new Vector3(0, -3.5f, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerOne == true)
        {
            MovementOne();

            //logic for shooting and cooldown
            if (Input.GetKeyDown(KeyCode.Space) && Time.time > _nextFire)
            {
                if (_gameManager.isSingle == true)
                {
                    if (_ammo > 0)
                    {
                        Shoot();
                        _ammo--;
                    }
                }
                else
                {
                    Shoot();
                }
            }

        }
        
        if (isPlayerTwo == true)
        {
            MovementTwo();
            if (Input.GetKeyDown(KeyCode.RightControl) && isPlayerTwo == true)
            {
                Shoot();
            }
        }
        

        shield = GameObject.FindWithTag("Player_Shield");
        if (isShield == true)
        {
            shield.transform.position = transform.position;
        }

        if (_lives == 2)
        {
            this.hurt1.SetActive(true);
        }

        if(_lives == 1)
        {
            this.hurt2.SetActive(true);
        }
    }

    void MovementOne()
    {
        if (isSpeed == true && _lives != 0)
        {
            _speed = 10f;
        }
        else if (isSpeed == false && _lives != 0)
        {
            _speed = 8f;
        }
        //left and right movement
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontalInput * _speed * Time.deltaTime);
        //up and down movement
        float verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.up * verticalInput * _speed * Time.deltaTime);

        //boundary logic
        if (transform.position.x > 11.5f)
        {
            transform.position = new Vector3(-11.5f, transform.position.y, 0);
        }
        else if (transform.position.x < -11.5f)
        {
            transform.position = new Vector3(11.5f, transform.position.y, 0);
        }
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.5f, 0), 0);
    }

    void MovementTwo()
    {
        if (isSpeed == true && _lives != 0)
        {
            _speed = 10f;
        }
        else if (isSpeed == false && _lives != 0)
        {
            _speed = 8f;
        }
        //left and right movement
        float horizontalInput = Input.GetAxis("HorizontalTwo");
        transform.Translate(Vector3.right * horizontalInput * _speed * Time.deltaTime);
        //up and down movement
        float verticalInput = Input.GetAxis("VerticalTwo");
        transform.Translate(Vector3.up * verticalInput * _speed * Time.deltaTime);

        //boundary logic
        if (transform.position.x > 11.5f)
        {
            transform.position = new Vector3(-11.5f, transform.position.y, 0);
        }
        else if (transform.position.x < -11.5f)
        {
            transform.position = new Vector3(11.5f, transform.position.y, 0);
        }
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.5f, 0), 0);
    }

    void Shoot()
    {
        //shooting mechanism
        _nextFire = Time.time + _fireRate;
        if (isTrippleShot)
        {
            Instantiate(_trippleShotPrefab, transform.position + new Vector3(-2.5f, 0.75f, 0), Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
        }
        laser_sound.Play(0);
    }
    //method to damage player
    public void Damage()
    {
        this._lives--;
        if(this._lives == 0)
        {
            this._speed = 0f;
            spawnManager.onPlayerDeath();
            this.hurt1.SetActive(false);
            this.hurt2.SetActive(false);
            this.thruster.SetActive(false);
            animator.SetTrigger("Player_dead");
            explosion_sound.volume = 1f;
            explosion_sound.Play(0);
            Destroy(this.gameObject, 2.3f);
        }
        if (this._lives < 0)
        {
            this._lives = 0;
        }
    }
    //triple shot powerup activation method
    public void TrippleShotActive()
    {
        isTrippleShot = true;
        StartCoroutine(PowerCooldown());
    }
    //speed powerup activation method
    public void SpeedActive()
    {
        isSpeed = true;
        thruster.transform.localScale = new Vector3(1f, 1f, 0);
        StartCoroutine(PowerCooldown());
    }
    //shield powerup activation method
    public void ShieldActive()
    {
        isShield = true;
        Instantiate(_shieldPrefab, transform.position, Quaternion.identity);
        StartCoroutine(PowerCooldown());
    }
    //cooldown system for all the powerups
    IEnumerator PowerCooldown()
    {
        yield return new WaitForSeconds(5f);
        isTrippleShot = false;
        isSpeed = false;
        //shield destroy logic
        if (isShield == true)
        {
            shield = GameObject.FindWithTag("Player_Shield");
            Destroy(shield);
        }
        isShield = false;
        thruster.transform.localScale = new Vector3(0.5f, 0.5f, 0);
    }
    public void AddScore(int points)
    {
        _score = _score + points;
    }
}
