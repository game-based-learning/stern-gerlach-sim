using SternGerlach;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static SternGerlach.Source;

public class GameObjectFactory : MonoBehaviour
{
    [SerializeField] GameObject particlePrefab;
    [SerializeField] GameObject sgMagnetPrefab, imagePlatePrefab, emptyNodePrefab;
    [SerializeField] GameObject debugBoxPrefab;
    [SerializeField] GameObject macroSourcePrefab, silverAtomSourcePrefab;
    [SerializeField] Source source;
    [SerializeField] bool instructionFactory;
    public bool SilverAtomMode() {
        return source.type == SourceType.SilverAtom;
    }
    public bool CanUpdateSetup() {
        return this.transform.childCount == 0;
    }
    private bool CanFireParticle() {
        return CanFireParticle(source.children[0]);
    }
    private bool CanFireParticle(Node node) {
        if (node is ImagePlate) {
            return true;
        }
        if (node is SGMagnet) {
            return CanFireParticle(node.children[0]) && CanFireParticle(node.children[1]);
        }
        else {
            return false;
        }
    }
    private void AddInstructionTagIfNecessary(GameObject gameObj) {
        if (!instructionFactory) {
            return;
        }
        SetGameLayerRecursive(gameObj, LayerMask.NameToLayer("TopLayer"));
    }
    // from unity forums "ignacio-casal"
    private void SetGameLayerRecursive(GameObject gameObject, int layer)
    {
        gameObject.layer = layer;
        foreach (Transform child in gameObject.transform)
        {
            SetGameLayerRecursive(child.gameObject, layer);
        }
    }

    public Source CreateSource(bool isMacro) {
        GameObject source;
        if (isMacro) {
            source = GameObject.Instantiate(macroSourcePrefab, Vector3.zero + new Vector3(0, 25, 0), Quaternion.identity);
        }
        else {
            source = GameObject.Instantiate(silverAtomSourcePrefab, Vector3.zero + new Vector3(0, 25, 0), Quaternion.identity);
        }
        this.source = source.GetComponent<Source>();
        AddInstructionTagIfNecessary(source);
        return this.source;
    }
    public void CreateParticle() {
        if (source.type == SourceType.MacroscopicMagnet) {
            CreateMacroscopicMagnet();
        }
        else if (source.type == SourceType.SilverAtom)
        {
            CreateSilverAtom();
        }
        else {
            Debug.Log("Invalid Source Type.");
        }
    }
    private void CreateMacroscopicMagnet() {
        if (CanFireParticle()) {
            GameObject magnet = GameObject.Instantiate(particlePrefab, source.GetStartLocation, Quaternion.identity);
            Agent agent = magnet.AddComponent<Agent>();
            agent.Initialize(source, Agent.AgentType.MacroscopicMagnet);
        }
        else {
            Debug.Log("Incomplete setup.");
        }
    }
    private void CreateSilverAtom() {
        if (CanFireParticle()) {
            GameObject atom = GameObject.Instantiate(particlePrefab, source.GetStartLocation, Quaternion.identity);
            atom.transform.parent = this.transform;
            Agent agent = atom.AddComponent<Agent>();
            agent.Initialize(source, Agent.AgentType.SilverAtom);
        }
        else {
            Debug.Log("Incomplete setup.");
        }
    }
    internal List<ImagePlate> CreateLargeImagePlate(Vector3 loc) {
        GameObject largeImagePlate = GameObject.Instantiate(imagePlatePrefab, loc, Quaternion.identity);
        AddInstructionTagIfNecessary(largeImagePlate);
        return largeImagePlate.GetComponentsInChildren<ImagePlate>().ToList();

    }
    internal Source GetSource() { return source; }
    internal ImagePlate CreateImagePlate(Vector3 loc)
    {
        GameObject imagePlate = GameObject.Instantiate(imagePlatePrefab, loc, Quaternion.identity);
        AddInstructionTagIfNecessary(imagePlate);
        return imagePlate.GetComponent<ImagePlate>();
    }
    internal SGMagnet CreateSGMagnet(Vector3 loc)
    {
        GameObject sgMagnet = GameObject.Instantiate(sgMagnetPrefab, loc, Quaternion.identity);
        SGMagnet sg = sgMagnet.GetComponent<SGMagnet>();
        AddInstructionTagIfNecessary(sgMagnet);
        return sg;
    }
    internal EmptyNode CreateEmptyNode(Vector3 loc)
    {
        GameObject emptyNode = GameObject.Instantiate(emptyNodePrefab, loc, Quaternion.identity);
        AddInstructionTagIfNecessary(emptyNode);
        return emptyNode.GetComponent<EmptyNode>();
    }
}
