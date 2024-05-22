using UnityEngine;

public class PlatformsPolyDirectinal : MonoBehaviour
{
    public Rigidbody2D mobRigidbody;

    public BoxCollider2D boxCollider2D;

    public Transform[] waypoints;

    public float speed;


    private Transform target;

    private int desPoint;

    void Start()
    {
        target = waypoints[0];
    }


    void Update()
    {
        Vector3 direction = target.position - transform.position;
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);


        // Si l'ennemie est quasiment arrivé a sa destination
        if (Vector3.Distance(transform.position, target.position) < 0.3f)
        {
            desPoint = (desPoint + 1) % waypoints.Length; // % = reste division
            target = waypoints[desPoint];
        }
    }
}