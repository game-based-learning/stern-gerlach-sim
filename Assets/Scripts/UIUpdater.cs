using UnityEngine;
using UnityEngine.InputSystem;
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

        private Button closewarning;
        private bool thisIsFirstEdit = true;

        //private VisualElement clickoffcontainer;
        private Button clickoffbutton;
        private bool sceneHasOneSGMagnet;


        public States state = States.UI_CLOSED;
        private (float x, float y) popupPosition;

        private InputAction ubutton;
        private bool showUI = true;

        public enum States
        {
            UI_CLOSED,
            UI_OPEN,
        }
        // Start is called before the first frame update
        public void Initialize(InputAction u)
        {
            u.Enable();
            ubutton = u;
        }
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

                closewarning = root.Q<Button>("closewarning");

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

                closewarning.clicked += WarningClosePressed;
            } else
            {
                guidedbutton = root.Q<Button>("guided-mode");
                freeplaybutton = root.Q<Button>("freeplay-mode");
                helpbutton = root.Q<Button>("help");

                guidedbutton.clicked += GuidedModeButtonPressed;
            }
        }

        void Update()
        {
            if (ubutton.WasPressedThisFrame())
            {
                if (showUI)
                {
                    root.style.display = DisplayStyle.None;
                    showUI = false;
                } else
                {
                    root.style.display = DisplayStyle.Flex;
                    showUI = true;
                }
            }
            Modify();
        }
        private void WarningClosePressed()
        {
            root.Q<VisualElement>("Warning").visible = false;
        }
        private void GuidedModeButtonPressed()
        {
            scenechanger.changeScene("MacroscopicNodeBuilder");
            //isMacro = true;
        }

        private void DeleteButtonPressed()
        {
            Debug.Log("use this: " + builder.selectedNode.name);
            if (builder.selectedNode.name == "S-G Magnet(Clone)")
            {
                sceneHasOneSGMagnet = false;
                sgbutton.visible = bc.visible;
            }
            Debug.Log(sceneHasOneSGMagnet);
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
            Modify();
            if (SceneManager.GetActiveScene().name == "MacroscopicNodeBuilder")
            {
                sceneHasOneSGMagnet = true;
                state = States.UI_CLOSED;
                return;
            }
            CloseButtonPressed();
            RotationPopup();
            
        }

        private void ImagePlateButtonPressed()
        {
            builder.PlaceImagePlate();
            bc.visible = false;
            clickoffbutton.visible = false;
            state = States.UI_CLOSED;
            Modify();
        }


        public void PopupDialog(Vector3 position)
        {
            /*if (isMacro && )
            {
                root.Q<Button>("imageplate-button").visible = false;
            }*/
            Debug.Log("before popup check: " + sceneHasOneSGMagnet);
            bc.visible = true;
            if (sceneHasOneSGMagnet)
            {
                sgbutton.visible = false;
            }
            else
            {
                sgbutton.style.visibility = StyleKeyword.Null;
                //Debug.Log("sgbutton visiblity: " + sgbutton.visible + ", parent visibility: " + sgbutton.parent.visible);
            }
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
            if (thisIsFirstEdit)
            {
                root.Q<VisualElement>("Warning").visible = true;
                thisIsFirstEdit = false;
            }
            dc.visible = true;
            
            dc.style.left = position.x;
            dc.style.top = Screen.height - position.y;
            clickoffbutton.visible = true;
        }

        public void Modify()
        {
            if(builder.selectedNode == null) { return; }
            var n = builder.selectedNode.name;
            //Debug.Log("Modify called, selected Node: " + n);

            switch (n)
            {
                case "Image Plate(Clone)":
                    root.Q<Label>("FocusName").text = "Image Plate";
                    //Debug.Log("Modifying imgplate");
                    ImagePlateMod(builder.selectedNode);
                    root.Q<VisualElement>("side").style.display = DisplayStyle.Flex;
                    root.Q<VisualElement>("side-2").style.display = DisplayStyle.None;
                    break;
                case "S-G Magnet(Clone)":
                    root.Q<Label>("FocusName").text = "S-G Magnet";
                    SGMagnetMod(builder.selectedNode);
                    root.Q<VisualElement>("side").style.display = DisplayStyle.Flex;
                    root.Q<VisualElement>("side-2").style.display = DisplayStyle.None;
                    break;
                case "SG Magnet(Clone)":
                    root.Q<Label>("FocusName").text = "S-G Magnet";
                    SGMagnetMod(builder.selectedNode);
                    root.Q<VisualElement>("side").style.display = DisplayStyle.Flex;
                    root.Q<VisualElement>("side-2").style.display = DisplayStyle.None;
                    break;
                case "Silver Atom":
                    SilverAtomMod(builder.selectedNode);
                    root.Q<VisualElement>("side").style.display = DisplayStyle.Flex;
                    root.Q<VisualElement>("side-2").style.display = DisplayStyle.None;
                    break;
                case "Classical Magnet":
                    ClassicalMagnetMod(builder.selectedNode);
                    root.Q<VisualElement>("side").style.display = DisplayStyle.Flex;
                    root.Q<VisualElement>("side-2").style.display = DisplayStyle.None;
                    break;
                case "LargeImagePlate":
                    root.Q<Label>("FocusName").text = "Large Image Plate";
                    root.Q<VisualElement>("side").style.display = DisplayStyle.Flex;
                    root.Q<VisualElement>("side-2").style.display = DisplayStyle.None;
                    //LargeImagePlateMod(builder.selectedNode);
                    break;
                case "EmptyNode":
                    break;
                default:
                    break;
            }
            
        }

        private void ImagePlateMod(Node sn)
        {
            var count = sn.GetComponent<ImagePlate>().textCount.text;
            root.Q<Label>("1").text = "Total Particles Hit: " + count + "\n";
        }

        private void SGMagnetMod(Node sn)
        {
            Debug.Log("modifying sgmagnet ui");

            var orientation = sn.GetComponent<SGMagnet>().GetRotation();
            root.Q<Label>("1").text = "Orientation: " + orientation + " clockwise\n";
        }

        private void SilverAtomMod(Node sn)
        {
            var spin = sn.GetComponent<Agent>().lastCollapse;
            root.Q<Label>("1").text = "Spin: " + spin + "\n";
        }

        private void ClassicalMagnetMod(Node sn)
        {
            var orientation = sn.GetComponent<Agent>().angle;
            root.Q<Label>("1").text = "Orientation: " + orientation + " clockwise\n";
        }

        public float PredictionToggle(float pos)
        {
            var predictionbox = root.Q<VisualElement>("predictionpopup");
            predictionbox.style.bottom = new StyleLength(new Length(pos, LengthUnit.Percent));
            if (pos < 0) {
                return 0;
            } else
            {
                return -35;
            }
        }

        /*private void LargeImagePlateMod(Node sn)
        {
        }*/
    }
}
