using UnityEngine; //Biblioteca principal do Unity (necessária para MonoBehaviour e Application)
using UnityEngine.SceneManagement; //Necessária para carregar cenas

public class Menu : MonoBehaviour
{
    public GameObject PanelConfig;

    public GameObject imagePocao;
    public GameObject imageRosa;
    public GameObject barraDeVida;

    public void AbrirConfig() 
    {
        if (PanelConfig != null)
        {
            PanelConfig.SetActive(true); // Ativa o Canvas
            imagePocao.SetActive(false);
            imageRosa.SetActive(false);
            barraDeVida.SetActive(false);
        }
    }
    
    public void FecharConfig()
    {
        if (PanelConfig != null)
        {
            PanelConfig.SetActive(false); //Desativa o Canvas
        }
    }

    //Método para carregar uma cena específica
    //Chamado a partir de um botão no Canvas, passando o nome da cena como parâmetro
    public void LoadScene(string cena)
    {
        SceneManager.LoadScene(cena); // Carrega a cena cujo nome foi passado
    }

    // Método para sair do jogo
    // Funciona apenas no executável final (não funciona no editor do Unity)
    public void Quit()
    {
        Application.Quit(); // Fecha a aplicação
    }
}
