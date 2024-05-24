using Rewired;
using UnityEngine;
using System.Collections;

public class CrosshairMovement : MonoBehaviour
{
    ////////// * Variables publiques * \\\\\\\\\\

    public Transform firePoint; // Point de départ de la flèche (par exemple, la main de l'archer)

    public GameObject crossHair, arrowPrefab, myPlayer;

    public static CrosshairMovement instance;

    public PauseMenu pauseMenu;

    public bool useController = false;

    public float arrowSpeed;

    ////////// * Variables privées * \\\\\\\\\\

    public bool isAiming = false;

    private Vector3 controller_AttackDirection, aim;

    private int playerId = 0;

    private Player player;

    ////////// * Méthode Awake() * \\\\\\\\\\
    void Awake()
    {
        player = ReInput.players.GetPlayer(playerId);

        if (instance != null)
        {
            Debug.LogWarning("Il n'a plus d'instance de CrosshairMovement dans la scène");
            return;
        }

        instance = this;
    }

    ////////// * Méthode MoveCrossHair() * \\\\\\\\\\
    public void MoveCrossHair()
    {
        if (pauseMenu.gameIsPaused == false)
        {
            ////////// * Contrôle du crosshair à la manette * \\\\\\\\\\
            if (useController && AttackController.instance.useBow)
            {
                controller_AttackDirection = new Vector3(player.GetAxis("Controller_AimHorizontal"), player.GetAxis("Controller_AimVertical"), 0.0f);

                if (controller_AttackDirection.magnitude > 0.0f)
                {
                    controller_AttackDirection.Normalize();
                    controller_AttackDirection *= 2.0f;
                    crossHair.transform.localPosition = controller_AttackDirection;
                    //crossHair.SetActive(true);

                    if (player.GetButtonDown("Controller_Attack"))
                    {
                        StartAttack();
                    }
                }

                else
                {
                    crossHair.SetActive(false);
                }
            }

            ////////// * Contrôle du crosshair à la sourie * \\\\\\\\\\
            if (!useController && AttackController.instance.useBow)
            {
                // mouse_AttackDirection = new Vector3(player.GetAxis("Mouse_AimHorizontal"), player.GetAxis("Mouse_AimVertical"), 0.0f);
                Vector3 mouseMovement = new Vector3(player.GetAxis("Mouse_Aim_X"), player.GetAxis("Mouse_Aim_Y"), 0.0f);
                aim += mouseMovement;

                if (isAiming)
                {
                    //crossHair.SetActive(true);

                    if (aim.magnitude > 1.0f)
                    {
                        aim.Normalize();
                        aim *= 40.0f;
                        crossHair.transform.localPosition = aim;
                    }

                    if (player.GetButtonDown("Mouse_LaunchArrow"))
                    {
                        StartAttack();
                    }
                }

                else
                {
                    crossHair.SetActive(false);
                }
            }
        }        
    }

    public void StartAttack()
    {
        if (player.GetButtonDown("Mouse_LaunchArrow"))
        {
            StartCoroutine(LaunchArrow());
        }        
    }

    public void IsAimingChangerAtTrue()
    {
        isAiming = true;
    }

    public void IsAimingChangerAtFalse()
    {
        isAiming = false;
    }

    public IEnumerator LaunchArrow()
    {
        // Créez la flèche à partir du prefab
        GameObject arrow = Instantiate(arrowPrefab, firePoint.position, Quaternion.identity);

        // Calculer la direction vers le joueur
        Vector2 direction = (firePoint.position - myPlayer.transform.position).normalized;

        // Obtenez le Rigidbody2D de la flèche pour appliquer la force
        Rigidbody2D arrowRb = arrow.GetComponent<Rigidbody2D>();

        // Appliquer une vitesse constante à la flèche
        arrowRb.velocity = direction * arrowSpeed;

        // Aligner la rotation de la flèche avec sa trajectoire
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        arrow.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        yield return new WaitForSeconds(1.75f); // Attendez le cooldown
    }
}