using UnityEngine;
using TMPro;

public class ResgateBussola : MonoBehaviour
{
    [Header("Painel de Vitória")]
    public GameObject panelVictory;

    [Header("UI durante o jogo")]
    public GameObject barraVidaUI;
    public GameObject rosasUI;
    public TMP_Text textoRosas;

    void Start()
    {
        panelVictory.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            panelVictory.SetActive(true);

            barraVidaUI.SetActive(false);
            rosasUI.SetActive(false);
            textoRosas.gameObject.SetActive(false);

            Debug.Log("Vitória!");
        }
    }
}
