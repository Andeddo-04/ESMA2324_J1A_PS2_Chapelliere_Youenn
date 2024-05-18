using UnityEngine;

public class XAxisTracker : MonoBehaviour
{
    public GameObject waypoint, targetToTrack;

    // Update is called once per frame
    void Update()
    {
        // Obtenir la position actuelle de "waypoint"
        Vector2 newPosition = waypoint.transform.position;

        // Modifier la valeur Y de la position pour qu'elle corresponde à "targetToTrack"
        newPosition.y = targetToTrack.transform.position.y;

        // Définir la nouvelle position de "waypoint"
        waypoint.transform.position = newPosition;
    }
}
