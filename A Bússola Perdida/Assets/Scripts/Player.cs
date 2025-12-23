using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movimento")]
    public float Speed;
    public float JumpForce;

    public bool isJumping;
    public bool doubleJump;

    [Header("Ataque")]
    public Transform attackPoint;
    public float attackCooldown = 0.5f;

    private float attackTimer;
    private Rigidbody2D rig;
    private Animator anim;

    [Header("Dano por perigo")]
    public float danoPerigo = 20f;
    public float tempoEntreDano = 1f;
    private float danoTimer;

    private bool facingRight = true; // controla direção do player

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
        Jump();
        Attack();

        danoTimer += Time.deltaTime;
        attackTimer += Time.deltaTime;
    }

    // ---------------- MOVIMENTO HORIZONTAL ----------------
    void Move()
    {
        float h = Input.GetAxis("Horizontal");

        transform.position += new Vector3(h, 0, 0) * Speed * Time.deltaTime;
        anim.SetBool("Walk", h != 0);

        if (h > 0 && !facingRight)
            Flip();
        else if (h < 0 && facingRight)
            Flip();
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    // ---------------- PULO ----------------
    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (!isJumping)
            {
                rig.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
                isJumping = true;
                doubleJump = true;
                anim.SetBool("Jump", true);
                AudioManager.Instance.PlayJump();
            }
            else if (doubleJump)
            {
                rig.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
                doubleJump = false;
                AudioManager.Instance.PlayJump();
            }
        }
    }

    // ---------------- ATAQUE (OBJECT POOL) ----------------
    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.X) && attackTimer >= attackCooldown)
        {
            if (!GameController.instance.TemPocao())
                return;

            anim.SetTrigger("Attack");

            GameObject potionObj = PotionPool.Instance.GetPotion();
            potionObj.transform.position = attackPoint.position;
            potionObj.transform.rotation = Quaternion.identity;
            potionObj.SetActive(true);

            PotionProjectile potion = potionObj.GetComponent<PotionProjectile>();
            potion.direcao = facingRight ? 1 : -1;

            GameController.instance.UsarPocao();
            attackTimer = 0f;
        }
    }

    // ---------------- COLISÕES ----------------
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == 8) // chão
        {
            isJumping = false;
            anim.SetBool("Jump", false);
        }

        if (col.collider.CompareTag("Perigo"))
            TomarDanoPerigo();
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.collider.CompareTag("Perigo"))
            TomarDanoPerigo();
    }

    // ---------------- DANO CONTÍNUO ----------------
    void TomarDanoPerigo()
    {
        if (danoTimer >= tempoEntreDano)
        {
            GameController.instance.AlterarVida(-danoPerigo);
            danoTimer = 0f;
        }
    }
}
