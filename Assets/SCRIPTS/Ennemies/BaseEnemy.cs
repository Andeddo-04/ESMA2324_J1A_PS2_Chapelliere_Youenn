using System;
using UnityEngine;
public class BaseEnemy : MonoBehaviour
{
    public event Action OnDeath;

    public void TriggerOnDeath()
    {
        OnDeath?.Invoke();
    }
}
