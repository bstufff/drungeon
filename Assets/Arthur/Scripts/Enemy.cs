using UnityEngine;

public class Enemy : MonoBehaviour, IPrototype<Enemy>
{
    public float speed = 10; // Vitesse actuelle
    public float baseSpeed;  // Vitesse de base

    public Path path;        // R�f�rence au chemin suivi

    // M�thode d'initialisation pour remplacer le constructeur
    public void Initialize(float speed, Path path)
    {
        this.speed = speed;
        this.path = path;
        this.baseSpeed = speed;
    }

    // Impl�mentation du clonage
    public Enemy Clone()
    {
        // Cr�er une nouvelle instance d'Enemy en clonant l'objet
        Enemy clonedEnemy = Instantiate(this);
        clonedEnemy.Initialize(this.speed, this.path);
        return clonedEnemy;
    }

    private void Start()
    {
        // Assigner la vitesse de base si elle n'est pas d�j� d�finie
        if (baseSpeed == 0)
            baseSpeed = speed;
    }
}
public interface IPrototype<T>
{
    T Clone();
}