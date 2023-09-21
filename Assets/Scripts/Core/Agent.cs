using System.Collections;
using System.Collections.Generic;
using SternGerlach;
using UnityEngine;

public class Agent : MonoBehaviour
{
    private Node currentNode, nextNode;
    private System.Random random;
    enum AgentState { WithinNode, BetweenNodes }
    enum CollapseType { UpState, DownState }
    public enum AgentType { MacroscopicMagnet, SilverAtom }
    private bool enteredFirstMagnet = false;
    private AgentState agentState;
    private AgentType agentType;
    private CollapseType lastCollapse;
    private int angle;
    private GameObject arrow, questionMark, upCaret, downCaret;
    public void Initialize(Node firstNode, AgentType type)  {
        this.agentState = AgentState.WithinNode;
        this.agentType = type;
        this.currentNode = firstNode;
        this.angle = -1;
        this.random = new System.Random();
        if (type == AgentType.SilverAtom) {
            this.arrow = this.transform.Find(Globals.ARROW_NAME).gameObject;
            this.questionMark = this.transform.Find(Globals.QUESTION_MARK_NAME).gameObject;
            this.arrow.SetActive(false);
            this.questionMark.SetActive(true);
            this.upCaret = this.transform.Find(Globals.UP_CARET_NAME).gameObject;
            this.downCaret = this.transform.Find(Globals.DOWN_CARET_NAME).gameObject;
            return;
        }
        if(type == AgentType.MacroscopicMagnet) {
            this.transform.Rotate(0,90,Globals.POSSIBLE_MACROSCOPIC_ANGLES[random.Next(Globals.POSSIBLE_MACROSCOPIC_ANGLES.Count)],Space.Self);
        }

    }
    void Update()
    {
        //Debug.Log(angle);
        if (GameManager.Instance.GetGameState() == GameManager.GameState.FROZEN) {
            // no updates if we are frozen
            return;
        }
        if (agentState.Equals(AgentState.WithinNode))
        {
            if (!transform.position.Equals(currentNode.GetEndLocation)) {
                StepToward(currentNode.GetEndLocation);
                return;
            }
            if (currentNode.children != null)
            {
                if (currentNode is Source) {
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
            // set angle of agent to previous node's rotation
            this.angle = currentNode.GetRotation();

            // switch from "-->" to "?"
            this.arrow.SetActive(false);
            this.questionMark.SetActive(true);
            HideCaret();
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
        var imgplate = ((ImagePlate)currentNode); //added code
        imgplate.ShowIndicator();
        imgplate.collapseCount++; //added code
        imgplate.textCount.text = imgplate.collapseCount.ToString(); //added code
    }
    void Collapse() {
        if (!enteredFirstMagnet) {
            nextNode = currentNode.children[0];
            enteredFirstMagnet = !enteredFirstMagnet;
            return;
        }
        if (agentType == AgentType.SilverAtom) {
            //changed to next node
            int magnetRotation = currentNode.GetRotation();
            print("Last rotation: " + angle + " This rotation: " + magnetRotation);
            // collapse to random node
            int choice = 0;
            if (angle != magnetRotation) {
                choice = random.Next(0, 2);
                nextNode = currentNode.children[choice];
                this.angle = nextNode.GetRotation();
            }
            // collapse to node parallel with our angle
            else {
                this.angle = magnetRotation;
                if (lastCollapse == CollapseType.UpState) {
                    choice = 1;
                }
                nextNode = currentNode.children[choice];
            }

            // Update collapse to our choice
            lastCollapse = (choice == 0) ? CollapseType.DownState : CollapseType.UpState;

            // make necessary visual changes
            if (!(currentNode is Source)) {
                this.transform.eulerAngles = this.currentNode.GetAbsoluteRotation();
                // Rotate up if up or down if down!
                this.transform.Rotate(0, 0, lastCollapse == CollapseType.UpState 
                    ? Globals.ANGLE_BETWEEN_NODES : -Globals.ANGLE_BETWEEN_NODES, Space.Self);
                this.arrow.SetActive(true);
                this.questionMark.SetActive(false);
                ShowCaret();
            }
        }
        else if(agentType == AgentType.MacroscopicMagnet && currentNode is SGMagnet) {
            int destination = MacroscopicCollapseHelper(transform.localRotation.eulerAngles.z);
            nextNode = currentNode.children[destination];
        }
        else {
            Debug.Log("Unexpected agent type.");
        }
    }
    void ShowCaret() {
        if (lastCollapse == CollapseType.UpState)
        {
            this.upCaret.SetActive(true);
        }
        else { 
            this.downCaret.SetActive(true);
        }
    }
    void HideCaret() {
        this.upCaret.SetActive(false);
        this.downCaret.SetActive(false);
    }
    private int MacroscopicCollapseHelper(float angle) {
        for(int i = 0; i < 7; i++) {
            if(((i*30 - 15) <= angle)  && ((15 + i*30) >= angle)) {
                return i;
            }
        }
        return -1;
    }
}
