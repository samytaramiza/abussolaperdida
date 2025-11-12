using UnityEngine;

public class BossPower : MonoBehaviour
{
    //CONFIGURAÇÕES DO PODER 
    public float lifeTime = 3f; //Tempo que o poder permanece ativo na cena antes de ser destruído
    public GameObject hitEffect; //Efeito visual (partículas, explosão, faíscas, etc.) ao colidir com algo


    void Start()
    {
        //Destroi automaticamente o projétil após 'lifeTime' segundos
        //Isso evita que o objeto fique eternamente na cena caso não colida com nada
        Destroy(gameObject, lifeTime);
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        //Quando o poder colide com algo (por exemplo, o jogador, o chão ou parede)

        //Se houver um efeito de impacto configurado, ele é criado na posição atual do poder
        if (hitEffect != null)
            Instantiate(hitEffect, transform.position, Quaternion.identity);

        //Destroi o poder logo após a colisão (independente de com o que colidiu)
        Destroy(gameObject);
    }
}
