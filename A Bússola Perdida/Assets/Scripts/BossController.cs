using UnityEngine;
using System.Collections;

public enum BossState
{
    Walk,
    Attack,
    Teleport,
    Cooldown
}

public class BossController : MonoBehaviour
{
    public BossState currentState;

    [Header("Movement")]
    public float speed = 2f;
    public Transform[] walkPointsBoss;
    private int currentPoint;

    [Header("Attack")]
    public GameObject powerBoss;
    public Transform firePoint;
    public float attackCooldown = 1.5f;

    [Header("Bullet Pool")]
    public GameObject[] bossBullets;


    [Header("Attack Flash")]
    public Color attackFlashColor = Color.red;
    public int attackFlashCount = 3;
    public float attackFlashInterval = 0.1f;

    [Header("Teleport")]
    public Transform[] teleportPoints;
    public float teleportDelay = 0.3f;

    [Header("Teleport Flash")]
    public int teleportFlashCount = 4;
    public float teleportFlashInterval = 0.1f;

    [Header("Player")]
    public Transform player;

    [Header("Vision")]
    public float visionRange = 10f;

    private float timer;
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;
    private Color originalColor;
    private bool isFlashing;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
    }

    void Start()
    {
        ChangeState(BossState.Walk);
    }

    void Update()
    {
        anim.SetBool("isWalking", currentState == BossState.Walk);

        if (currentState == BossState.Walk && PlayerInRange())
        {
            ChangeState(BossState.Attack);
            return;
        }

        switch (currentState)
        {
            case BossState.Walk:
                Walk();
                break;

            case BossState.Attack:
                if (!isFlashing)
                    StartCoroutine(AttackSequence());
                break;
        }
    }

    // ================= MOVIMENTO =================
    void Walk()
    {
        Transform target = walkPointsBoss[currentPoint];
        Vector2 direction = target.position - transform.position;

        if (direction.x != 0)
            sr.flipX = direction.x < 0;

        rb.MovePosition(Vector2.MoveTowards(
            rb.position,
            target.position,
            speed * Time.deltaTime
        ));

        if (Vector2.Distance(rb.position, target.position) < 0.1f)
            currentPoint = (currentPoint + 1) % walkPointsBoss.Length;
    }

    // ================= ATAQUE =================
    IEnumerator AttackSequence()
    {
        isFlashing = true;

        // Olha para o player
        if (player != null)
            sr.flipX = player.position.x < transform.position.x;

        // Pisca cor de aviso
        for (int i = 0; i < attackFlashCount; i++)
        {
            sr.color = attackFlashColor;
            yield return new WaitForSeconds(attackFlashInterval);

            sr.color = originalColor;
            yield return new WaitForSeconds(attackFlashInterval);
        }

        anim.SetTrigger("Attack");
        ShootAtPlayer();

        ChangeState(BossState.Teleport);
        StartCoroutine(Teleport());

        isFlashing = false;
    }

    void ShootAtPlayer()
    {
        if (player == null) return;

        Vector2 dir = (player.position - firePoint.position).normalized;

        foreach (GameObject bullet in bossBullets)
        {
            if (!bullet.activeInHierarchy)
            {
                bullet.transform.position = firePoint.position;
                bullet.transform.right = dir;
                bullet.SetActive(true);
                break;
            }
        }
    }


    // ================= TELEPORTE =================
    IEnumerator Teleport()
    {
        for (int i = 0; i < teleportFlashCount; i++)
        {
            sr.enabled = false;
            yield return new WaitForSeconds(teleportFlashInterval);

            sr.enabled = true;
            yield return new WaitForSeconds(teleportFlashInterval);
        }

        yield return new WaitForSeconds(teleportDelay);

        int index = Random.Range(0, teleportPoints.Length);
        transform.position = teleportPoints[index].position;

        ChangeState(BossState.Walk);
    }

    // ================= VISÃO =================
    bool PlayerInRange()
    {
        if (player == null) return false;

        float distance = Vector2.Distance(transform.position, player.position);
        return distance <= visionRange;
    }

    // ================= ESTADO =================
    void ChangeState(BossState newState)
    {
        currentState = newState;
        timer = 0f;
    }

    // ================= DEBUG =================
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, visionRange);
    }
}
