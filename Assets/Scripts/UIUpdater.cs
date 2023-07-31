using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

namespace SternGerlach
{
    public class UIUpdater : MonoBehaviour
    {
        [SerializeField] public Transform[] focusables;
        [SerializeField] UIDocument ui;
        [SerializeField] public NodeBuilder builder;
        private VisualElement root;
        private VisualElement bc;
        private Button closebutton;
        private Button sgbutton;
        private Button ipbutton;
        public States state = States.UI_CLOSED;

        public enum States
        {
            UI_CLOSED,
            UI_OPEN,
        }
        // Start is called before the first frame update
        void Start()
        {
            root = ui.rootVisualElement;
            bc = root.Q<VisualElement>("DialogPopup");
            closebutton = root.Q<Button>("close-button");
            sgbutton = root.Q<Button>("sgmagnet-button");
            ipbutton = root.Q<Button>("imageplate-button");

            closebutton.clicked += CloseButtonPressed;
            sgbutton.clicked += SGMagnetButtonPressed;
            ipbutton.clicked += ImagePlateButtonPressed;
        }
        private void CloseButtonPressed()
        {
            bc.visible = false;
            state = States.UI_CLOSED;
        }
        private void SGMagnetButtonPressed()
        {
            builder.PlaceSGMagnet();
            bc.visible = false;
            state = States.UI_CLOSED;
        }

        private void ImagePlateButtonPressed()
        {
            builder.PlaceImagePlate();
            bc.visible = false;
            state = States.UI_CLOSED;
        }

        private void LargeImagePlateMod()
        {
            root.Q<Label>("1").text = "Total Particles Hit: \n";
            root.Q<Label>("2").text = "Distribution of particles: \n";
            root.Q<Label>("3").text = "Last Particle hit ";
        }

        public void PopupDialog(Vector3 position)
        {
            bc.visible = true;
            bc.style.left = position.x;
            bc.style.top = Screen.height - position.y;
        }

        public void Modify(Transform t)
        {
            root.Q<Label>("FocusName").text = t.name;
            switch (t.name)
            {
                case "LargeImagePlate":
                    LargeImagePlateMod();
                    break;
                default:
                    break;
            }
        }
    }
}
