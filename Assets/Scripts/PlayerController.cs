//using System.Numerics;
using UnityEditor.Callbacks;
using UnityEngine;

public class CS_PlayerController : MonoBehaviour
{
    [SerializeField] private int _speed;

    private PlayerControls _playerControls;
    private Rigidbody _rb;
    private Vector3 _movement;

    private void Awake()
    {
        _playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }

    private void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody>();


    }

    // Update is called once per frame
    void Update()
    {
        float x = _playerControls.Player.Move.ReadValue<Vector2>().x;
        float z = _playerControls.Player.Move.ReadValue<Vector2>().y;

        Debug.Log(x + "," + z);

        _movement = new Vector3(x, 0, z).normalized;
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(transform.position + _movement * _speed * Time.fixedDeltaTime);
    }
}
