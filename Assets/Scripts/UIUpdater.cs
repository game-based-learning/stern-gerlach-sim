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
        // Start is called before the first frame update
        void Start()
        {
            root = ui.rootVisualElement;
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void LargeImagePlateMod()
        {
             root.Q<Label>("1").text = "Total Particles Hit: \n";
            root.Q<Label>("2").text = "Distribution of particles: \n";
            root.Q<Label>("3").text = "Last Particle hit ";
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
