using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length, startPos;
    private Transform cam;
    public float parallaxEffect;

    void Start()
    {
        cam = Camera.main.transform;
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        // Calcula a diferença de posição da câmera desde o início
        float temp = cam.position.x * (1 - parallaxEffect);
        float distance = cam.position.x * parallaxEffect;

        // Move o background
        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);

        // Reposiciona para criar efeito contínuo
        if (temp > startPos + length) startPos += length;
        else if (temp < startPos - length) startPos -= length;
    }
}
