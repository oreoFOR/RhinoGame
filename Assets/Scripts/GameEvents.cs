using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents instance;
    private void Awake()
    {
        if(instance== null)
        {
            instance = this;
        }
    }
    public event Action onAnimalDie;
    public void AnimalDie()
    {
        onAnimalDie();
    }
    public event Action<Transform> onRemoveAnimal;
    public void RemoveAnimal(Transform transform)//remove from baby list so doesnt run away
    {
        onRemoveAnimal(transform);
    }
    public event Action<float> onBabyHurt;
    public void TakeDamage(float health)
    {
        onBabyHurt(health);
    }
    public event Action onGameOver;
    public void GameOver()
    {
        onGameOver();
    }
}
