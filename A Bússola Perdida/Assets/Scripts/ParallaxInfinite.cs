     using UnityEngine;

     public class ParallaxInfinite : MonoBehaviour
     {
         public float parallaxEffect; // 0 a 1
         private Transform cam;
         private Vector3 previousCamPos;
         private float spriteWidth;
         private float startPosX;

         void Start()
         {
             cam = Camera.main.transform;
             previousCamPos = cam.position;
             spriteWidth = GetComponent<SpriteRenderer>().bounds.size.x;
             startPosX = transform.position.x;
             Debug.Log("Start: " + gameObject.name + " | ParallaxEffect: " + parallaxEffect);
         }

         void Update()
         {
             // Calcula movimento da câmera
             float deltaX = cam.position.x - previousCamPos.x;
             
             // Aplica parallax
             float moveAmount = deltaX * parallaxEffect;
             transform.position += new Vector3(moveAmount, 0, 0);
             
             // Debug: mostra o movimento
             if (deltaX != 0) Debug.Log(gameObject.name + " | DeltaX: " + deltaX + " | MoveAmount: " + moveAmount);
             
             // Reposicionamento (opcional, para loop)
             if (transform.position.x < cam.position.x - spriteWidth)
             {
                 transform.position += new Vector3(spriteWidth * 2, 0, 0);
             }
             else if (transform.position.x > cam.position.x + spriteWidth)
             {
                 transform.position -= new Vector3(spriteWidth * 2, 0, 0);
             }
             
             previousCamPos = cam.position;
         }
     }
     