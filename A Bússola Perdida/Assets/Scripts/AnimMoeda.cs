using UnityEngine;

public class AnimMoeda : MonoBehaviour
{
    //Velocidade de rotação da moeda
    public int velocidadeGiro = 50;

    //Start é chamado no início do jogo (não usado aqui)
    void Start()
    {
        
    }

    //Detecta quando outro objeto entra no trigger da moeda
    private void OnTriggerEnter(Collider other)
    {
        //Se o objeto que entrou no trigger for o jogador...
        if (other.tag == "Player")
        {
            //Destroi a moeda (coleta)
            Destroy(gameObject);
        }
    }

    //Update é chamado a cada frame
    void Update()
    {
        //Faz a moeda girar no eixo Y (rotação no espaço do mundo)
        transform.Rotate(Vector3.up * velocidadeGiro * Time.deltaTime, Space.World);
    }
}
