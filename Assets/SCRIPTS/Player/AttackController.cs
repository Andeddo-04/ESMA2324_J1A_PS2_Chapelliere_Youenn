using Rewired;
using UnityEngine.UI;
using System.Collections;
using UnityEngine;


public class AttackController : MonoBehaviour
{
    ////////// * Variables publiques * \\\\\\\\\\

    public static AttackController instance;

    public Transform firePoint; // Point de d�part de la fl�che (par exemple, la main de l'archer)

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

    ////////// * Variables priv�es * \\\\\\\\\\

    private Player player;

    private bool isShooting = false;

    private int playerId = 0;

    private float ATKCooldown = 0.5f;

    ////////// * M�thode Awake() * \\\\\\\\\\
    void Awake()
    {
        player = ReInput.players.GetPlayer(playerId);

        if (instance != null)
        {
            Debug.LogWarning("Il n'a plus d'instance de AttackController dans la sc�ne");
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
                if (useSword)
                {
                    // V�rifie si la fl�che du haut est press�e et si l'objet est au sol
                    if ((player.GetAxis("Controller_AttackDirection_Y") > 0.5) && player.GetButtonDown("Controller_Attack") && !isAttacking)
                    {
                        StartCoroutine(SwordAttackAtTop());
                    }

                    // V�rifie si la fl�che droite est press�e et si l'objet est au sol
                    else if ((player.GetAxis("Controller_AttackDirection_X") > 0.5) && player.GetButtonDown("Controller_Attack") && !isAttacking)
                    {
                        StartCoroutine(SwordAttackAtRight());
                    }

                    // V�rifie si la fl�che gauche est press�e et si l'objet est au sol
                    else if ((player.GetAxis("Controller_AttackDirection_X") < -0.5) && player.GetButtonDown("Controller_Attack") && !isAttacking)
                    {
                        StartCoroutine(SwordAttackAtLeft());
                    }
                }

                else if (useHalberd)
                {
                    // V�rifie si la fl�che du haut est press�e et si l'objet est au sol
                    if ((player.GetAxis("Controller_AttackDirection_Y") > 0.5) && player.GetButtonDown("Controller_Attack") && !isAttacking)
                    {
                        StartCoroutine(HalberdAttackAtTop());
                    }

                    // V�rifie si la fl�che droite est press�e et si l'objet est au sol
                    else if ((player.GetAxis("Controller_AttackDirection_X") > 0.5) && player.GetButtonDown("Controller_Attack") && !isAttacking)
                    {
                        StartCoroutine(HalberdAttackAtRight());
                    }

                    // V�rifie si la fl�che gauche est press�e et si l'objet est au sol
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
                }
            }

            if (!PlayerMovement.instance.useController)
            {
                if (useSword)
                {
                    // V�rifie si la fl�che du haut est press�e et si l'objet est au sol
                    if (player.GetButtonDown("KeyBoard_AttackAtTop") && !isAttacking)
                    {
                        StartCoroutine(SwordAttackAtTop());
                    }

                    // V�rifie si la fl�che droite est press�e et si l'objet est au sol
                    else if (player.GetButtonDown("KeyBoard_AttackAtRight") && !isAttacking)
                    {
                        StartCoroutine(SwordAttackAtRight());
                    }

                    // V�rifie si la fl�che gauche est press�e et si l'objet est au sol
                    else if (player.GetButtonDown("KeyBoard_AttackAtLeft") && !isAttacking)
                    {
                        StartCoroutine(SwordAttackAtLeft());
                    }
                }

                else if (useHalberd)
                {
                    // V�rifie si la fl�che du haut est press�e et si l'objet est au sol
                    if (player.GetButtonDown("KeyBoard_AttackAtTop") && !isAttacking)
                    {
                        StartCoroutine(HalberdAttackAtTop());
                    }

                    // V�rifie si la fl�che droite est press�e et si l'objet est au sol
                    else if (player.GetButtonDown("KeyBoard_AttackAtRight") && !isAttacking)
                    {
                        StartCoroutine(HalberdAttackAtRight());
                    }

                    // V�rifie si la fl�che gauche est press�e et si l'objet est au sol
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
                }
            }
        }
    }



    public void StartAttack()
    {
        isShooting = true;

        if (PlayerMovement.instance.useController && player.GetButtonDown("Controller_Attack"))
        {
            if (Inventory.instance.HasItem(ArrowItem) && isShooting) // V�rifie si le joueur a des fl�ches dans son inventaire
            {
                isShooting = false;
                StartCoroutine(LaunchArrow());

            }
        }

        if (!PlayerMovement.instance.useController && player.GetButtonDown("Mouse_LaunchArrow"))
        {
            if (Inventory.instance.HasItem(ArrowItem) && isShooting) // V�rifie si le joueur a des fl�ches dans son inventaire
            {
                isShooting = false;
                StartCoroutine(LaunchArrow());

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
    public IEnumerator SwordAttackAtTop()
    {
        isAttacking = true; // D�finit l'attaque comme en cours
        swordTopHitboxAttack.SetActive(true);
        yield return new WaitForSeconds(0.33f);
        swordTopHitboxAttack.SetActive(false);
        yield return new WaitForSeconds(ATKCooldown);
        isAttacking = false; // R�initialise l'attaque comme termin�e
    }

    public IEnumerator SwordAttackAtRight()
    {
        isAttacking = true; // D�finit l'attaque comme en cours
        swordRightHitboxAttack.SetActive(true);
        yield return new WaitForSeconds(0.33f);
        swordRightHitboxAttack.SetActive(false);
        yield return new WaitForSeconds(ATKCooldown);
        isAttacking = false; // R�initialise l'attaque comme termin�e
    }

    public IEnumerator SwordAttackAtLeft()
    {
        isAttacking = true; // D�finit l'attaque comme en cours
        swordLeftHitboxAttack.SetActive(true);
        yield return new WaitForSeconds(0.33f);
        swordLeftHitboxAttack.SetActive(false);
        yield return new WaitForSeconds(ATKCooldown);
        isAttacking = false; // R�initialise l'attaque comme termin�e
    }

    ////////// * Coroutine Attaque a la halbarde * \\\\\\\\\\
    public IEnumerator HalberdAttackAtTop()
    {
        isAttacking = true; // D�finit l'attaque comme en cours
        halberdTopHitboxAttack.SetActive(true);
        yield return new WaitForSeconds(0.75f);
        halberdTopHitboxAttack.SetActive(false);
        yield return new WaitForSeconds(ATKCooldown);
        isAttacking = false; // R�initialise l'attaque comme termin�e
    }

    public IEnumerator HalberdAttackAtRight()
    {
        isAttacking = true; // D�finit l'attaque comme en cours
        halberdRightHitboxAttack.SetActive(true);
        yield return new WaitForSeconds(0.75f);
        halberdRightHitboxAttack.SetActive(false);
        yield return new WaitForSeconds(ATKCooldown);
        isAttacking = false; // R�initialise l'attaque comme termin�e
    }

    public IEnumerator HalberdAttackAtLeft()
    {
        isAttacking = true; // D�finit l'attaque comme en cours
        halberdLeftHitboxAttack.SetActive(true);
        yield return new WaitForSeconds(0.75f);
        halberdLeftHitboxAttack.SetActive(false);
        yield return new WaitForSeconds(ATKCooldown);
        isAttacking = false; // R�initialise l'attaque comme termin�e
    }

    public IEnumerator LaunchArrow()
    {
        if (Inventory.instance.HasItem(ArrowItem)) // V�rifie si le joueur a des fl�ches dans son inventaire
        {
            // Cr�ez la fl�che � partir du prefab
            GameObject arrow = Instantiate(arrowPrefab, firePoint.position, Quaternion.identity);

            // Calculer la direction vers le joueur
            Vector2 direction = (firePoint.position - myPlayer.transform.position).normalized;

            // Obtenez le Rigidbody2D de la fl�che pour appliquer la force
            Rigidbody2D arrowRb = arrow.GetComponent<Rigidbody2D>();

            // Appliquer une vitesse constante � la fl�che
            arrowRb.velocity = direction * arrowSpeed;

            // Aligner la rotation de la fl�che avec sa trajectoire
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            arrow.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            // D�duire une fl�che de l'inventaire
            Inventory.instance.RemoveItem(ArrowItem);

            yield return new WaitForSeconds(1.75f); // Attendez le cooldown
        }
    }
}