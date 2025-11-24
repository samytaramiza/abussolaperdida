using UnityEngine;

public class BossController : MonoBehaviour
{
    [Header("Referências")]
    public Transform player;
    public Transform firePoint;
    public GameObject bossPowerPrefab;

    [Header("Configurações de Movimento")]
    public float moveSpeed = 2f;
    public float stoppingDistance = 3f;

    [Header("Configurações de Ataque")]
    public float attackCooldown = 2f;
    public float attackSpeed = 10f;

    [Header("Animações")]
    private Animator anim;
    private bool isAttacking = false;
    private float attackTimer;

    private Rigidbody2D rb;


    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        attackTimer = attackCooldown;
    }

    void Update()
    {
        if (player == null) return;

        // Timer do ataque
        attackTimer -= Time.deltaTime;

        // Se não está atacando → pode andar
        if (!isAttacking)
        {
            Movimento();
        }

        // Se o player está perto o suficiente → atacar
        float distancia = Vector2.Distance(transform.position, player.position);

        if (distancia <= stoppingDistance && attackTimer <= 0f)
        {
            Atacar();
        }

        // Faz o boss olhar para o player sempre
        FlipTowardsPlayer();
    }


    // ---------------- MOVIMENTO ----------------
    void Movimento()
    {
        float distancia = Vector2.Distance(transform.position, player.position);

        if (distancia > stoppingDistance)
        {
            anim.SetBool("Walk", true);

            Vector2 novaPos = Vector2.MoveTowards(
                transform.position,
                player.position,
                moveSpeed * Time.deltaTime
            );

            rb.MovePosition(novaPos);
        }
        else
        {
            anim.SetBool("Walk", false);
        }
    }


    // ---------------- ATAQUE ----------------
    void Atacar()
    {
        isAttacking = true;
        anim.SetTrigger("Attack");
    }


    // Chamado pelo Animation Event no final da animação "Attack"
    public void EndAttack()
    {
        // Instancia o projétil
        GameObject power = Instantiate(bossPowerPrefab, firePoint.position, firePoint.rotation);

        // Define direção até o jogador
        Vector2 dir = (player.position - firePoint.position).normalized;

        Rigidbody2D rbPower = power.GetComponent<Rigidbody2D>();
        rbPower.linearVelocity = dir * attackSpeed;

        // Reseta estado
        isAttacking = false;
        attackTimer = attackCooldown;
    }


    // ---------------- VIRAR PARA O PLAYER ----------------
    void FlipTowardsPlayer()
    {
        if (player.position.x > transform.position.x)
        {
            transform.eulerAngles = Vector3.zero;
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }
}
