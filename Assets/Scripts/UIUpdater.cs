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
        private VisualElement root;
        private VisualElement bc;
        private Button closebutton;
        // Start is called before the first frame update
        void Start()
        {
            root = ui.rootVisualElement;
            bc = root.Q<VisualElement>("DialogPopup");
            closebutton = root.Q<Button>("close-button");

            closebutton.clicked += CloseButtonPressed;
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

        private void CloseButtonPressed()
        {
            bc.visible = false;
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
