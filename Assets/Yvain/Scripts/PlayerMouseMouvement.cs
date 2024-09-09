using UnityEngine;

public class PlayerMouseMouvement : MonoBehaviour
{
    void Update()
    {
        // Récupérer la position de la souris dans le monde
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Calculer la direction de la souris à partir de la position du GameObject
        Vector3 direction = mousePosition - transform.position;
        direction.z = 0; // S'assurer qu'il reste en 2D

        // Calculer l'angle en radians entre le GameObject et la position de la souris
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Appliquer la rotation du GameObject
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
    }
}
