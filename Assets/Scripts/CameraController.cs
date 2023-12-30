using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    #region Serialize Varialbles
    [SerializeField] float _speed;
    [SerializeField] Vector3 _offset;
    [SerializeField] float _force;
    [SerializeField] float downAngle;
    [SerializeField] float _maxDownDrag;
    [SerializeField] Text _powerIndicatorText;

    #endregion

    #region Private Variables

    Transform _target;
    GameObject ball, _gameManager;
    float _horizontalInput, _saveMousePosition;
    bool _isShooting = false;
    AudioSource _audioSource;

    #endregion


    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("GameManager");
        ResetCamera();
    }

    // Update is called once per frame
    void Update()
    {
        FindWhiteBall();
        Movement();
        ShootWhiteBall();
    }

    void FindWhiteBall()
    {
        ball = GameObject.FindGameObjectWithTag("WhiteBall");
        if (ball != null)
        {
            _target = ball.transform;
        }
        else
        { Debug.LogError("ball is not found"); }
    }

    public void Movement()
    {
        _horizontalInput = Input.GetAxis("Mouse X") * _speed * Time.deltaTime;
        transform.RotateAround(_target.position, Vector3.up, _horizontalInput);
    }

    public void ResetCamera()
    {
        transform.position = _target.position + _offset;
        transform.LookAt(_target.position);
        transform.localEulerAngles = new Vector3(downAngle, transform.localEulerAngles.y, 0);
    }

    public void ShootWhiteBall()
    {
        if (gameObject.GetComponent<Camera>().enabled)
        {
            if (Input.GetMouseButton(0) && _isShooting == false)
            {
                _saveMousePosition = 0f;
                _isShooting = true;
            }
            else if (_isShooting == true)
            {
                if (_saveMousePosition + Input.GetAxis("Mouse Y") <= 0)
                {
                    _saveMousePosition += Input.GetAxis("Mouse Y");

                    if (_saveMousePosition <= _maxDownDrag)
                    { _saveMousePosition = _maxDownDrag; }
                }

                float powerIndicator = Mathf.RoundToInt((_saveMousePosition / _maxDownDrag) * 100);
                _powerIndicatorText.text = ("Power = " + powerIndicator + "%");

                if (Input.GetMouseButtonUp(0) == true)
                {
                    Vector3 hitDirection = transform.forward;
                    hitDirection = new Vector3(hitDirection.x, 0, hitDirection.z).normalized;
                    ball.GetComponent<Rigidbody>().AddForce(hitDirection * Mathf.Abs(_saveMousePosition) * _force, ForceMode.Impulse);
                    _gameManager.GetComponent<GameManager>().SwitchingCameras();
                    _isShooting = false;
                }
            }
        }
    }
}
