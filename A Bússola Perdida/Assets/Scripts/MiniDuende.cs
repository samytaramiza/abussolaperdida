using UnityEngine;

public class MiniDuende : MonoBehaviour
{
    public float velocidade = 2f; //Velocidade de movimentação do inimigo
    public float dano; //Quantidade de dano causado ao jogador
    public float tempoDano = 1f; //Tempo mínimo entre cada dano (em segundos)
    
    //Pontos entre os quais o inimigo patrulha (marcados no editor)
    public Transform pontoEsquerdo;
    public Transform pontoDireito;

    private int direcao = 1; //1 = direita, -1 = esquerda
    private float tempoProximoDano; //Armazena o tempo do próximo ataque permitido
    private Rigidbody2D rb; //Referência ao componente de física do inimigo

    //Chamado uma vez quando o objeto é criado
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //Pega o Rigidbody2D do inimigo
        tempoProximoDano = 0f; //Zera o tempo inicial de dano
    }

    //Chamado a cada frame
    void Update()
    {
        //Movimento lateral constante na direção atual
        rb.linearVelocity = new Vector2(direcao * velocidade, rb.linearVelocity.y);
        
        //Verifica se o inimigo chegou ao limite direito e inverte a direção
        if (direcao == 1 && transform.position.x >= pontoDireito.position.x)
        {
            MudarDirecao(-1);
        }
        //Verifica se o inimigo chegou ao limite esquerdo e inverte a direção
        else if (direcao == -1 && transform.position.x <= pontoEsquerdo.position.x)
        {
            MudarDirecao(1);
        }
    }   

    //Função que muda a direção de movimento e também vira o sprite do inimigo
    void MudarDirecao(int novaDirecao)
    {
        direcao = novaDirecao;

        //Inverte a escala no eixo X para o inimigo "olhar" para o lado correto
        Vector3 escala = transform.localScale;
        escala.x = Mathf.Abs(escala.x) * direcao;
        transform.localScale = escala;
    }

    //Detecta colisão contínua com outro objeto (enquanto permanece tocando)
    private void OnCollisionStay2D(Collision2D collision)
    {
        //Se colidir com o jogador...
        if (collision.gameObject.CompareTag("Player"))
        {
            //Verifica se já passou o tempo de espera entre danos
            if (Time.time >= tempoProximoDano)
            {
                //Acessa o script PlayerVida do jogador
                PlayerVida jogador = collision.gameObject.GetComponent<PlayerVida>();
                if (jogador != null)
                {
                    //Aplica o dano ao jogador
                    jogador.TomarDano(dano);
                }

                //Atualiza o tempo para o próximo dano
                tempoProximoDano = Time.time + tempoDano;
            }
        }
    }

    //Método visual que desenha uma linha entre os pontos de patrulha no editor da Unity
    private void OnDrawGizmosSelected()
    {
        if (pontoEsquerdo != null && pontoDireito != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(pontoEsquerdo.position, pontoDireito.position);
        }
    }
}
