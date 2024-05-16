using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitboxDetection : MonoBehaviour
{
    ////////// * Variables publiques * \\\\\\\\\\

    public PlayerMovement playerMovement;

    

    public GameObject attackHitbox;

    public LayerMask ennemiesLayer;

    ////////// * Variables privées * \\\\\\\\\\

    private BasicGuardian basicGuardian;
    private TankyGuardian tankyGuardian;
    //private NobleGuardian nobleGuardian;
    private ArcherGuardian archerGuardian;

    ////////// * Méthode Awake() * \\\\\\\\\\

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Basic_Guardian"))
        {
            basicGuardian = collision.transform.GetComponent<BasicGuardian>();
            basicGuardian.TakeDamages();
        }

        if (collision.transform.CompareTag("Tanky_Guardian"))
        {
            tankyGuardian = collision.transform.GetComponent<TankyGuardian>();
            tankyGuardian.TakeDamages();
        }

        //if (collision.transform.CompareTag("Noble_Guardian"))
        //{
        //    nobleGuardian = collision.transform.GetComponent<NobleGuardian>();
        //    nobleGuardian.TakeDamages();
        //}

        if (collision.transform.CompareTag("Archer_Guardian"))
        {
            archerGuardian = collision.transform.GetComponent<ArcherGuardian>();
            archerGuardian.TakeDamages();
        }
    }










    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Debug.Log("kjdwsvklhifwdblhiwfdb");
    //    if (collision.collider.CompareTag("Basic_Guardian"))
    //    {
    //        if (basicGuardian.haveSheild)
    //        {
    //            basicGuardian.BreakSheild();
    //        }

    //        else if (!basicGuardian.haveSheild)
    //        {
    //            basicGuardian.SelfDestroy();
    //        }
    //    }
    //}
}