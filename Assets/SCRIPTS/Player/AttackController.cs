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

    ////////// * Variables privées * \\\\\\\\\\

    private Player player;

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

            if (useSword)
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

            if (useHalberd)
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

            if (useBow)
            {
                CrosshairMovement.instance.MoveCrossHair();
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

            if (useSword)
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

            if (useHalberd)
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

    ////////// * Coroutine Attaque au fléau d'arme * \\\\\\\\\\
    //public IEnumerator FlailWeaponAttackAtTop()
    //{
    //    isAttacking = true; // Définit l'attaque comme en cours
    //    flailWeaponTopHitboxAttack.SetActive(true);
    //    yield return new WaitForSeconds(0.33f);
    //    flailWeaponTopHitboxAttack.SetActive(false);
    //    yield return new WaitForSeconds(ATKCooldown);
    //    isAttacking = false; // Réinitialise l'attaque comme terminée
    //}

    //public IEnumerator FlailWeaponAttackAtRight()
    //{
    //    isAttacking = true; // Définit l'attaque comme en cours
    //    flailWeaponRightHitboxAttack.SetActive(true);
    //    yield return new WaitForSeconds(0.33f);
    //    flailWeaponRightHitboxAttack.SetActive(false);
    //    yield return new WaitForSeconds(ATKCooldown);
    //    isAttacking = false; // Réinitialise l'attaque comme terminée
    //}

    //public IEnumerator FlailWeaponAttackAtLeft()
    //{
    //    isAttacking = true; // Définit l'attaque comme en cours
    //    flailWeaponLeftHitboxAttack.SetActive(true);
    //    yield return new WaitForSeconds(0.33f);
    //    flailWeaponLeftHitboxAttack.SetActive(false);
    //    yield return new WaitForSeconds(ATKCooldown);
    //    isAttacking = false; // Réinitialise l'attaque comme terminée
    //}


    

}
