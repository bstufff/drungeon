using UnityEngine;

public class Enemy : MonoBehaviour, IPrototype<Enemy>
{
    public float speed = 10; // Vitesse actuelle
    public float baseSpeed;  // Vitesse de base

    public Path path;        // Référence au chemin suivi

    // Méthode d'initialisation pour remplacer le constructeur
    public void Initialize(float speed, Path path)
    {
        this.speed = speed;
        this.path = path;
        this.baseSpeed = speed;
    }

    // Implémentation du clonage
    public Enemy Clone()
    {
        // Créer une nouvelle instance d'Enemy en clonant l'objet
        Enemy clonedEnemy = Instantiate(this);
        clonedEnemy.Initialize(this.speed, this.path);
        return clonedEnemy;
    }

    private void Start()
    {
        // Assigner la vitesse de base si elle n'est pas déjà définie
        if (baseSpeed == 0)
            baseSpeed = speed;
    }
}
public interface IPrototype<T>
{
    T Clone();
}