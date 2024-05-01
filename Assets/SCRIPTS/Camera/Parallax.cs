using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length, startPos; // Déclare deux variables privées pour stocker la longueur et la position de départ

	public GameObject cam; // Référence à la caméra dans la scène
	public float parallexEffect; // Facteur de parallaxe

	void Start () {
		startPos = transform.position.x; // Stocke la position initiale sur l'axe x
		length = GetComponent<SpriteRenderer>().bounds.size.x; // Calcule la longueur du sprite
	}
	
	void FixedUpdate () {
		float temp = (cam.transform.position.x * (1-parallexEffect)); // Calcule la position temporaire
		float dist = (cam.transform.position.x * parallexEffect); // Calcule la distance de déplacement

		transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z); // Déplace l'objet selon la parallaxe

		// Vérifie si l'objet est sorti du champ de vision de la caméra et ajuste la position de départ en conséquence
		if (temp > startPos + length) startPos += length;
		else if (temp < startPos - length) startPos -= length;
	}
}
