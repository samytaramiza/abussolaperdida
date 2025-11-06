using UnityEngine;

public class miniDuende : MonoBehaviour
{
    public  float velocidade = 2f; // velocidade
    public float dano = 20;
    public float tempoDano = 1f;
    public Transform pontoEsquerdo;
    public Transform pontoDireito;

    private int direcao = 1; //1 = direita, -1 =
    private float tempoProximoDano;
    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tempoProximoDano = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        // Movimento lateral
        rb.linearVelocity = new Vector2(direcao * velocidade, rb.linearVelocity.y);
        
        // Verifica limites e troca direção
        if(direcao == 1 && transform.position.x >= pontoDireito.position.x){
            MudarDirecao(-1);
        }
        else if(direcao == -1 && transform.position.x <= pontoEsquerdo.position.x){
            MudarDirecao(1);
        }
    }   

    void MudarDirecao(int novaDirecao)
    {
        direcao = novaDirecao;

        // Inverter a escala para o inimigo "virar" visualmente
        Vector3 escala = transform.localScale;
        escala.x = Mathf.Abs(escala.x) * direcao;
        transform.localScale = escala;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Causa dano se passou tempo suficiente desde o último
            if (Time.time >= tempoProximoDano)
            {
                PlayerVida jogador = collision.collider.GetComponentInParent<PlayerVida>();

                if (jogador != null)
                {
                    jogador.TomarDano(dano);
                }
                tempoProximoDano = Time.time + tempoDano;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Mostra visualmente os pontos no editor
        if (pontoEsquerdo != null && pontoDireito != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(pontoEsquerdo.position, pontoDireito.position);
        }
    }

}

