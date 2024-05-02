using Rewired;
using UnityEngine;

public class CrossHairMovement : MonoBehaviour
{
    ////////// * Variables publiques * \\\\\\\\\\

    public GameObject crossHair;

    public PlayerMovement playerMovement;

    public int playerId = 0;

    ////////// * Variables privées * \\\\\\\\\\

    private Vector3 controller_AttackDirection, aim;
    
    private Player player;

    ////////// * Méthode MoveCrossHair() * \\\\\\\\\\
    public void MoveCrossHair()
    {
        ////////// * Contrôle du crosshair à la manette * \\\\\\\\\\
        if (playerMovement.useController)
        {
            controller_AttackDirection = new Vector3(player.GetAxis("Controller_AimHorizontal"), player.GetAxis("Controller_AimVertical"), 0.0f);

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
        if (!playerMovement.useController)
        {
            // mouse_AttackDirection = new Vector3(player.GetAxis("Mouse_AimHorizontal"), player.GetAxis("Mouse_AimVertical"), 0.0f);
            Vector3 mouseMovement = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0.0f);
            aim += mouseMovement * 2;

            if (player.GetButton("Mouse_IsAiming"))
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
