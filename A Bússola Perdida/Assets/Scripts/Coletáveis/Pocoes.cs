using UnityEngine;

public class Pocoes : MonoBehaviour
{
    private SpriteRenderer sr;
    private CircleCollider2D circle;

    public int quantidade = 1;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        circle = GetComponent<CircleCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            sr.enabled = false;
            circle.enabled = false;

            GameController.instance.AddPocao(quantidade);

            AudioManager.Instance.PlayAudioPotion();

            Destroy(gameObject, 0.3f);
        }
    }
}
