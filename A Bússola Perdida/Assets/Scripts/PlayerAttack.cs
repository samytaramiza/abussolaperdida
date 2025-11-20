using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Referências")]
    public GameObject arrowPrefab;
    public GameObject potionPrefab;
    public Transform attackPoint;
    public Animator anim;

    [Header("Configuração")]
    public float arrowSpeed = 12f;
    public float potionSpeed = 8f;
    public float attackCooldown = 0.4f;

    private float attackTimer = 0f;

    void Update()
    {
        attackTimer += Time.deltaTime;

        // FLECHA - Z
        if (Input.GetKeyDown(KeyCode.Z) && attackTimer >= attackCooldown)
        {
            anim.SetTrigger("Attack");
            Shoot(arrowPrefab, arrowSpeed);
            attackTimer = 0f;
        }

        // POÇÃO - X
        if (Input.GetKeyDown(KeyCode.X) && attackTimer >= attackCooldown)
        {
            anim.SetTrigger("Attack");
            Shoot(potionPrefab, potionSpeed);
            attackTimer = 0f;
        }
    }

    void Shoot(GameObject prefab, float speed)
    {
        if (prefab == null || attackPoint == null)
            return;

        GameObject obj = Instantiate(prefab, attackPoint.position, attackPoint.rotation);
        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();

        // Direção baseada no lado que o player está virado
        Vector2 direction =
            transform.eulerAngles.y == 180f ? Vector2.left : Vector2.right;

        rb.linearVelocity = direction * speed;
    }
}
