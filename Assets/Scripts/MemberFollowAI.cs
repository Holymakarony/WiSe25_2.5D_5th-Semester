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
            // multiply speed by 3 if distance from player to follower > then sprintDistance
            if (Vector3.Distance(transform.position, followTarget.position) > sprintDist){sprintMutliplier = 3;}
            else if (Vector3.Distance(transform.position, followTarget.position) !> sprintDist){sprintMutliplier = 1;}
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
            sprintMutliplier = 1;
        }
    }

    public void SetFollowDistance(float followDistance)
    {
        followDist = followDistance;
        sprintDist = followDist * 3;
    }
}
