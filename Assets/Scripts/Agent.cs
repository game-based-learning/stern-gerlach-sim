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
        if (type == AgentType.SilverAtom) {
            this.arrow = this.transform.Find("Arrow").gameObject;
            this.questionMark = this.transform.Find("QuestionMark").gameObject;
            this.arrow.SetActive(false);
            this.questionMark.SetActive(true);
        }
    }
    void Start()
    {
        this.random = new System.Random();
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
            this.arrow.transform.rotation = Quaternion.Euler(NewArrowAngle(nextNode), 90, 0);
            this.arrow.SetActive(true);
            this.questionMark.SetActive(false);
        }

    }
    int NewArrowAngle(Node node) {
        return -Mathf.FloorToInt(Vector2.Angle(transform.position, node.GetStartLocation));
    }
}
