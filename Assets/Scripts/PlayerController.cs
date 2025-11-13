//using System.Numerics;
//using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CS_PlayerController : MonoBehaviour
{
    [SerializeField] private int _speed;
    [SerializeField] private Animator _anim;
    [SerializeField] private SpriteRenderer _playerSprite;
    [SerializeField] private LayerMask _grassLayer;
    [SerializeField] private int _stepsInGrass;
    [SerializeField] private int _minStepsToEncounter;
    [SerializeField] private int _maxStepsToEncounter;



    private PlayerControls _playerControls;
    private Rigidbody _rb;
    private Vector3 _movement;
    private bool _movingInGrass;
    private float _stepTimer;
    private int _stepsToEncounter;

    private const string IS_WALKING_PARAM = "IsWalking";
    private const string BATTLE_SCENE = "BattleScene";
    private const float TIME_PER_STEP = 0.5f;

    private void Awake()
    {
        _playerControls = new PlayerControls();
        CalculateStepsToNextEncounter();
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

        Collider[] colliders = Physics.OverlapSphere(transform.position, 1, _grassLayer);
        _movingInGrass = colliders.Length != 0 && _movement != Vector3.zero;

        if (_movingInGrass == true)
        {
            _stepTimer += Time.fixedDeltaTime;
            if (_stepTimer > TIME_PER_STEP)
            {
                _stepsInGrass++;
                _stepTimer = 0;

                if (_stepsInGrass>=_stepsToEncounter)
                {
                    SceneManager.LoadScene(BATTLE_SCENE);
                }
            }
        }
    }

    private void CalculateStepsToNextEncounter()
    {
        _stepsToEncounter = Random.Range(_minStepsToEncounter, _maxStepsToEncounter);
    }


}
