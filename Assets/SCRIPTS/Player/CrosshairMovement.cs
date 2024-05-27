using Rewired;
using UnityEngine;
using System.Collections;

public class CrosshairMovement : MonoBehaviour
{
    ////////// * Variables publiques * \\\\\\\\\\

    public GameObject crossHair;

    public AttackController attackController;

    public static CrosshairMovement instance;

    public PauseMenu pauseMenu;

    ////////// * Variables privées * \\\\\\\\\\

    public bool isAiming = false;

    private Vector2 controller_AttackDirection, aim, mouseMovement;

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
        if (pauseMenu.gameIsPaused == false && AttackController.instance.useBow)
        {
            ////////// * Contrôle du crosshair à la manette * \\\\\\\\\\
            if (PlayerMovement.instance.useController)
            {
                controller_AttackDirection = new Vector2(player.GetAxis("Controller_AttackDirection_X"), player.GetAxis("Controller_AttackDirection_Y"));

                if (controller_AttackDirection.magnitude > 0.0f)
                {
                    controller_AttackDirection.Normalize();
                    controller_AttackDirection *= 40.0f;
                    crossHair.transform.localPosition = controller_AttackDirection;
                }
            }

            ////////// * Contrôle du crosshair à la sourie * \\\\\\\\\\
            if (!PlayerMovement.instance.useController)
            {
                // mouse_AttackDirection = new Vector3(player.GetAxis("Mouse_AimHorizontal"), player.GetAxis("Mouse_AimVertical"), 0.0f);
                mouseMovement = new Vector2(player.GetAxis("Mouse_Aim_X"), player.GetAxis("Mouse_Aim_Y"));
                aim += mouseMovement / 1.5f;

                if (isAiming && aim.magnitude > 1.0f)
                {
                    aim.Normalize();
                    aim *= 40.0f;
                    crossHair.transform.localPosition = aim;
                }
            }
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

    

}