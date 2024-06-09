using Rewired;
using UnityEngine.UI;
using System.Collections;
using UnityEngine;


public class AttackController : MonoBehaviour
{
    public static AttackController instance;

    public Transform firePoint; // Point de d�part de la fl�che (par exemple, la main de l'archer)
    public GameObject arrowPrefab, myPlayer;
    public CrosshairMovement crosshairMovement;
    public HideHimSelf hideHimSelf;
    public GameObject handTopHitboxAttack, handRightHitboxAttack, handLeftHitboxAttack;
    public GameObject swordTopHitboxAttack, swordRightHitboxAttack, swordLeftHitboxAttack;
    public GameObject halberdTopHitboxAttack, halberdRightHitboxAttack, halberdLeftHitboxAttack;

    public InventoryItem ArrowItem;

    public InventoryItem firstEquippedWeapon = null;
    public InventoryItem secondEquippedWeapon = null;
    public InventoryItem currentEquippedWeapon = null;

    public bool isAttacking, dontUseWeapon = true, useSword = false, useHalberd = false, useBow = false;
    public float arrowSpeed;

    private Player player;
    private bool isShooting = false;
    private int playerId = 0;
    private float ATKCooldown = 0.5f;

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

    void Update()
    {
        if (!hideHimSelf.isPlayerHidden)
        {

            if (PlayerMovement.instance.useController)
            {
                HandleControllerInput();
            }
            else
            {
                HandleKeyboardInput();
            }

            // Bascule entre les armes �quip�es avec les touches d�finies
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SwitchWeapon(1);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SwitchWeapon(2);
            }
        }
    }

    private void HandleControllerInput()
    {
        if (dontUseWeapon)
        {
            HandleNoWeaponAttack("Controller_AttackDirection_Y", "Controller_AttackDirection_X", "Controller_Attack");
        }
        else if (useSword)
        {
            HandleSwordAttack("Controller_AttackDirection_Y", "Controller_AttackDirection_X", "Controller_Attack");
        }
        else if (useHalberd)
        {
            HandleHalberdAttack("Controller_AttackDirection_Y", "Controller_AttackDirection_X", "Controller_Attack");
        }
        else if (useBow && Inventory.instance.HasItem(ArrowItem))
        {
            if (player.GetButtonDown("Controller_Attack"))
            {
                StartAttack();
            }
        }
    }

    private void HandleKeyboardInput()
    {
        if (dontUseWeapon)
        {
            HandleNoWeaponAttack("KeyBoard_AttackAtTop", "KeyBoard_AttackAtRight", "KeyBoard_AttackAtLeft");
        }
        else if (useSword)
        {
            HandleSwordAttack("KeyBoard_AttackAtTop", "KeyBoard_AttackAtRight", "KeyBoard_AttackAtLeft");
        }
        else if (useHalberd)
        {
            HandleHalberdAttack("KeyBoard_AttackAtTop", "KeyBoard_AttackAtRight", "KeyBoard_AttackAtLeft");
        }
        else if (useBow && Inventory.instance.HasItem(ArrowItem))
        {
            if (player.GetButtonDown("Mouse_LaunchArrow"))
            {
                StartAttack();
            }
        }
    }

    private void HandleNoWeaponAttack(string attackUp, string attackRight, string attackLeft)
    {
        if (player.GetAxis(attackUp) > 0.5 && player.GetButtonDown(attackUp) && !isAttacking)
        {
            StartCoroutine(DontUseWeaponAttackAtTop());
        }
        else if (player.GetAxis(attackRight) > 0.5 && player.GetButtonDown(attackRight) && !isAttacking)
        {
            StartCoroutine(DontUseWeaponAttackAtRight());
        }
        else if (player.GetAxis(attackLeft) < -0.5 && player.GetButtonDown(attackLeft) && !isAttacking)
        {
            StartCoroutine(DontUseWeaponAttackAtLeft());
        }
    }

    private void HandleSwordAttack(string attackUp, string attackRight, string attackLeft)
    {
        if (player.GetAxis(attackUp) > 0.5 && player.GetButtonDown(attackUp) && !isAttacking)
        {
            StartCoroutine(SwordAttackAtTop());
        }
        else if (player.GetAxis(attackRight) > 0.5 && player.GetButtonDown(attackRight) && !isAttacking)
        {
            StartCoroutine(SwordAttackAtRight());
        }
        else if (player.GetAxis(attackLeft) < -0.5 && player.GetButtonDown(attackLeft) && !isAttacking)
        {
            StartCoroutine(SwordAttackAtLeft());
        }
    }

    private void HandleHalberdAttack(string attackUp, string attackRight, string attackLeft)
    {
        if (player.GetAxis(attackUp) > 0.5 && player.GetButtonDown(attackUp) && !isAttacking)
        {
            StartCoroutine(HalberdAttackAtTop());
        }
        else if (player.GetAxis(attackRight) > 0.5 && player.GetButtonDown(attackRight) && !isAttacking)
        {
            StartCoroutine(HalberdAttackAtRight());
        }
        else if (player.GetAxis(attackLeft) < -0.5 && player.GetButtonDown(attackLeft) && !isAttacking)
        {
            StartCoroutine(HalberdAttackAtLeft());
        }
    }

    public void StartAttack()
    {
        isShooting = true;

        if (PlayerMovement.instance.useController && player.GetButtonDown("Controller_Attack"))
        {
            if (Inventory.instance.HasItem(ArrowItem) && isShooting)
            {
                isShooting = false;
                StartCoroutine(LaunchArrow());
            }
        }

        if (!PlayerMovement.instance.useController && player.GetButtonDown("Mouse_LaunchArrow"))
        {
            if (Inventory.instance.HasItem(ArrowItem) && isShooting)
            {
                isShooting = false;
                StartCoroutine(LaunchArrow());
            }
        }
    }

    private void EquipWeapon(InventoryItem weaponItem)
    {
        UnequipCurrentWeapon();

        if (weaponItem.itemName == "Sword")
        {
            useSword = true;
            dontUseWeapon = false;
        }
        else if (weaponItem.itemName == "Halberd")
        {
            useHalberd = true;
            dontUseWeapon = false;
            useBow = false;
            useSword = false;
        }
        else if (weaponItem.itemName == "Bow")
        {
            useBow = true;
            dontUseWeapon = false;
        }

        dontUseWeapon = false;
    }


    private void SwitchWeapon(int slot)
    {
        if (slot == 1 && InventoryUI.instance.firstEquippedWeapon != null)
        {
            currentEquippedWeapon = InventoryUI.instance.firstEquippedWeapon;
            EquipWeapon(currentEquippedWeapon);
        }
        else if (slot == 2 && InventoryUI.instance.secondEquippedWeapon != null)
        {
            currentEquippedWeapon = InventoryUI.instance.secondEquippedWeapon;
            EquipWeapon(currentEquippedWeapon);
        }
    }

    private void UnequipCurrentWeapon()
    {
        if (currentEquippedWeapon != null)
        {
            if (currentEquippedWeapon.itemName == "Sword")
            {
                useSword = false;
            }
            else if (currentEquippedWeapon.itemName == "Halberd")
            {
                useHalberd = false;
            }
            else if (currentEquippedWeapon.itemName == "Bow")
            {
                useBow = false;
            }

            dontUseWeapon = true;
        }
    }





    ////////// * Coroutine Attaque a l'epee * \\\\\\\\\\
    public IEnumerator DontUseWeaponAttackAtTop()
    {
        isAttacking = true; // D�finit l'attaque comme en cours
        handTopHitboxAttack.SetActive(true);
        yield return new WaitForSeconds(0.33f);
        handTopHitboxAttack.SetActive(false);
        yield return new WaitForSeconds(ATKCooldown);
        isAttacking = false; // R�initialise l'attaque comme termin�e
    }

    public IEnumerator DontUseWeaponAttackAtRight()
    {
        isAttacking = true; // D�finit l'attaque comme en cours
        handRightHitboxAttack.SetActive(true);
        yield return new WaitForSeconds(0.33f);
        handRightHitboxAttack.SetActive(false);
        yield return new WaitForSeconds(ATKCooldown);
        isAttacking = false; // R�initialise l'attaque comme termin�e
    }

    public IEnumerator DontUseWeaponAttackAtLeft()
    {
        isAttacking = true; // D�finit l'attaque comme en cours
        handLeftHitboxAttack.SetActive(true);
        yield return new WaitForSeconds(0.33f);
        handLeftHitboxAttack.SetActive(false);
        yield return new WaitForSeconds(ATKCooldown);
        isAttacking = false; // R�initialise l'attaque comme termin�e
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
        yield return new WaitForSeconds(0.33f);
        halberdTopHitboxAttack.SetActive(false);
        yield return new WaitForSeconds(ATKCooldown);
        isAttacking = false; // R�initialise l'attaque comme termin�e
    }

    public IEnumerator HalberdAttackAtRight()
    {
        isAttacking = true; // D�finit l'attaque comme en cours
        halberdRightHitboxAttack.SetActive(true);
        yield return new WaitForSeconds(0.33f);
        halberdRightHitboxAttack.SetActive(false);
        yield return new WaitForSeconds(ATKCooldown);
        isAttacking = false; // R�initialise l'attaque comme termin�e
    }

    public IEnumerator HalberdAttackAtLeft()
    {
        isAttacking = true; // D�finit l'attaque comme en cours
        halberdLeftHitboxAttack.SetActive(true);
        yield return new WaitForSeconds(0.33f);
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
