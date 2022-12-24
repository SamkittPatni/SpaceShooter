using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3f;
    private AudioSource powerup_sound;
    // Start is called before the first frame update
    void Start()
    {
        powerup_sound = GameObject.Find("Powerup").transform.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y <= -5.5f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //checking if collision is with player and giving powerup based on which is collided with 
        if(other.tag == "Player")
        {
            powerup_sound.Play(0);
            Player _player = other.transform.GetComponent<Player>();
            if(_player != null)
            {
                if (gameObject.tag == "Triple_Shot")
                {
                    _player.TrippleShotActive();

                }
                else if (gameObject.tag == "Speed")
                {
                    _player.SpeedActive();
                }
                else if (gameObject.tag == "Shield")
                {
                    _player.ShieldActive();
                }
            }
            Destroy(this.gameObject);
        }
    }
}
