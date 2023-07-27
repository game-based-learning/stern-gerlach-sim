using SternGerlach;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static SternGerlach.Source;

public class GameObjectFactory : MonoBehaviour
{
    [SerializeField] GameObject particlePrefab;
    [SerializeField] GameObject sgMagnetPrefab, imagePlatePrefab;
    [SerializeField] Source source;
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
        if(CanFireParticle()) {
            GameObject magnet = GameObject.Instantiate(particlePrefab, source.GetStartLocation, Quaternion.identity);
            //magnet.transform.parent = this.transform;
            Agent agent = magnet.AddComponent<Agent>();
            agent.Initialize(source,Agent.AgentType.MacroscopicMagnet);
        }
        else {
            Debug.Log("Incomplete setup.");
        }
    }
    private void CreateSilverAtom() {
        if(CanFireParticle()) {
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
        return largeImagePlate.GetComponentsInChildren<ImagePlate>().ToList();

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
