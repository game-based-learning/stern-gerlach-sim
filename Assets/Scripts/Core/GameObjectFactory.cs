using UnityEngine;

public class GameObjectFactory : MonoBehaviour
{
    [SerializeField] GameObject macroscopicMagnetPrefab, silverAtomPrefab;
    [SerializeField] GameObject sgMagnetPrefab, imagePlatePrefab;
    [SerializeField] Node firstNode;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
