using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Velocidade de movimento
    public float Speed;

    //Força do pulo
    public float JumpForce;

    //Controle de estado de pulo
    public bool isJumping; //true = no ar, false = no chão
    public bool doubleJump; //controla se o jogador pode dar um segundo pulo

    // Componentes
    private AudioSource sound; //(não usado ainda) para sons
    private Rigidbody2D rig; // corpo físico do jogador
    private Animator anim; // animações 

    void Start()
    {
        //Obtém a referência ao Rigidbody2D
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        //Chama as funções de movimento e pulo a cada frame
        Move();
        Jump();
    }

    //Movimentação lateral
    void Move()
    {
        //Captura o eixo horizontal (-1 a 1) e cria um vetor de movimento
        Vector3 moviment = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);

        //Move o personagem multiplicando pelo deltaTime e velocidade
        transform.position += moviment * Time.deltaTime * Speed;

        //Virar o sprite para a direita
        if (Input.GetAxis("Horizontal") > 0f)
        {
            anim.SetBool("Walk", true);
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }

        // Virar o sprite para a esquerda
        if (Input.GetAxis("Horizontal") < 0f)
        {
            anim.SetBool("Walk", true);
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }

        // Para quando o player estiver parado, rodar a animação idle
        if (Input.GetAxis("Horizontal") == 0f)
        {
            anim.SetBool("Walk", false);
        }
    }

    //Sistema de pulo com pulo duplo
    void Jump()
    {
        //Detecta se o botão de pulo foi pressionado
        if (Input.GetButtonDown("Jump"))
        {
            // Se está no chão
            if (!isJumping)
            {
                rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
                doubleJump = true; //habilita o segundo pulo
                anim.SetBool("Jump", true);
                AudioManager.Instance.PlayJump();               
            }
            else
            {
                //Se está no ar e ainda pode dar o segundo pulo
                if (doubleJump)
                {
                    rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
                    doubleJump = false; //desabilita o segundo pulo
                    //anim.SetBool("Jump", true);
                    AudioManager.Instance.PlayJump();  
                }
            }
        }
    }

    //Detecta colisões
    //Detecta colisões normais com chão ou perigos
    void OnCollisionEnter2D(Collision2D collision)
    {
        //Layer 8 = chão
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

    //Detecta triggers (como o abismo)
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Abismo"))
        {
            GameController.instance.ShowGameOver(); //mostra tela de game over
            Destroy(gameObject); //remove o jogador
        }
    }

    //Detecta quando o jogador sai do chão
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            isJumping = true; //marca que está no ar
        }
    }

}
