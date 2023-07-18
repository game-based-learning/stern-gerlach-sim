using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using SternGerlach;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] InputControl controls;
    private InputAction mouseClick;
    private InputAction placeImagePlate, placeSGMagnet;
    private InputAction createSilverAtom, createMacroscopicMagnet;
    private InputAction rotateLeft, rotateRight;
    [SerializeField] GameObjectFactory factory;
    [SerializeField] GameObjectFactory factory2;
    [SerializeField] NodeBuilder builder;
    [SerializeField] Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        controls = new InputControl();
        controls.Enable();
        createSilverAtom = controls.Control.Silver;
        createMacroscopicMagnet = controls.Control.Macro;
        placeSGMagnet = controls.Control.PlaceSGMagnet;
        placeImagePlate = controls.Control.PlaceImagePlate;
        mouseClick = controls.Control.MouseLeftClick;
        rotateLeft = controls.Control.RotateLeft;
        rotateRight = controls.Control.RotateRight;
    }

    // Update is called once per frame
    void Update()
    {
        if (createSilverAtom.WasPressedThisFrame())
        {
            factory.CreateSilverAtom();
        }
        if (createMacroscopicMagnet.WasPerformedThisFrame())
        {
            factory2.CreateMacroscopicMagnet();
        }
        if (placeSGMagnet.WasPressedThisFrame()) {
            builder.PlaceSGMagnet();
        }
        if (placeImagePlate.WasPressedThisFrame()) {
            builder.PlaceImagePlate();
        }
        if (mouseClick.WasPressedThisFrame()) {
            builder.SelectNode(mainCamera.ScreenPointToRay(Input.mousePosition));
        }
        if (rotateLeft.WasPerformedThisFrame()) {
            builder.RotateLeft();
        }
        if (rotateRight.WasPerformedThisFrame()) {
            builder.RotateRight();
        }
    }
}
