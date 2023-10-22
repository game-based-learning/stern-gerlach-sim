using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SternGerlach
{
    public class Source : Node
    {
        [SerializeField] public Node firstMagnet;
        [SerializeField] GameObject entrance;
        [SerializeField] GameObject exit;
        [SerializeField] GameObject magnetSprite, questionSprite;
        [SerializeField] public SourceType type;
        public enum SourceType { MacroscopicMagnet, SilverAtom }
        public Dictionary<int,Node> nodes = new Dictionary<int, Node>();
        private Vector3 entranceLoc, exitLoc;
        public override Dictionary<int, Node> children { get => nodes; set => nodes[0] = value[0]; }
        public override Vector3 GetStartLocation { get => entranceLoc; set => entranceLoc = value; }
        public override Vector3 GetEndLocation { get => exitLoc; set => exitLoc = value; }

        // Start is called before the first frame update
        public void SetFirstNode(Node node) { 
            firstMagnet = node;
            nodes[0] = node;
        }
        void Start()
        {
            this.entranceLoc = entrance.transform.position;
            this.exitLoc = exit.transform.position;
            if(firstMagnet != null) {
                nodes[0] = firstMagnet;
            }
            if (magnetSprite == null && questionSprite == null) {
                return;
            }
            magnetSprite.SetActive(type == SourceType.MacroscopicMagnet);
            questionSprite.SetActive(type == SourceType.SilverAtom);
        }
        void SetSourceType(SourceType type)
        {
            this.type = type;
            magnetSprite.SetActive(type == SourceType.MacroscopicMagnet);
            questionSprite.SetActive(type == SourceType.SilverAtom);
        }
        public override int GetRotation() { 
            return -1; 
        }
    }
}
