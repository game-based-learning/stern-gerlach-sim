using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SternGerlach
{
    public class EmptyNode : Node
    {
        public override Dictionary<int, Node> children { get => new Dictionary<int,Node>(); set => throw new System.NotImplementedException(); }
        public override Vector3 GetStartLocation { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public override Vector3 GetEndLocation { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    }
}
