using UnityEngine;

public class PlayerPotion : MonoBehaviour
{
    public float speed = 8f;
    public float lifeTime = 3f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
}
