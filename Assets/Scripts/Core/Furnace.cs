using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SternGerlach
{
    public class Furnace : Node
    {
        [SerializeField] GameObject furnace;
        [SerializeField] Node firstMagnet;
        [SerializeField] GameObject entrance;
        [SerializeField] GameObject exit;
        private Vector3 entranceLoc, exitLoc;
        public override Dictionary<int, Node> children { get => new() { { 0, firstMagnet } }; set => firstMagnet = value[0]; }
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
        }
    }
}
