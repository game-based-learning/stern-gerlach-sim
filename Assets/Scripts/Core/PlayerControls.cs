using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] InputControl controls;
    private InputAction createSilverAtom;
    private InputAction createMacroscopicMagnet;
    [SerializeField] MagnetFactory factory;
    [SerializeField] MagnetFactory factory2;
    // Start is called before the first frame update
    void Start()
    {
        controls = new InputControl();
        controls.Enable();
        createSilverAtom = controls.Control.Silver;
        createMacroscopicMagnet = controls.Control.Macro;
    }

    // Update is called once per frame
    void Update()
    {
        if (createSilverAtom.WasPressedThisFrame())
        {
            factory.createSilverAtom();
        }
        else if (createMacroscopicMagnet.WasPerformedThisFrame()) {
            factory2.createMacroscopicMagnet();
        }
    }
}
