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
    private InputAction freeze, cortest;
    private float cooldown = 0f;
    [SerializeField] GameObjectFactory factory;
    [SerializeField] NodeBuilder builder;
    [SerializeField] Camera mainCamera;
    [SerializeField] GuidedComponent guidedComponent;
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
        freeze = controls.Control.Freeze;
        cortest = controls.Control.FindCOR;
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
                if (GameManager.Instance.GetGameState() == GameManager.GameState.FROZEN) {
                    GameManager.Instance.ToggleFreeze();
                }
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
        //if (mouseClick.WasPressedThisFrame()) {
        //    builder.SelectNode(mainCamera.ScreenPointToRay(Input.mousePosition));
        //}
        if (rotateLeft.WasPressedThisFrame()) {
            builder.RotateLeft();
        }
        if (rotateRight.WasPressedThisFrame()) {
            builder.RotateRight();
        }
        if (deleteNode.WasPressedThisFrame()) {
            builder.DeleteNode();
        }
        if (freeze.WasPressedThisFrame()) {
            GameManager.Instance.ToggleFreeze();
        }
    }
}
