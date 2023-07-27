using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SternGerlach
{
    public class Source : Node
    {
        [SerializeField] Node firstMagnet;
        [SerializeField] GameObject entrance;
        [SerializeField] GameObject exit;
        [SerializeField] public SourceType type;
        public enum SourceType { MacroscopicMagnet, SilverAtom }
        private Dictionary<int,Node> nodes = new Dictionary<int, Node>();
        private Vector3 entranceLoc, exitLoc;
        public override Dictionary<int, Node> children { get => nodes; set => nodes[0] = value[0]; }
        public override Vector3 GetStartLocation { get => entranceLoc; set => entranceLoc = value; }
        public override Vector3 GetEndLocation { get => exitLoc; set => exitLoc = value; }

        public override int GetRotation(GameObject gameObj)
        {
            return -1;
        }
        // Start is called before the first frame update
        void Start()
        {
            this.entranceLoc = entrance.transform.position;
            this.exitLoc = exit.transform.position;
            if(firstMagnet != null) {
                nodes[0] = firstMagnet;
            }
        }
    }
}
