using UnityEngine;

public class BossController : MonoBehaviour
{
    [Header("Referências")]
    public Transform player; // Referência ao jogador
    public Transform firePoint; // Ponto de onde o chefe lança o poder
    public GameObject bossPowerPrefab; // Prefab do poder do chefe (BossPower)

    [Header("Configurações de Ataque")]
    public float attackCooldown = 2f; // Tempo entre ataques
    public float attackSpeed = 10f; // Velocidade do projétil
    public float predictiveOffset = 1.5f; // Deslocamento preditivo do alvo

    [Header("Feedback Visual e Efeitos")]
    public Renderer bossRenderer; // Renderer do corpo do chefe
    public Color defaultColor = Color.white;
    public Color attackColor = Color.cyan;
    public float colorFlashTime = 0.2f; // Tempo que o chefe fica colorido durante ataque

    [Header("Teleporte")]
    public Transform[] teleportPoints; // Pontos possíveis de teleporte
    public float teleportCooldown = 5f; // Tempo entre teletransportes

    [Header("Câmera")]
    public CameraShake cameraShake; // Referência ao script que treme a câmera

    private float attackTimer;

    private float teleportTimer;
    private bool alternatePattern;

    void Start()
    {
        attackTimer = attackCooldown;
        teleportTimer = teleportCooldown;
        bossRenderer.material.color = defaultColor;
    }

    void Update()
    {
        if (player == null) return;

        attackTimer -= Time.deltaTime;
        teleportTimer -= Time.deltaTime;

        // Ataque
        if (attackTimer <= 0f)
        {
            Attack();
            attackTimer = attackCooldown;
        }

        // Teleporte (sem animação — apenas piscar e mudar posição)
        if (teleportTimer <= 0f)
        {
            StartCoroutine(Teleport());
            teleportTimer = teleportCooldown;
        }
    }

    void Attack()
    {
        StartCoroutine(FlashColor());
        cameraShake.Shake(0.1f, 0.2f); // Tremer leve ao atacar

        Vector3 targetPos = player.position;

        if (alternatePattern)
        {
            // Ataque preditivo — tenta prever onde o jogador estará
            targetPos += player.forward * predictiveOffset;
        }

        Vector3 direction = (targetPos - firePoint.position).normalized;

        GameObject power = Instantiate(bossPowerPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = power.GetComponent<Rigidbody2D>();
        rb.linearVelocity = direction * attackSpeed;

        alternatePattern = !alternatePattern; // Alterna o padrão de ataque
    }

    System.Collections.IEnumerator FlashColor()
    {
        bossRenderer.material.color = attackColor;
        yield return new WaitForSeconds(colorFlashTime);
        bossRenderer.material.color = defaultColor;
    }

    System.Collections.IEnumerator Teleport()
    {
        // Piscar antes de sumir
        bossRenderer.enabled = false;
        yield return new WaitForSeconds(0.2f);

        // Escolhe ponto aleatório
        Transform newPoint = teleportPoints[Random.Range(0, teleportPoints.Length)];
        transform.position = newPoint.position;

        // Piscar ao reaparecer
        bossRenderer.enabled = true;
        yield return new WaitForSeconds(0.1f);
        bossRenderer.material.color = attackColor;
        yield return new WaitForSeconds(0.1f);
        bossRenderer.material.color = defaultColor;

        cameraShake.Shake(0.15f, 0.3f); // Tremer a câmera ao reaparecer
    }
}
