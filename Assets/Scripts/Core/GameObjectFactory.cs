using UnityEngine;

public class GameObjectFactory : MonoBehaviour
{
    [SerializeField] GameObject macroscopicMagnetPrefab, silverAtomPrefab;
    [SerializeField] GameObject sgMagnetPrefab, imagePlatePrefab;
    [SerializeField] Node firstNode;

    public bool CanUpdateSetup() {
        return this.transform.childCount == 0;
    }
    private bool CanFireParticle() {
        return CanFireParticle(firstNode.children[0]);
    }
    private bool CanFireParticle(Node node) {
        if(node is ImagePlate) {
            return true;
        }
        if(node is SGMagnet) {
            return CanFireParticle(node.children[0]) && CanFireParticle(node.children[1]);
        }
        else {
            return false;
        }
    }
    public void CreateMacroscopicMagnet() {
        if(CanFireParticle()) {
            GameObject magnet = GameObject.Instantiate(macroscopicMagnetPrefab, firstNode.GetStartLocation, Quaternion.identity);
            //magnet.transform.parent = this.transform;
            Agent agent = magnet.AddComponent<Agent>();
            agent.Initialize(firstNode,Agent.AgentType.MacroscopicMagnet);
        }
        else {
            Debug.Log("Incomplete setup.");
        }
    }
    public void CreateSilverAtom() {
        if(CanFireParticle()) {
            GameObject atom = GameObject.Instantiate(silverAtomPrefab, firstNode.GetStartLocation, Quaternion.identity);
            atom.transform.parent = this.transform;
            Agent agent = atom.AddComponent<Agent>();
            agent.Initialize(firstNode, Agent.AgentType.SilverAtom);
        }
        else {
            Debug.Log("Incomplete setup.");
        }
    }

    internal ImagePlate CreateImagePlate(Vector3 loc)
    {
        GameObject imagePlate = GameObject.Instantiate(imagePlatePrefab, loc, Quaternion.identity);
        return imagePlate.GetComponent<ImagePlate>();
    }
    internal SGMagnet CreateSGMagnet(Vector3 loc)
    {
        GameObject imagePlate = GameObject.Instantiate(sgMagnetPrefab, loc, Quaternion.identity);
        return imagePlate.GetComponent<SGMagnet>();
    }
}
