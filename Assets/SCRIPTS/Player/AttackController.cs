using Rewired;
using System.Collections;
using UnityEngine;


public class AttackController : MonoBehaviour
{
    ////////// * Variables publiques * \\\\\\\\\\

    public static AttackController instance;

    public PlayerMovement playerMovement;

    public GameObject topHitboxAttack, rightHitboxAttack, leftHitboxAttack;

    public bool isAttacking;

    ////////// * Variables privées * \\\\\\\\\\

    private Player player;

    private int playerId = 0;

    ////////// * Méthode Awake() * \\\\\\\\\\
    void Awake()
    {
        player = ReInput.players.GetPlayer(playerId);

        if (instance != null)
        {
            Debug.LogWarning("Il n'a plus d'instance de AttackController dans la scène");
            return;
        }

        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMovement.useController)
        {
            
            // Vérifie si le joystick est vers le haut et que la touche d'attack est pressée et si le joueur n'attack pas
            if ( (player.GetAxis("Controller_AttackDirection_Y") > 0) && player.GetButtonDown("Controller_Attack") && !isAttacking)
            {
                StartCoroutine(AttackAtTop());
            }

            // Vérifie si le joystick est vers la droite et que la touche d'attack est pressée et si le joueur n'attack pas
            else if ( (player.GetAxis("Controller_AttackDirection_X") > 0) && player.GetButtonDown("Controller_Attack") && !isAttacking)
            {
                StartCoroutine(AttackAtRight());
            }

            // Vérifie si le joystick est vers la gauche et que la touche d'attack est pressée et si le joueur n'attack pas
            else if ( (player.GetAxis("Controller_AttackDirection_X") < 0) && player.GetButtonDown("Controller_Attack") && !isAttacking)
            {
                StartCoroutine(AttackAtLeft());
            }
        }

        if (!playerMovement.useController)
        {
            // Vérifie si la flèche du haut est pressée et si l'objet est au sol
            if (player.GetButtonDown("KeyBoard_AttackAtTop") && !isAttacking)
            {
                StartCoroutine(AttackAtTop());
            }

            // Vérifie si la flèche droite est pressée et si l'objet est au sol
            else if (player.GetButtonDown("KeyBoard_AttackAtRight") && !isAttacking)
            {
                StartCoroutine(AttackAtRight());
            }

            // Vérifie si la flèche gauche est pressée et si l'objet est au sol
            else if (player.GetButtonDown("KeyBoard_AttackAtLeft") && isAttacking)
            {
                StartCoroutine(AttackAtLeft());
            }
        }
    }

    public IEnumerator AttackAtTop()
    {
        isAttacking = true; // Définit l'attaque comme en cours
        topHitboxAttack.SetActive(true);
        yield return new WaitForSeconds(0.33f);
        topHitboxAttack.SetActive(false);
        yield return new WaitForSeconds(1f);
        isAttacking = false; // Réinitialise l'attaque comme terminée
    }

    public IEnumerator AttackAtRight()
    {
        isAttacking = true; // Définit l'attaque comme en cours
        rightHitboxAttack.SetActive(true);
        yield return new WaitForSeconds(0.33f);
        rightHitboxAttack.SetActive(false);
        yield return new WaitForSeconds(1f);
        isAttacking = false; // Réinitialise l'attaque comme terminée
    }

    public IEnumerator AttackAtLeft()
    {
        isAttacking = true; // Définit l'attaque comme en cours
        leftHitboxAttack.SetActive(true);
        yield return new WaitForSeconds(0.33f);
        leftHitboxAttack.SetActive(false);
        yield return new WaitForSeconds(1f);
        isAttacking = false; // Réinitialise l'attaque comme terminée
    }
}
