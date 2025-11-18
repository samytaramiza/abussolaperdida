using UnityEngine;

public class PlayerArrow : MonoBehaviour
{
    public float speed = 12f;
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
