using System.Collections;
using System.Collections.Generic;
using SternGerlach;
using UnityEngine;

public class Agent : MonoBehaviour
{
    private Node currentNode, nextNode;
    private System.Random random;
    enum AgentState { WithinNode, BetweenNodes }
    public enum AgentType { MacroscopicMagnet, SilverAtom }
    private AgentState agentState;
    private AgentType agentType;
    private int angle;
    private GameObject arrow, questionMark;
    public void Initialize(Node firstNode, AgentType type)  {
        this.agentState = AgentState.WithinNode;
        this.agentType = type;
        this.currentNode = firstNode;
        this.angle = -1;
        this.random = new System.Random();
        if (type == AgentType.SilverAtom) {
            this.arrow = this.transform.Find("Arrow").gameObject;
            this.questionMark = this.transform.Find("QuestionMark").gameObject;
            this.arrow.SetActive(false);
            this.questionMark.SetActive(true);
        }
        else if(type == AgentType.MacroscopicMagnet) {
            this.transform.Rotate(0,0,Globals.POSSIBLE_MACROSCOPIC_ANGLES[random.Next(Globals.POSSIBLE_MACROSCOPIC_ANGLES.Count)],Space.Self);
        }

    }
    void Update()
    {
        if (agentState.Equals(AgentState.WithinNode))
        {
            if (!transform.position.Equals(currentNode.GetEndLocation)) {
                StepToward(currentNode.GetEndLocation);
                return;
            }
            if (currentNode.children != null)
            {
                if (currentNode is Furnace) {
                    nextNode = currentNode.children[0];
                }
                ExitMagnet();
                return;
            }
            else {
                Destroy(this.gameObject);
                if (currentNode is ImagePlate) {
                    HitImagePlate();
                }
            }
        }
        else if (agentState.Equals(AgentState.BetweenNodes)) {
            if (!transform.position.Equals(nextNode.GetStartLocation))
            {
                StepToward(nextNode.GetStartLocation);
                return;
            }
            else {
                EnterMagnet();
            }
        }
    }
    void StepToward(Vector3 location) {
        transform.position = Vector3.MoveTowards(transform.position, location, 3f * Time.deltaTime);
    }
    void EnterMagnet()
    {
        if (agentType == AgentType.SilverAtom) {
            this.angle = currentNode.GetRotation(this.gameObject);
            this.arrow.SetActive(false);
            this.questionMark.SetActive(true);
        }
        currentNode = nextNode;
        agentState = AgentState.WithinNode;
    }
    void ExitMagnet() {
        Collapse();
        agentState = AgentState.BetweenNodes;
    }
    void HitImagePlate()
    {
        ((ImagePlate)currentNode).ShowIndicator();
    }
    void Collapse() {
        if (agentType == AgentType.SilverAtom) {
            int magnetRotation = currentNode.GetRotation(this.gameObject);
            // collapse to random node
            if (angle != magnetRotation)
            {
                nextNode = currentNode.children[random.Next(0, 2)];
                this.angle = nextNode.GetRotation(this.gameObject);
            }
            // collapse to node parallel with our angle
            else {
                this.angle = magnetRotation;
                if (angle > 0) {
                    nextNode = currentNode.children[1];
                }
                else {
                    nextNode = currentNode.children[0];
                }
            }
            // make necessary visual changes
            if (!(currentNode is Furnace)) {
                this.arrow.transform.LookAt(nextNode.GetStartLocation);
                this.arrow.transform.Rotate(-180,0,0);
                this.arrow.SetActive(true);
                this.questionMark.SetActive(false);
            }
        }
        else if(agentType == AgentType.MacroscopicMagnet && currentNode is Magnet) {
            Debug.Log("transform rotation" + transform.localRotation.eulerAngles.z);
            int destination = MacroscopicCollapseHelper(transform.localRotation.eulerAngles.z);
            Debug.Log("destination" + destination);
            Debug.Log("Current node is magnet: " + currentNode is Magnet);
            Debug.Log("Current node children count:" + currentNode.children.Count);
            nextNode = currentNode.children[destination];
        }
        else {
            Debug.Log("Unexpected agent type.");
        }
    }
    private int MacroscopicCollapseHelper(float angle) {
        for(int i = 0; i < 7; i++) {
            if(((i*30 - 15) <= angle)  && ((15 + i*30) >= angle)) {
                return i;
            }
        }
        if(angle >= 0 && angle < 15f) {
            return 0;
        }
        if(angle >= 15f && angle < 45f) {
            return 1;
        } 
        if(angle >= 45f && angle < 75f) {
            return 2;
        }
        if(angle >= 75f && angle < 105f) {
            return 3;
        }
        if(angle >= 105f && angle < 135f) {
            return 4;
        }
        if(angle >= 135f && angle < 165f) {
            return 5;
        }
        if(angle >= 165f) {
            return 6;
        }
        return -1;
    }
}
