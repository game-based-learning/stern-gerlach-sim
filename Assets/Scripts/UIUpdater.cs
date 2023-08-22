using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace SternGerlach
{
    public class UIUpdater : MonoBehaviour
    {
        [SerializeField] public Transform[] focusables;
        [SerializeField] UIDocument ui;
        [SerializeField] public NodeBuilder builder;
        [SerializeField] SceneChanger scenechanger;

        private bool isMain = false;
        //private bool isMacro = false;

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

        private Button guidedbutton;
        private Button freeplaybutton;
        private Button helpbutton;

        //private VisualElement clickoffcontainer;
        private Button clickoffbutton;

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
            Debug.Log(SceneManager.GetActiveScene().name);
            if(SceneManager.GetActiveScene().name == "AlphaBuild")
            {
                isMain = true;
            }
            root = ui.rootVisualElement;

            Debug.Log(isMain);
            if (!isMain)
            {
                bc = root.Q<VisualElement>("DialogPopup");
                rc = root.Q<VisualElement>("RotationContainer");
                dc = root.Q<VisualElement>("DeleteContainer");

                /*clickoffcontainer = root.Q<VisualElement>("ClickOff");

                clickoffcontainer.AddManipulator(new Clickable(evt => {
                    if(state == States.UI_OPEN)
                    {
                        CloseButtonPressed();
                    }
                }));*/


                clickoffbutton = root.Q<Button>("ClickOff");
                clickoffbutton.clicked += CloseButtonPressed;
                clickoffbutton.visible = false;

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
            } else
            {
                guidedbutton = root.Q<Button>("guided-mode");
                freeplaybutton = root.Q<Button>("freeplay-mode");
                helpbutton = root.Q<Button>("help");

                guidedbutton.clicked += GuidedModeButtonPressed;
            }
        }

        /*void Update()
        {
            if (SceneManager.GetActiveScene().name == "MacroscopicNodeBuilder")
            {
                isMain = true;
            }
        }*/
        private void GuidedModeButtonPressed()
        {
            scenechanger.changeScene("MacroscopicNodeBuilder");
            //isMacro = true;
        }

        private void DeleteButtonPressed()
        {
            builder.DeleteNode();
            dc.visible = false;
            clickoffbutton.visible = false;
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
            clickoffbutton.visible = false;
            state = States.UI_CLOSED;
        }
        private void SGMagnetButtonPressed()
        {
            builder.PlaceSGMagnet();
            bc.visible = false;
            clickoffbutton.visible = false;
            RotationPopup();
            //state = States.UI_CLOSED;
            Modify();
        }

        private void ImagePlateButtonPressed()
        {
            builder.PlaceImagePlate();
            bc.visible = false;
            clickoffbutton.visible = false;
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
            /*if (isMacro && )
            {
                root.Q<Button>("imageplate-button").visible = false;
            }*/
            bc.visible = true;
            clickoffbutton.visible = true;
            bc.style.left = position.x;
            bc.style.top = Screen.height - position.y;
            popupPosition = (position.x, Screen.height - position.y);
            Debug.Log("at PopupDialog: " + popupPosition);
        }

        public void RotationPopup()
        {
            rc.visible = true;
            clickoffbutton.visible = true;
            rc.style.left = popupPosition.x;
            rc.style.top = popupPosition.y;
            Debug.Log("at RotationPopup: " + popupPosition);
        }

        public void DeletePopup(Vector3 position)
        {
            dc.visible = true;
            
            dc.style.left = position.x;
            dc.style.top = Screen.height - position.y;
            clickoffbutton.visible = true;
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
