using UnityEngine;

public class MemberFollowAI : MonoBehaviour
{
    [SerializeField] private Transform followTarget;
    [SerializeField] private int speed;
    
    private float followDist;
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
            // walk to player
            anim.SetBool(IS_WALKING_PARAM, true);
            float step = speed * Time.deltaTime;
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
        }
    }

    public void SetFollowDistance(float followDistance)
    {
        followDist = followDistance;
    }
}
