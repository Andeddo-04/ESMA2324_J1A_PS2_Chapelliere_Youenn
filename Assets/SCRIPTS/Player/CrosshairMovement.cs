using Rewired;
using UnityEngine;

public class CrosshairMovement : MonoBehaviour
{
    ////////// * Variables publiques * \\\\\\\\\\

    public GameObject crossHair;

    public static CrosshairMovement instance;

    public bool useController = false;

    ////////// * Variables privées * \\\\\\\\\\

    private bool isAiming = false, endOfAiming;

    private Vector3 velocity, controller_AttackDirection, aim;


    ////////// * Méthode Awake() * \\\\\\\\\\
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il n'a plus d'instance de CrosshairMovement dans la scène");
            return;
        }

        instance = this;
    }

    ////////// * Méthode MoveCrossHair() * \\\\\\\\\\
    void MoveCrossHair()
    {
        ////////// * Contrôle du crosshair à la manette * \\\\\\\\\\
        if (useController)
        {
            controller_AttackDirection = new Vector3(player.GetAxis("Controler_AimHorizontal"), player.GetAxis("Controler_AimVertical"), 0.0f);

            if (controller_AttackDirection.magnitude > 0.0f)
            {
                controller_AttackDirection.Normalize();
                controller_AttackDirection *= 2.0f;
                crossHair.transform.localPosition = controller_AttackDirection;
                crossHair.SetActive(true);
            }

            else
            {
                crossHair.SetActive(false);
            }
        }

        ////////// * Contrôle du crosshair à la sourie * \\\\\\\\\\
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
            }

            else
            {
                crossHair.SetActive(false);
            }
        }
    }
}