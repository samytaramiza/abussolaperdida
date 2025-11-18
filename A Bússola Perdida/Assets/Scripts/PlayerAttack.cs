using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Referências")]
    public Transform attackPoint; //Local onde a flecha e poção saem
    public GameObject arrowPrefab; //Prefab da flecha
    public GameObject potionPrefab; //Prefab da poção

    [Header("Configurações de Ataque")]
    public float arrowSpeed = 10f; //Velocidade da flecha
    public float potionForce = 7f; // Força do lançamento da poção
    public float attackCooldown = 0.5f; //Tempo entre ataques

    private float cooldownTimer;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        cooldownTimer -= Time.deltaTime;

        // ATAQUE 1 — FLECHA — tecla Z
        if (Input.GetKeyDown(KeyCode.Z) && cooldownTimer <= 0f)
        {
            ShootArrow();
        }

        // ATAQUE 2 — POÇÃO — tecla X
        if (Input.GetKeyDown(KeyCode.X) && cooldownTimer <= 0f)
        {
            ThrowPotion();
        }
    }

    void ShootArrow()
    {
        cooldownTimer = attackCooldown;
        anim.SetTrigger("Attack");

        GameObject arrow = Instantiate(arrowPrefab, attackPoint.position, attackPoint.rotation);

        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.right * arrowSpeed * (transform.localScale.x > 0 ? 1 : -1);
    }

    void ThrowPotion()
    {
        cooldownTimer = attackCooldown;
        anim.SetTrigger("Attack2");

        GameObject potion = Instantiate(potionPrefab, attackPoint.position, attackPoint.rotation);

        Rigidbody2D rb = potion.GetComponent<Rigidbody2D>();

        Vector2 direction = new Vector2(transform.localScale.x > 0 ? 1 : -1, 1).normalized;

        rb.AddForce(direction * potionForce, ForceMode2D.Impulse);
    }
}
