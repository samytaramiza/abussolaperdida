using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movimento")]
    public float Speed;
    public float JumpForce;

    public bool isJumping;
    public bool doubleJump;

    [Header("Ataque")]
    public GameObject arrowPrefab;     // Prefab da flecha
    public GameObject potionPrefab;    // Prefab da poção
    public Transform attackPoint;      // Posição de onde os ataques saem
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
        Vector3 moviment = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += moviment * Time.deltaTime * Speed;

        if (Input.GetAxis("Horizontal") > 0f)
        {
            anim.SetBool("Walk", true);
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
        else if (Input.GetAxis("Horizontal") < 0f)
        {
            anim.SetBool("Walk", true);
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
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
                rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
                doubleJump = true;
                anim.SetBool("Jump", true);
            }
            else if (doubleJump)
            {
                rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
                doubleJump = false;
            }
        }
    }

    // ---------------- ATAQUE ----------------
    void Attack()
    {
        attackTimer += Time.deltaTime;

        // ATAQUE 1 - Flecha (botão Z)
        if (Input.GetKeyDown(KeyCode.Z) && attackTimer >= attackCooldown)
        {
            anim.SetTrigger("Attack");
            Instantiate(arrowPrefab, attackPoint.position, attackPoint.rotation);
            attackTimer = 0;
        }

        // ATAQUE 2 - Poção (botão X)
        if (Input.GetKeyDown(KeyCode.X) && attackTimer >= attackCooldown)
        {
            anim.SetTrigger("Attack");
            Instantiate(potionPrefab, attackPoint.position, attackPoint.rotation);
            attackTimer = 0;
        }
    }

    // ---------------- COLISÕES ----------------
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            isJumping = false;
            anim.SetBool("Jump", false);
        }

        if (collision.gameObject.CompareTag("Perigo"))
        {
            GameController.instance.AlterarVida(-10f);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            isJumping = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Abismo"))
        {
            GameController.instance.ShowGameOver();
            Destroy(gameObject);
        }
    }
}
