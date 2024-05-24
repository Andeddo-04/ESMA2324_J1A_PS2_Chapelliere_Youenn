using Rewired;
using UnityEngine;
using System.Collections;

public class CrosshairMovement : MonoBehaviour
{
    ////////// * Variables publiques * \\\\\\\\\\

    public Transform firePoint; // Point de d�part de la fl�che (par exemple, la main de l'archer)

    public GameObject crossHair, arrowPrefab, myPlayer;

    public static CrosshairMovement instance;

    public bool useController = false;

    public float arrowSpeed;

    ////////// * Variables priv�es * \\\\\\\\\\

    private bool isAiming = false, endOfAiming;

    private Vector3 controller_AttackDirection, aim;

    private int playerId = 0;

    private Player player;

    ////////// * M�thode Awake() * \\\\\\\\\\
    void Awake()
    {
        player = ReInput.players.GetPlayer(playerId);

        if (instance != null)
        {
            Debug.LogWarning("Il n'a plus d'instance de CrosshairMovement dans la sc�ne");
            return;
        }

        instance = this;
    }

    ////////// * M�thode MoveCrossHair() * \\\\\\\\\\
    public void MoveCrossHair()
    {
        ////////// * Contr�le du crosshair � la manette * \\\\\\\\\\
        if (useController)
        {
            controller_AttackDirection = new Vector3(player.GetAxis("Controller_AimHorizontal"), player.GetAxis("Controller_AimVertical"), 0.0f);

            if (controller_AttackDirection.magnitude > 0.0f)
            {
                controller_AttackDirection.Normalize();
                controller_AttackDirection *= 2.0f;
                crossHair.transform.localPosition = controller_AttackDirection;
                crossHair.SetActive(true);

                if (player.GetButtonDown("Controller_Attack"))
                {
                    StartCoroutine(LaunchArrow());
                }
            }

            else
            {
                crossHair.SetActive(false);
            }
        }

        ////////// * Contr�le du crosshair � la sourie * \\\\\\\\\\
        if (!useController)
        {
            // mouse_AttackDirection = new Vector3(player.GetAxis("Mouse_AimHorizontal"), player.GetAxis("Mouse_AimVertical"), 0.0f);
            Vector3 mouseMovement = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0.0f);
            aim += mouseMovement * 2;

            isAiming = player.GetButton("Mouse_IsAiming");
            endOfAiming = player.GetButtonUp("Mouse_IsAiming");

            if (isAiming)
            {
                crossHair.SetActive(true);

                if (aim.magnitude > 1.0f)
                {
                    aim.Normalize();
                    aim *= 2.0f;
                    crossHair.transform.localPosition = aim;
                }

                if (player.GetButtonDown("Mouse_LaunchArrow"))
                {
                    StartCoroutine(LaunchArrow());
                }
            }

            else
            {
                crossHair.SetActive(false);
            }
        }
    }

    public IEnumerator LaunchArrow()
    {
        // Cr�ez la fl�che � partir du prefab
        GameObject arrow = Instantiate(arrowPrefab, firePoint.position, Quaternion.identity);

        // Calculer la direction vers le joueur
        Vector2 direction = (myPlayer.transform.position - firePoint.position).normalized;

        // Obtenez le Rigidbody2D de la fl�che pour appliquer la force
        Rigidbody2D arrowRb = arrow.GetComponent<Rigidbody2D>();

        // Appliquer une vitesse constante � la fl�che
        arrowRb.velocity = direction * arrowSpeed;

        // Aligner la rotation de la fl�che avec sa trajectoire
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        arrow.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        yield return new WaitForSeconds(1.75f); // Attendez le cooldown
    }
}