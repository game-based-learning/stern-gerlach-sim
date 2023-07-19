using System.Collections;
using System.Collections.Generic;
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

        public void Modify(Transform t)
        {
            root.Q<Label>("FocusName").text = t.name;
        }
    }
}
