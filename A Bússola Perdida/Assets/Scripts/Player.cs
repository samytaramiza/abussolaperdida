using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movimento")]
    public float Speed;
    public float JumpForce;

    public bool isJumping;
    public bool doubleJump;

    [Header("Ataque")]
    public GameObject potionPrefab;
    public Transform attackPoint;
    public float attackCooldown = 0.5f;

    private float attackTimer;
    private Rigidbody2D rig;
    private Animator anim;

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
    }

    // ---------------- MOVIMENTO ----------------
    void Move()
    {
        float h = Input.GetAxis("Horizontal");
        transform.position += new Vector3(h, 0, 0) * Speed * Time.deltaTime;

        if (h > 0)
        {
            anim.SetBool("Walk", true);
            transform.eulerAngles = Vector3.zero;
        }
        else if (h < 0)
        {
            anim.SetBool("Walk", true);
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            anim.SetBool("Walk", false);
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
            }
            else if (doubleJump)
            {
                rig.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
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

            // INSTANCIA POÇÃO
            GameObject potionObj = Instantiate(potionPrefab, attackPoint.position, attackPoint.rotation);

            // PASSA A DIREÇÃO PARA O PROJÉTIL
            PotionProjectile potion = potionObj.GetComponent<PotionProjectile>();

            if (transform.eulerAngles.y == 0)
                potion.direcao = 1;   // direita
            else
                potion.direcao = -1;  // esquerda

            attackTimer = 0;
        }
    }

    void Morrer()
    {
        // coloque aqui qualquer ação de game over
        Destroy(gameObject); // ou chamar uma animação antes
    }


    // ---------------- COLISÕES ----------------
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == 8)
        {
            isJumping = false;
            anim.SetBool("Jump", false);
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.layer == 8)
        {
            isJumping = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Abismo"))
        {
            Debug.Log("Player caiu no abismo");
            Morrer();
        }
    }

}
