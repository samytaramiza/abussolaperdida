using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public float speed = 6f;
    public float lifeTime = 3f;

    void OnEnable()
    {
        CancelInvoke();
        Invoke(nameof(Disable), lifeTime);
    }

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void Disable()
    {
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        gameObject.SetActive(false);
    }
}
