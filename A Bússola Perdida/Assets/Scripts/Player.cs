using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movimento")]
    public float Speed;
    public float JumpForce;

    public bool isJumping;
    public bool doubleJump;

    private int facingDirection = 1; // 1 = direita | -1 = esquerda
    private Vector3 originalScale;

    [Header("Ataque")]
    public GameObject potionPrefab;
    public Transform attackPoint;
    public float attackCooldown = 0.5f;

    private float attackTimer;
    private Rigidbody2D rig;
    private Animator anim;

    [Header("DANO POR PERIGO")]
    public float danoPerigo = 20f;
    public float tempoEntreDano = 1f;
    private float danoTimer = 0f;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        // Guarda o scale original para não alterar o tamanho
        originalScale = transform.localScale;
    }

    void Update()
    {
        Move();
        Jump();
        Attack();

        danoTimer += Time.deltaTime;
    }

    // ---------------- MOVIMENTO ----------------
    void Move()
    {
        float h = Input.GetAxis("Horizontal");

        // Movimento horizontal
        transform.position += new Vector3(h, 0, 0) * Speed * Time.deltaTime;

        anim.SetBool("Walk", h != 0);

        // Direção correta (sem alterar tamanho)
        if (h > 0)
        {
            facingDirection = 1;
            transform.localScale = new Vector3(
                Mathf.Abs(originalScale.x),
                originalScale.y,
                originalScale.z
            );
        }
        else if (h < 0)
        {
            facingDirection = -1;
            transform.localScale = new Vector3(
                -Mathf.Abs(originalScale.x),
                originalScale.y,
                originalScale.z
            );
        }
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
                AudioManager.Instance.PlayJump();
                doubleJump = false;
            }
        }
    }

    // ---------------- ATAQUE ----------------
    void Attack()
    {
        attackTimer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.X) && attackTimer >= attackCooldown)
        {
            anim.SetTrigger("Attack");

            GameObject potionObj = Instantiate(
                potionPrefab,
                attackPoint.position,
                Quaternion.identity
            );

            PotionProjectile potion = potionObj.GetComponent<PotionProjectile>();
            potion.direcao = facingDirection;

            attackTimer = 0f;
        }
    }

    // ---------------- COLISÕES ----------------
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == 8)
        {
            isJumping = false;
            anim.SetBool("Jump", false);
        }

        if (col.collider.CompareTag("Perigo"))
        {
            TomarDanoPerigo();
        }
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.collider.CompareTag("Perigo"))
        {
            TomarDanoPerigo();
        }
    }

    // ---------------- DANO POR PERIGO ----------------
    void TomarDanoPerigo()
    {
        if (danoTimer >= tempoEntreDano)
        {
            GameController.instance.AlterarVida(-danoPerigo);
            danoTimer = 0f;
        }
    }
}
