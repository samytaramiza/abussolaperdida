using UnityEngine;

public class BossController : MonoBehaviour
{
    public float speed = 2f;
    public float distanciaAtaque = 2.5f;
    public float tempoEntreAtaques = 2f;

    private float proximoAtaque;
    private Transform player;
    private Animator anim;
    private Rigidbody2D rb;

    private bool atacando = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float distancia = Vector2.Distance(transform.position, player.position);

        // ---- Se está atacando, não anda ----
        if (atacando)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        // ---- Se está no RANGE do ataque ----
        if (distancia <= distanciaAtaque)
        {
            rb.linearVelocity = Vector2.zero;
            anim.SetBool("isWalking", false);

            if (Time.time >= proximoAtaque)
            {
                Atacar();
            }

            return;
        }

        // ---- ANDAR ----  
        Vector2 direcao = (player.position - transform.position).normalized;
        rb.linearVelocity = new Vector2(direcao.x * speed, rb.linearVelocity.y);

        anim.SetBool("isWalking", true);
    }

    void Atacar()
    {
        atacando = true;
        proximoAtaque = Time.time + tempoEntreAtaques;

        anim.SetBool("isWalking", false);

        // TRIGGER DA ANIMAÇÃO
        anim.SetTrigger("Attack");
    }

    // Chamado pelo EVENTO dentro da animação
    public void Shoot()
    {
        // Aqui você instancia o poder do Boss
        Debug.Log("Boss lançou feitiço!");
    }

    // Chamado NO FINAL da animação (EVENT no último frame)
    public void EndAttack()
    {
        atacando = false;
        anim.ResetTrigger("Attack");
    }
}
