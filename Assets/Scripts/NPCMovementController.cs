using UnityEngine;

public class NPCMovementController : MonoBehaviour
{
    // References
    internal Transform thisTransform;
    private CircleCollider2D aggroTrigger;
    private NPCInformation npcInfo;
    private EnemyController enemyController;

    // NPC Info
    internal Vector3 startPos;
    public bool isDead;

    // The movement speed of the object
    private float moveSpeed;

    // Switch if the NPC should wander
    [Header("Can the NPC wander?")]
    public bool canWander;
    private bool canWander_cache;

    // Restrict area of wandering from starting position
    [Header("Area of wandering:")]
    [Tooltip("Choose how far from the startpos the NPC can wander")]
    public float maxWanderLeft;
    public float maxWanderRight;
    public float maxWanderUp;
    public float maxWanderDown;

    // A minimum and maximum time delay for taking a decision, choosing a direction to move in
    [Header("How long between direction decisions:")]
    [Tooltip("Choose how long it takes between direction decisions")]
    public int decisionTimeMin;
    public int decisionTimeMax;
    internal float decisionTimeCount = 0;

    // The possible directions that the object can move int, right, left, up, down, and zero for staying in place. I added zero twice to give a bigger chance if it happening than other directions
    [Header("Choose direction:")]
    [Tooltip("Choose which way the NPC is allowed to go, -1 to 1 (left is -1 in X and 0,0,0 is idle, if you have more idle's its more likely that its idle)")]
    public Vector3[] wanderDirections = new Vector3[] { };
    internal int currentWanderDirection;

    // Chasing spesifics
    [Header("Chasing specifics:")]
    private Transform targetTransform;
    public bool isChasing;
    public float endChaseAtDistance;


    private void Awake()
    {
        // References
        enemyController = GetComponent<EnemyController>();
        npcInfo = GetComponent<NPCInformation>();
        aggroTrigger = GetComponentInChildren<CircleCollider2D>();
    }

    // Use this for initialization
    void Start()
    {
        // Initial variable settings
        isDead = enemyController.isDead;
        
        isChasing = false;
        canWander_cache = canWander;

        // Cache Start Position
        startPos = this.transform.position;

        // Cache the transform for quicker access
        thisTransform = this.transform;

        // Movement Speed

        moveSpeed = enemyController.movementSpeed;


        // Set a random time delay for taking a decision ( changing direction, or standing in place for a while )
        decisionTimeCount = UnityEngine.Random.Range(decisionTimeMin, decisionTimeMax);

        // Choose a movement direction, or stay in place
        ChooseMoveDirection();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        { HandleMovement(); }
    }

    public void Die()
    {
        isDead = true;
        if (isChasing)
        { EndChase(); }
        aggroTrigger.enabled = false;
    }

    private void HandleMovement()
    {
        if (canWander && !isChasing && !isDead)
        {
            // Wander around
            Wander();
        }
        else if (isChasing && !canWander && !isDead)
        {
            // Chase the player
            ChasePlayer();
            // If distance is too big between the NPC and player, stop chasing
            if (Vector2.Distance(thisTransform.position, targetTransform.position) > endChaseAtDistance)
            {
                EndChase();
            }
        }
        else if (!canWander && !isChasing && !isDead)
        {
            GoToSpawn();

            if (thisTransform.position == startPos)
            {
                canWander = canWander_cache;
            }
        }
    }

    public void StartChase(Transform targetToChase)
    {
        // Stops wandering and starts chasing the targetToChase
        canWander = false;
        isChasing = true;
        targetTransform = targetToChase;
    }

    void ChasePlayer()
    {
        // Move towards the target
        thisTransform.position = Vector2.MoveTowards(thisTransform.position, targetTransform.position, Time.deltaTime * moveSpeed);
    }

    void EndChase()
    {
        // Stops the chasing
        isChasing = false;
        targetTransform = null;
    }

    void GoToSpawn()
    {
        // Moves back to the start position
        thisTransform.position = Vector2.MoveTowards(thisTransform.position, startPos, Time.deltaTime * moveSpeed);
    }

    private void Wander()
    {
        // Move the object in the chosen direction at the set speed
        thisTransform.position += wanderDirections[currentWanderDirection] * Time.deltaTime * moveSpeed;

        // Check if the character is trying to wander out of restricted area
        CheckWanderingArea();

        if (decisionTimeCount > 0)
        {
            decisionTimeCount -= Time.deltaTime;
        }
        else
        {
            // Choose a random time delay for taking a decision ( changing direction, or standing in place for a while )
            decisionTimeCount = UnityEngine.Random.Range(decisionTimeMin, decisionTimeMax);

            // Choose a movement direction, or stay in place
            ChooseMoveDirection();
        }
    }

    void ChooseMoveDirection()
    {
        // Choose whether to move sideways or up/down
        currentWanderDirection = Mathf.FloorToInt(UnityEngine.Random.Range(0, wanderDirections.Length));
    }

    void CheckWanderingArea()
    {
        // Restricting NPC to not wander of too long from the start position
        if (thisTransform.position.x > (startPos.x + maxWanderRight) || thisTransform.position.x < (startPos.x - maxWanderLeft) || thisTransform.position.y > (startPos.y + maxWanderUp) || thisTransform.position.y < (startPos.y - maxWanderDown))
        {
            decisionTimeCount = 0;
        }
    }
}