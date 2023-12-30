using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    Transform target;
    GameObject ball;
    public Vector3 _offset;
    // Start is called before the first frame update
    void Start()
    {
        ResetCuePosition();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        float speed = Input.GetAxis("Mouse X") * 10 * Time.deltaTime;
        transform.RotateAround(target.position, Vector3.up, speed);
    }

    void ResetCuePosition()
    {
        ball = GameObject.Find("WhiteBall");
        target = ball.transform;
        transform.position = target.position + _offset;
    }
}
