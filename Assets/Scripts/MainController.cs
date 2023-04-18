using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MainController : MonoBehaviour
{
    [SerializeField] private int maxHP = 50;
    public UnityEvent onDestroyPlayer;
    public static event Action<int> onPlayerDamage;
    public static event Action onPlayerDeath;

    public void RemoveHealth(int damage)
    {
        maxHP = Math.Clamp(maxHP - damage, 0, 50);
        onPlayerDamage?.Invoke(damage);

        if(maxHP <= 0)
        {
            onPlayerDeath?.Invoke();
        }
    }
    private void OnDisable()
    {
        onDestroyPlayer.Invoke();
    }
}
