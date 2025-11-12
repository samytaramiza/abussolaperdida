/*using UnityEngine;

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

    private float attackTimer;using UnityEngine;

public class ParallaxComLoopInfinito : MonoBehaviour
{
    public float efeitoParallax = 0.5f; // 0 = mais distante, 1 = acompanha a câmera
    public float suavizacao = 1f;       // Suaviza o movimento do fundo (opcional)

    private Transform cam;              // Referência à câmera principal
    private Vector3 ultimaPosCam;       // Guarda a posição anterior da câmera

    private Transform[] camadas;        // Array com as duas imagens do fundo
    private float tamanhoSprite;        // Largura da imagem

    void Start()
    {
        cam = Camera.main.transform;
        ultimaPosCam = cam.position;

        // Obtém todas as camadas (filhas do objeto)
        camadas = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            camadas[i] = transform.GetChild(i);
        }

        // Usa a largura do sprite para saber quando reposicionar
        SpriteRenderer sr = camadas[0].GetComponent<SpriteRenderer>();
        if (sr != null)
            tamanhoSprite = sr.bounds.size.x;
    }

    void LateUpdate()
    {
        // Calcula o quanto a câmera se moveu
        float deltaX = (cam.position.x - ultimaPosCam.x) * efeitoParallax;

        // Move o fundo suavemente
        transform.position -= new Vector3(deltaX, 0, 0);

        ultimaPosCam = cam.position;

        // Verifica se a câmera passou do ponto de loop
        foreach (Transform camada in camadas)
        {
            float distCam = cam.position.x - camada.position.x;

            // Se a câmera ultrapassou a borda direita da imagem
            if (distCam > tamanhoSprite)
            {
                camada.position += new Vector3(tamanhoSprite * camadas.Length, 0, 0);
            }
            // Se voltou pra esquerda (caso o player retorne)
            else if (distCam < -tamanhoSprite)
            {
                camada.position -= new Vector3(tamanhoSprite * camadas.Length, 0, 0);
            }
        }
    }
}

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
}*/
