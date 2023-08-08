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
    private InputAction createParticle;
    private InputAction rotateLeft, rotateRight;
    private InputAction deleteNode;
    private float cooldown = 0f;
    [SerializeField] GameObjectFactory factory;
    [SerializeField] NodeBuilder builder;
    [SerializeField] Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        controls = new InputControl();
        controls.Enable();
        createParticle = controls.Control.Silver;
        placeSGMagnet = controls.Control.PlaceSGMagnet;
        placeImagePlate = controls.Control.PlaceImagePlate;
        mouseClick = controls.Control.MouseLeftClick;
        rotateLeft = controls.Control.RotateLeft;
        rotateRight = controls.Control.RotateRight;
        deleteNode = controls.Control.DeleteNode;
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldown > 0f) {
            cooldown -= Time.deltaTime;
        }
        if (createParticle.IsPressed())
        {
            if (cooldown <= 0f) {
                factory.CreateParticle();
                cooldown = Globals.PARTICLE_COOLDOWN;
            }
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
        if (rotateLeft.WasPressedThisFrame()) {
            builder.RotateLeft();
        }
        if (rotateRight.WasPressedThisFrame()) {
            builder.RotateRight();
        }
        if (deleteNode.WasPressedThisFrame()) {
            builder.DeleteNode();
        }
    }
}
