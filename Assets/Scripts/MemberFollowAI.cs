using Unity.VisualScripting;
using UnityEngine;

public class MemberFollowAI : MonoBehaviour
{
    [SerializeField] private Transform followTarget;
    [SerializeField] private int speed;
    
    private float followDist;
    private float sprintDist;
    private float sprintMutliplier = 1;
    private Animator anim;
    private SpriteRenderer spriteRenderer;

    private const string IS_WALKING_PARAM = "IsWalking";

    [Header("Footsteps")]
    public AudioClip[] FootstepsClips;
    public float stepInterval = 0.3f;

    private AudioSource audioSource;
    private float stepTimer = 0.0f;
    private bool isSprinting;
    private bool isWalking;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        followTarget = GameObject.FindFirstObjectByType<CS_PlayerController>().transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, followTarget.position) > followDist)
        {
            isWalking = true;
            // multiply speed by 3 if distance from player to follower > then sprintDistance
            if (Vector3.Distance(transform.position, followTarget.position) > sprintDist)
            {
                sprintMutliplier = 3;
                isSprinting = true;

            }
            else if (Vector3.Distance(transform.position, followTarget.position) !> sprintDist)
            {
                sprintMutliplier = 1;
                isSprinting = false;
            }
            // walk to player
            anim.SetBool(IS_WALKING_PARAM, true);
            float step = speed * sprintMutliplier * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, followTarget.position, step);

            if (followTarget.position.x - transform.position.x < 0)
            {
                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipX = false;
            }
        }
        else
        {
            // stop walking + return to idle
            anim.SetBool(IS_WALKING_PARAM, false);
            isWalking = false; 
            sprintMutliplier = 1;
        }

        HandleFootsteps();
    }

    public void SetFollowDistance(float followDistance)
    {
        followDist = followDistance;
        sprintDist = followDist * 3;
    }
    private void HandleFootsteps()
    {

        if (!isWalking)
        {
            stepTimer = 0f;
            return;
        }

        float currentStepInterval = stepInterval;

        if (isSprinting)
        {
            currentStepInterval *= 0.6f;
        }

        stepTimer -= Time.deltaTime;

        if (stepTimer <= 0f)
        {
            PlayFootsteps();
            stepTimer = currentStepInterval;
        }
    }

    private void PlayFootsteps()
    {
        if (FootstepsClips == null || FootstepsClips.Length == 0 || audioSource == null) return;

        AudioClip clip = FootstepsClips[Random.Range(0, FootstepsClips.Length)];

        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.PlayOneShot(clip);
    }
}
