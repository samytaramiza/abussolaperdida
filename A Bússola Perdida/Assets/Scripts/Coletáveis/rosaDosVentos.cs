using UnityEngine;

public class RosaDosVentos : MonoBehaviour
{
    public int valor = 1;

    private SpriteRenderer sr;
    private CircleCollider2D col;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<CircleCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            sr.enabled = false;
            col.enabled = false;

            GameController.instance.AddRosa(valor);

            AudioManager.Instance.PlayAudioCoin();

            Destroy(gameObject, 0.2f);
        }
    }
}
