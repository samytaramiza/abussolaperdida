using UnityEngine;

public class Enemy : MonoBehaviour
{
    public void Morrer()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            // Player pisou na cabeça
            col.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 8f, ForceMode2D.Impulse);
            Morrer();
        }
    }
}
