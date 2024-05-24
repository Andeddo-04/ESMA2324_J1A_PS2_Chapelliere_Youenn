using Rewired;
using System.Collections;
using UnityEngine;


public class AttackController : MonoBehaviour
{
    ////////// * Variables publiques * \\\\\\\\\\

    public static AttackController instance;

    public GameObject handTopHitboxAttack, handRightHitboxAttack, handLeftHitboxAttack;

    public GameObject swordTopHitboxAttack, swordRightHitboxAttack, swordLeftHitboxAttack;
    
    public GameObject halberdTopHitboxAttack, halberdRightHitboxAttack, halberdLeftHitboxAttack;
    
    public bool isAttacking, dontUseWeapon = true, useSword = false, useHalberd = false, useBow = false;

    ////////// * Variables priv�es * \\\\\\\\\\

    private Player player;

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
        ChoseYourWeapon();

        if (PlayerMovement.instance.useController)
        {
            if (dontUseWeapon)
            {
                // V�rifie si la fl�che du haut est press�e et si l'objet est au sol
                if ((player.GetAxis("Controller_AttackDirection_Y") > 0.5) && player.GetButtonDown("Controller_Attack") && !isAttacking)
                {
                    StartCoroutine(DontUseWeaponAttackAtTop());
                }

                // V�rifie si la fl�che droite est press�e et si l'objet est au sol
                else if ((player.GetAxis("Controller_AttackDirection_X") > 0.5) && player.GetButtonDown("Controller_Attack") && !isAttacking)
                {
                    StartCoroutine(DontUseWeaponAttackAtRight());
                }

                // V�rifie si la fl�che gauche est press�e et si l'objet est au sol
                else if ((player.GetAxis("Controller_AttackDirection_X") < -0.5) && player.GetButtonDown("Controller_Attack") && !isAttacking)
                {
                    StartCoroutine(DontUseWeaponAttackAtLeft());
                }
            }

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

            if (useHalberd)
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

            if (useBow)
            {
                CrosshairMovement.instance.MoveCrossHair();
            }
        }

        if (!PlayerMovement.instance.useController)
        {
            if (dontUseWeapon)
            {
                // V�rifie si la fl�che du haut est press�e et si l'objet est au sol
                if (player.GetButtonDown("KeyBoard_AttackAtTop") && !isAttacking)
                {
                    StartCoroutine(DontUseWeaponAttackAtTop());
                }

                // V�rifie si la fl�che droite est press�e et si l'objet est au sol
                else if (player.GetButtonDown("KeyBoard_AttackAtRight") && !isAttacking)
                {
                    StartCoroutine(DontUseWeaponAttackAtRight());
                }

                // V�rifie si la fl�che gauche est press�e et si l'objet est au sol
                else if (player.GetButtonDown("KeyBoard_AttackAtLeft") && !isAttacking)
                {
                    StartCoroutine(DontUseWeaponAttackAtLeft());
                }

            }

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

            if (useHalberd)
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

            if (useBow)
            {
                CrosshairMovement.instance.MoveCrossHair();
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
        }

        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            dontUseWeapon = false;
            useSword = true;
            useHalberd = false;
            useBow = false;
        }

        else if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            dontUseWeapon = false;
            useSword = false;
            useHalberd = true;
            useBow = false;
        }

        else if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            dontUseWeapon = false;
            useSword = false;
            useHalberd = false;
            useBow = true;
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

    ////////// * Coroutine Attaque au fl�au d'arme * \\\\\\\\\\
    //public IEnumerator FlailWeaponAttackAtTop()
    //{
    //    isAttacking = true; // D�finit l'attaque comme en cours
    //    flailWeaponTopHitboxAttack.SetActive(true);
    //    yield return new WaitForSeconds(0.33f);
    //    flailWeaponTopHitboxAttack.SetActive(false);
    //    yield return new WaitForSeconds(ATKCooldown);
    //    isAttacking = false; // R�initialise l'attaque comme termin�e
    //}

    //public IEnumerator FlailWeaponAttackAtRight()
    //{
    //    isAttacking = true; // D�finit l'attaque comme en cours
    //    flailWeaponRightHitboxAttack.SetActive(true);
    //    yield return new WaitForSeconds(0.33f);
    //    flailWeaponRightHitboxAttack.SetActive(false);
    //    yield return new WaitForSeconds(ATKCooldown);
    //    isAttacking = false; // R�initialise l'attaque comme termin�e
    //}

    //public IEnumerator FlailWeaponAttackAtLeft()
    //{
    //    isAttacking = true; // D�finit l'attaque comme en cours
    //    flailWeaponLeftHitboxAttack.SetActive(true);
    //    yield return new WaitForSeconds(0.33f);
    //    flailWeaponLeftHitboxAttack.SetActive(false);
    //    yield return new WaitForSeconds(ATKCooldown);
    //    isAttacking = false; // R�initialise l'attaque comme termin�e
    //}


    

}
