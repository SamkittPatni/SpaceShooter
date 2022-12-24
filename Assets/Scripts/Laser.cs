using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private float _speed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //movement of laser
        transform.Translate(Vector3.up * _speed * Time.deltaTime);
        //logic to destroy the laser object after it goes out of frame
        if(transform.position.y >= 7.5)
        {
            if(transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }
}
