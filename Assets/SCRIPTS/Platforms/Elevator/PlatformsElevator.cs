using System.Collections;
using UnityEngine;

public class PlatformsElevator : MonoBehaviour
{
    public BoxCollider2D boxCollider2D;
    
    public Transform[] waypoints;
    
    public float speed;
    
    public bool canMove = false, goUp = true, goDown = false;
    
    public static PlatformsElevator instance;


    private Transform target;
    
    private int desPoint;
    
    public bool isPlayerInElevator = false;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il n'a plus d'instance de PlatformsElevator dans la scène");
            return;
        }

        instance = this;
    }

    void Start()
    {
        target = waypoints[0];
    }

    void Update()
    {
        if (canMove && isPlayerInElevator)
        {
            MoveElevator();
        }
    }

    void MoveElevator()
    {
        Vector3 direction = target.position - transform.position;
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

        // Si la plateforme est quasiment arrivée à sa destination
        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            canMove = false;
            ToggleDirection();
        }
    }

    void ToggleDirection()
    {
        if (target == waypoints[0])
        {
            target = waypoints[1];
        }
        else
        {
            target = waypoints[0];
        }
    }
}