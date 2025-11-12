using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float efeitoParallax = 0.5f; //0 = mais distante, 1 = acompanha a câmera
    public float suavizacao = 1f; //Suaviza o movimento do fundo (opcional)

    private Transform cam; //Referência à câmera principal
    private Vector3 ultimaPosCam; //Guarda a posição anterior da câmera

    private Transform[] camadas; //Array com as duas imagens do fundo
    private float tamanhoSprite; //Largura da imagem

    void Start()
    {
        cam = Camera.main.transform;
        ultimaPosCam = cam.position;

        //Obtém todas as camadas (filhas do objeto)
        camadas = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            camadas[i] = transform.GetChild(i);
        }

        //Usa a largura do sprite para saber quando reposicionar
        SpriteRenderer sr = camadas[0].GetComponent<SpriteRenderer>();
        if (sr != null)
            tamanhoSprite = sr.bounds.size.x;
    }

    void LateUpdate()
    {
        //Calcula o quanto a câmera se moveu
        float deltaX = (cam.position.x - ultimaPosCam.x) * efeitoParallax;

        //Move o fundo suavemente
        transform.position -= new Vector3(deltaX, 0, 0);

        ultimaPosCam = cam.position;

        //Verifica se a câmera passou do ponto de loop
        foreach (Transform camada in camadas)
        {
            float distCam = cam.position.x - camada.position.x;

            //Se a câmera ultrapassou a borda direita da imagem
            if (distCam > tamanhoSprite)
            {
                camada.position += new Vector3(tamanhoSprite * camadas.Length, 0, 0);
            }
            //Se voltou pra esquerda (caso o player retorne)
            else if (distCam < -tamanhoSprite)
            {
                camada.position -= new Vector3(tamanhoSprite * camadas.Length, 0, 0);
            }
        }
    }
}
