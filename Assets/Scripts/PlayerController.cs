//using System.Numerics;
using UnityEditor.Callbacks;
using UnityEngine;

public class CS_PlayerController : MonoBehaviour
{
    [SerializeField] private int _speed;
    [SerializeField] private Animator _anim;
    [SerializeField] private SpriteRenderer _playerSprite;

    private PlayerControls _playerControls;
    private Rigidbody _rb;
    private Vector3 _movement;

    private const string IS_WALKING_PARAM = "IsWalking";

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

        _movement = new Vector3(x, 0, z).normalized;

        _anim.SetBool(IS_WALKING_PARAM, _movement != Vector3.zero);

        if (x != 0 && x < 0)
        {
            _playerSprite.flipX = true;
        }

        if (x != 0 && x > 0)
        {
            _playerSprite.flipX = false;
        }
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(transform.position + _movement * _speed * Time.fixedDeltaTime);
    }
}
