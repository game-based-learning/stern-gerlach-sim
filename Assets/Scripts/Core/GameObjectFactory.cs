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
    public Agent CreateMacroscopicMagnet() {
        GameObject magnet = GameObject.Instantiate(macroscopicMagnetPrefab, firstNode.GetStartLocation, Quaternion.identity);
        Agent agent = magnet.AddComponent<Agent>();
        agent.Initialize(firstNode,Agent.AgentType.MacroscopicMagnet);
        return agent;
    }
    public Agent CreateSilverAtom() {
        GameObject atom = GameObject.Instantiate(silverAtomPrefab, firstNode.GetStartLocation, Quaternion.identity);
        Agent agent = atom.AddComponent<Agent>();
        agent.Initialize(firstNode, Agent.AgentType.SilverAtom);
        return agent;
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
