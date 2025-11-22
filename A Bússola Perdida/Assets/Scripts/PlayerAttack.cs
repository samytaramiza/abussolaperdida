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
    public float potionForce = 8f;

    void Update()
    {
        // ATAQUE 1: FLECHA (tecla Z)
        if (Input.GetKeyDown(KeyCode.Z))
        {
            anim.SetTrigger("Attack");
            ShootArrow();
        }

        // ATAQUE 2: POÇÃO (tecla X)
        if (Input.GetKeyDown(KeyCode.X))
        {
            anim.SetTrigger("Attack");
            ThrowPotion();
        }
    }

    void ShootArrow()
    {
        GameObject arrow = Instantiate(arrowPrefab, attackPoint.position, attackPoint.rotation);
        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();

        Vector2 dir = transform.right;
        if (transform.eulerAngles.y == 180f) dir = -transform.right;

        rb.linearVelocity = dir * arrowSpeed;
    }

    void ThrowPotion()
    {
        GameObject potion = Instantiate(potionPrefab, attackPoint.position, attackPoint.rotation);
        Rigidbody2D rb = potion.GetComponent<Rigidbody2D>();

        Vector2 dir = transform.right;
        if (transform.eulerAngles.y == 180f) dir = -transform.right;

        rb.AddForce(new Vector2(dir.x * potionForce, 5f), ForceMode2D.Impulse);
    }
}
