using Rewired;
using UnityEngine.UI;
using System.Collections;
using UnityEngine;


public class AttackController : MonoBehaviour
{
    ////////// * Variables publiques * \\\\\\\\\\

    public static AttackController instance;

    public Transform firePoint; // Point de départ de la flèche (par exemple, la main de l'archer)

    public GameObject arrowPrefab, myPlayer;

    public CrosshairMovement crosshairMovement;

    public HideHimSelf hideHimSelf;

    public GameObject handTopHitboxAttack, handRightHitboxAttack, handLeftHitboxAttack;

    public GameObject swordTopHitboxAttack, swordRightHitboxAttack, swordLeftHitboxAttack;
    
    public GameObject halberdTopHitboxAttack, halberdRightHitboxAttack, halberdLeftHitboxAttack;

    //public Text arrowWarningText;

    public InventoryItem ArrowItem;

    public bool isAttacking, dontUseWeapon = true, useSword = false, useHalberd = false, useBow = false;

    public float arrowSpeed;

    ////////// * Variables privées * \\\\\\\\\\

    private Player player;

    private bool isShooting = false;

    private int playerId = 0;

    private float ATKCooldown = 0.5f;

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
        if (!hideHimSelf.isPlayerHidden)
        {
            ChoseYourWeapon();

            if (PlayerMovement.instance.useController)
            {
                if (dontUseWeapon)
                {
                    // Vérifie si la flèche du haut est pressée et si l'objet est au sol
                    if ((player.GetAxis("Controller_AttackDirection_Y") > 0.5) && player.GetButtonDown("Controller_Attack") && !isAttacking)
                    {
                        StartCoroutine(DontUseWeaponAttackAtTop());
                    }

                    // Vérifie si la flèche droite est pressée et si l'objet est au sol
                    else if ((player.GetAxis("Controller_AttackDirection_X") > 0.5) && player.GetButtonDown("Controller_Attack") && !isAttacking)
                    {
                        StartCoroutine(DontUseWeaponAttackAtRight());
                    }

                    // Vérifie si la flèche gauche est pressée et si l'objet est au sol
                    else if ((player.GetAxis("Controller_AttackDirection_X") < -0.5) && player.GetButtonDown("Controller_Attack") && !isAttacking)
                    {
                        StartCoroutine(DontUseWeaponAttackAtLeft());
                    }
                }

                else if (useSword)
                {
                    // Vérifie si la flèche du haut est pressée et si l'objet est au sol
                    if ((player.GetAxis("Controller_AttackDirection_Y") > 0.5) && player.GetButtonDown("Controller_Attack") && !isAttacking)
                    {
                        StartCoroutine(SwordAttackAtTop());
                    }

                    // Vérifie si la flèche droite est pressée et si l'objet est au sol
                    else if ((player.GetAxis("Controller_AttackDirection_X") > 0.5) && player.GetButtonDown("Controller_Attack") && !isAttacking)
                    {
                        StartCoroutine(SwordAttackAtRight());
                    }

                    // Vérifie si la flèche gauche est pressée et si l'objet est au sol
                    else if ((player.GetAxis("Controller_AttackDirection_X") < -0.5) && player.GetButtonDown("Controller_Attack") && !isAttacking)
                    {
                        StartCoroutine(SwordAttackAtLeft());
                    }
                }

                else if (useHalberd)
                {
                    // Vérifie si la flèche du haut est pressée et si l'objet est au sol
                    if ((player.GetAxis("Controller_AttackDirection_Y") > 0.5) && player.GetButtonDown("Controller_Attack") && !isAttacking)
                    {
                        StartCoroutine(HalberdAttackAtTop());
                    }

                    // Vérifie si la flèche droite est pressée et si l'objet est au sol
                    else if ((player.GetAxis("Controller_AttackDirection_X") > 0.5) && player.GetButtonDown("Controller_Attack") && !isAttacking)
                    {
                        StartCoroutine(HalberdAttackAtRight());
                    }

                    // Vérifie si la flèche gauche est pressée et si l'objet est au sol
                    else if ((player.GetAxis("Controller_AttackDirection_X") < -0.5) && player.GetButtonDown("Controller_Attack") && !isAttacking)
                    {
                        StartCoroutine(HalberdAttackAtLeft());
                    }
                }

                else if (useBow)
                {
                    if (Inventory.instance.HasItem(ArrowItem) && player.GetButtonDown("Controller_Attack"))
                    {
                        StartAttack();
                    }
                    else
                    {
                        // Display arrow warning text
                        //arrowWarningText.enabled = true;
                    }
                }
                else
                {
                    // Hide arrow warning text when not using bow
                    //arrowWarningText.enabled = false;
                }
            }

            if (!PlayerMovement.instance.useController)
            {
                if (dontUseWeapon)
                {
                    // Vérifie si la flèche du haut est pressée et si l'objet est au sol
                    if (player.GetButtonDown("KeyBoard_AttackAtTop") && !isAttacking)
                    {
                        StartCoroutine(DontUseWeaponAttackAtTop());
                    }

                    // Vérifie si la flèche droite est pressée et si l'objet est au sol
                    else if (player.GetButtonDown("KeyBoard_AttackAtRight") && !isAttacking)
                    {
                        StartCoroutine(DontUseWeaponAttackAtRight());
                    }

                    // Vérifie si la flèche gauche est pressée et si l'objet est au sol
                    else if (player.GetButtonDown("KeyBoard_AttackAtLeft") && !isAttacking)
                    {
                        StartCoroutine(DontUseWeaponAttackAtLeft());
                    }

                }

                else if (useSword)
                {
                    // Vérifie si la flèche du haut est pressée et si l'objet est au sol
                    if (player.GetButtonDown("KeyBoard_AttackAtTop") && !isAttacking)
                    {
                        StartCoroutine(SwordAttackAtTop());
                    }

                    // Vérifie si la flèche droite est pressée et si l'objet est au sol
                    else if (player.GetButtonDown("KeyBoard_AttackAtRight") && !isAttacking)
                    {
                        StartCoroutine(SwordAttackAtRight());
                    }

                    // Vérifie si la flèche gauche est pressée et si l'objet est au sol
                    else if (player.GetButtonDown("KeyBoard_AttackAtLeft") && !isAttacking)
                    {
                        StartCoroutine(SwordAttackAtLeft());
                    }
                }

                else if (useHalberd)
                {
                    // Vérifie si la flèche du haut est pressée et si l'objet est au sol
                    if (player.GetButtonDown("KeyBoard_AttackAtTop") && !isAttacking)
                    {
                        StartCoroutine(HalberdAttackAtTop());
                    }

                    // Vérifie si la flèche droite est pressée et si l'objet est au sol
                    else if (player.GetButtonDown("KeyBoard_AttackAtRight") && !isAttacking)
                    {
                        StartCoroutine(HalberdAttackAtRight());
                    }

                    // Vérifie si la flèche gauche est pressée et si l'objet est au sol
                    else if (player.GetButtonDown("KeyBoard_AttackAtLeft") && !isAttacking)
                    {
                        StartCoroutine(HalberdAttackAtLeft());
                    }
                }

                else if (useBow)
                {
                    if (Inventory.instance.HasItem(ArrowItem) && player.GetButtonDown("Mouse_LaunchArrow"))
                    {
                        StartAttack();
                    }
                    else
                    {
                        // Display arrow warning text
                        //arrowWarningText.enabled = true;
                    }
                }
                else
                {
                    // Hide arrow warning text when not using bow
                    //arrowWarningText.enabled = false;
                }
            }
        }
    }

        

    public void StartAttack()
    {
        isShooting = true;

        if (PlayerMovement.instance.useController && player.GetButtonDown("Controller_Attack"))
        {
            if (Inventory.instance.HasItem(ArrowItem) && isShooting) // Vérifie si le joueur a des flèches dans son inventaire
            {
                isShooting = false;
                StartCoroutine(LaunchArrow());

            }
            else
            {
                // Afficher un message indiquant que le joueur n'a pas de flèches
                Debug.Log("You don't have any arrows!");
            }
        }

        if (!PlayerMovement.instance.useController && player.GetButtonDown("Mouse_LaunchArrow"))
        {
            if (Inventory.instance.HasItem(ArrowItem) && isShooting) // Vérifie si le joueur a des flèches dans son inventaire
            {
                isShooting = false;
                StartCoroutine(LaunchArrow());

            }
            else
            {
                // Afficher un message indiquant que le joueur n'a pas de flèches
                Debug.Log("You don't have any arrows!");

            }
        }
    }

    void ChoseYourWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            dontUseWeapon = true;
            useSword = false;
            useHalberd = false;
            useBow = false;

            crosshairMovement.IsAimingChangerAtFalse();
            crosshairMovement.crossHair.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            dontUseWeapon = false;
            useSword = true;
            useHalberd = false;
            useBow = false;

            crosshairMovement.IsAimingChangerAtFalse();
            crosshairMovement.crossHair.SetActive(false);
        }

        else if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            dontUseWeapon = false;
            useSword = false;
            useHalberd = true;
            useBow = false;

            crosshairMovement.IsAimingChangerAtFalse();
            crosshairMovement.crossHair.SetActive(false);
        }

        else if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            dontUseWeapon = false;
            useSword = false;
            useHalberd = false;
            useBow = true;

            crosshairMovement.IsAimingChangerAtTrue();
            crosshairMovement.crossHair.SetActive(true);
        }
    }

    ////////// * Coroutine Attaque a l'epee * \\\\\\\\\\
    public IEnumerator DontUseWeaponAttackAtTop()
    {
        isAttacking = true; // Définit l'attaque comme en cours
        handTopHitboxAttack.SetActive(true);
        yield return new WaitForSeconds(0.33f);
        handTopHitboxAttack.SetActive(false);
        yield return new WaitForSeconds(ATKCooldown);
        isAttacking = false; // Réinitialise l'attaque comme terminée
    }

    public IEnumerator DontUseWeaponAttackAtRight()
    {
        isAttacking = true; // Définit l'attaque comme en cours
        handRightHitboxAttack.SetActive(true);
        yield return new WaitForSeconds(0.33f);
        handRightHitboxAttack.SetActive(false);
        yield return new WaitForSeconds(ATKCooldown);
        isAttacking = false; // Réinitialise l'attaque comme terminée
    }

    public IEnumerator DontUseWeaponAttackAtLeft()
    {
        isAttacking = true; // Définit l'attaque comme en cours
        handLeftHitboxAttack.SetActive(true);
        yield return new WaitForSeconds(0.33f);
        handLeftHitboxAttack.SetActive(false);
        yield return new WaitForSeconds(ATKCooldown);
        isAttacking = false; // Réinitialise l'attaque comme terminée
    }

    ////////// * Coroutine Attaque a l'epee * \\\\\\\\\\
    public IEnumerator SwordAttackAtTop()
    {
        isAttacking = true; // Définit l'attaque comme en cours
        swordTopHitboxAttack.SetActive(true);
        yield return new WaitForSeconds(0.33f);
        swordTopHitboxAttack.SetActive(false);
        yield return new WaitForSeconds(ATKCooldown);
        isAttacking = false; // Réinitialise l'attaque comme terminée
    }

    public IEnumerator SwordAttackAtRight()
    {
        isAttacking = true; // Définit l'attaque comme en cours
        swordRightHitboxAttack.SetActive(true);
        yield return new WaitForSeconds(0.33f);
        swordRightHitboxAttack.SetActive(false);
        yield return new WaitForSeconds(ATKCooldown);
        isAttacking = false; // Réinitialise l'attaque comme terminée
    }

    public IEnumerator SwordAttackAtLeft()
    {
        isAttacking = true; // Définit l'attaque comme en cours
        swordLeftHitboxAttack.SetActive(true);
        yield return new WaitForSeconds(0.33f);
        swordLeftHitboxAttack.SetActive(false);
        yield return new WaitForSeconds(ATKCooldown);
        isAttacking = false; // Réinitialise l'attaque comme terminée
    }

    ////////// * Coroutine Attaque a la halbarde * \\\\\\\\\\
    public IEnumerator HalberdAttackAtTop()
    {
        isAttacking = true; // Définit l'attaque comme en cours
        halberdTopHitboxAttack.SetActive(true);
        yield return new WaitForSeconds(0.33f);
        halberdTopHitboxAttack.SetActive(false);
        yield return new WaitForSeconds(ATKCooldown);
        isAttacking = false; // Réinitialise l'attaque comme terminée
    }

    public IEnumerator HalberdAttackAtRight()
    {
        isAttacking = true; // Définit l'attaque comme en cours
        halberdRightHitboxAttack.SetActive(true);
        yield return new WaitForSeconds(0.33f);
        halberdRightHitboxAttack.SetActive(false);
        yield return new WaitForSeconds(ATKCooldown);
        isAttacking = false; // Réinitialise l'attaque comme terminée
    }

    public IEnumerator HalberdAttackAtLeft()
    {
        isAttacking = true; // Définit l'attaque comme en cours
        halberdLeftHitboxAttack.SetActive(true);
        yield return new WaitForSeconds(0.33f);
        halberdLeftHitboxAttack.SetActive(false);
        yield return new WaitForSeconds(ATKCooldown);
        isAttacking = false; // Réinitialise l'attaque comme terminée
    }

    public IEnumerator LaunchArrow()
    {
        if (Inventory.instance.HasItem(ArrowItem)) // Vérifie si le joueur a des flèches dans son inventaire
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

            // Déduire une flèche de l'inventaire
            Inventory.instance.RemoveItem(ArrowItem);

            yield return new WaitForSeconds(1.75f); // Attendez le cooldown
        }
        else
        {
            // Afficher un message indiquant que le joueur n'a pas de flèches
            Debug.Log("You don't have any arrows!");
        }
    }
}
