using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody _rb;
    float _movementX, _movementZ;
    public float _speed = 30;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        _movementZ = Input.GetAxis("Horizontal");
        _movementX = Input.GetAxis("Vertical");

        _rb.velocity = new Vector3(_movementZ * _speed, _rb.velocity.y, _movementX * _speed);
    }
}
