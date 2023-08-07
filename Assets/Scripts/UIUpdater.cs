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
        private VisualElement rc;
        private VisualElement dc;

        private Button deletebutton;
        private Button dclosebutton;
        private Button closebutton;
        private Button sgbutton;
        private Button ipbutton;
        private Button rleftbutton;
        private Button rrightbutton;
        private Button rclosebutton;
        public States state = States.UI_CLOSED;
        private (float x, float y) popupPosition;

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
            rc = root.Q<VisualElement>("RotationContainer");
            dc = root.Q<VisualElement>("DeleteContainer");
            closebutton = root.Q<Button>("close-button");
            sgbutton = root.Q<Button>("sgmagnet-button");
            ipbutton = root.Q<Button>("imageplate-button");

            deletebutton = root.Q<Button>("delete-button");
            dclosebutton = root.Q<Button>("dclose-button");

            deletebutton.clicked += DeleteButtonPressed;
            dclosebutton.clicked += CloseButtonPressed;

            closebutton.clicked += CloseButtonPressed;
            sgbutton.clicked += SGMagnetButtonPressed;
            ipbutton.clicked += ImagePlateButtonPressed;


            rleftbutton = root.Q<Button>("rotate-left");
            rrightbutton = root.Q<Button>("rotate-right");
            rclosebutton = root.Q<Button>("rclose-button");

            rleftbutton.clicked += RotateLeftButtonPressed;
            rrightbutton.clicked += RotateRightButtonPressed;
            rclosebutton.clicked += CloseButtonPressed;
        }

        private void DeleteButtonPressed()
        {
            builder.DeleteNode();
            dc.visible = false;
            state = States.UI_CLOSED;
        }

        private void RotateLeftButtonPressed()
        {
            builder.RotateLeft();
            //rc.visible = false;
            //state = States.UI_CLOSED;
        }
        private void RotateRightButtonPressed()
        {
            builder.RotateRight();
            //rc.visible = false;
            //state = States.UI_CLOSED;
        }
        private void CloseButtonPressed()
        {
            bc.visible = false;
            rc.visible = false;
            dc.visible = false;
            state = States.UI_CLOSED;
        }
        private void SGMagnetButtonPressed()
        {
            builder.PlaceSGMagnet();
            bc.visible = false;
            RotationPopup();
            //state = States.UI_CLOSED;
            Modify();
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
            popupPosition = (position.x, Screen.height - position.y);
        }

        public void RotationPopup()
        {
            rc.visible = true;
            rc.style.left = popupPosition.x;
            rc.style.top = popupPosition.y;
        }

        public void DeletePopup()
        {
            dc.visible = true;
            dc.style.left = popupPosition.x;
            dc.style.top = popupPosition.y;
        }

        public void Modify()
        {
            root.Q<Label>("FocusName").text = builder.selectedNode.name;
            /*switch (t.name)
            {
                case "LargeImagePlate":
                    LargeImagePlateMod();
                    break;
                default:
                    break;
            }*/
        }
    }
}
