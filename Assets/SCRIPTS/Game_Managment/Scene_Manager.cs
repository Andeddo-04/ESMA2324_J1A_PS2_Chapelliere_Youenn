using UnityEngine;
using System.Collections.Generic;

public class Scene_Manager : MonoBehaviour
{
    [SerializeField] private GameObject[] objectsToPersist;
    [SerializeField] private List<EnemyObject> enemiesToTrack = new List<EnemyObject>();

    private void Awake()
    {
        PersistObjects();
        RegisterObjectDestroyListeners();
    }

    private void PersistObjects()
    {
        foreach (var obj in objectsToPersist)
        {
            DontDestroyOnLoad(obj);
        }
    }

    private void RegisterObjectDestroyListeners()
    {
        foreach (var enemy in enemiesToTrack)
        {
            if (enemy.gameObject != null)
            {
                var enemyHealth = enemy.gameObject.GetComponentInChildren<BaseEnemy>();

                if (enemyHealth != null)
                {
                    enemyHealth.OnDeath += () =>
                    {
                        Debug.Log($"{enemy.gameObject.name} has died.");
                        enemy.isDead = true;
                    };
                }
                else
                {
                    Debug.LogError("Object doesn't have a script implementing IEnemyHealth interface.");
                }
            }
        }
    }

    public List<EnemyObject> GetEnemies()
    {
        return enemiesToTrack;
    }
}

[System.Serializable]
public class EnemyObject
{
    public GameObject gameObject;
    public bool isDead;
}
