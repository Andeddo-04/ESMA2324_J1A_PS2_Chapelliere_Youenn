using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SwitchControls : MonoBehaviour
{
    public Toggle controlesClavier, controlesManette;

    public PlayerMovement playerMovement;

    private void Awake()
    {
        controlesClavier = GameObject.FindGameObjectWithTag("KeyBoardType").GetComponent<Toggle>();
        controlesManette = GameObject.FindGameObjectWithTag("ControllerType").GetComponent<Toggle>();
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    // Start is called before the first frame update
    void Start()
    {
        controlesClavier.isOn = true;
        controlesManette.isOn = false; 
    }

    public void SwitchIntoController()
    {
        controlesClavier.isOn = !controlesManette.isOn;

        playerMovement.SetControllerUsage(true);
    }

    public void SwitchIntoKeyBoard()
    {
        controlesManette.isOn = !controlesClavier.isOn;

        playerMovement.SetControllerUsage(false);
    }
}
