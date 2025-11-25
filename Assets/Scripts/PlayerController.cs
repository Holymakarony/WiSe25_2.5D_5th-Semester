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
    private PartyManager partyManager;

    private const string IS_WALKING_PARAM = "IsWalking";
    private const string BATTLE_SCENE = "BattleScene";
    private const float TIME_PER_STEP = 0.5f;
    private const int SPRINT_MULTIPLIER = 3;

    private void Awake()
    {
        _playerControls = new PlayerControls();
        CalculateStepsToNextEncounter();
    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }

    private void OnDisable()
    {
        _playerControls.Disable();
    }

    private void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody>();
        partyManager = GameObject.FindFirstObjectByType<PartyManager>();

        if(partyManager.GetPosition() != Vector3.zero) // if there is a saved position for the player when the player controller loads in
        {
            transform.position = partyManager.GetPosition(); // move player to saved position
        }
    }

    // Update is called once per frame
    void Update()
    {
        float x = _playerControls.Player.Move.ReadValue<Vector2>().x;
        float z = _playerControls.Player.Move.ReadValue<Vector2>().y;

        _movement = new Vector3(x, 0, z).normalized;

        if (_playerControls.Player.Sprint.IsPressed() == true)
        {
            Sprint();
        }
        else
        {
            _movement = new Vector3(x, 0, z).normalized;
        }

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
                    partyManager.SetPosition(transform.position);  // save player position before encounter starts
                    SceneManager.LoadScene(BATTLE_SCENE);
                }
            }
        }
    }

    private void CalculateStepsToNextEncounter()
    {
        _stepsToEncounter = Random.Range(_minStepsToEncounter, _maxStepsToEncounter);
    }

    private void Sprint()
    {
        _movement = _movement * SPRINT_MULTIPLIER;
    }
}
