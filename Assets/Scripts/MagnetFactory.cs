using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetFactory : MonoBehaviour
{
    [SerializeField] GameObject macroscopicMagnetPrefab;
    [SerializeField] GameObject silverAtomPrefab;
    [SerializeField] Node firstNode;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Agent createMacroscopicMagnet() {
        GameObject magnet = GameObject.Instantiate(macroscopicMagnetPrefab, firstNode.GetStartLocation, Quaternion.identity);
        Agent agent = magnet.AddComponent<MacroscopicMagnet>();
        agent.setInitNode(firstNode);
        return agent;
    }
    public Agent createSilverAtom() {
        GameObject atom = GameObject.Instantiate(silverAtomPrefab, firstNode.GetStartLocation, Quaternion.identity);
        Agent agent = atom.AddComponent<SilverAtom>();
        agent.setInitNode(firstNode);
        return agent;
    }
}
