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
    private GameObject arrow, questionMark;
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
        }
        else if(type == AgentType.MacroscopicMagnet) {
            this.transform.Rotate(0,90,Globals.POSSIBLE_MACROSCOPIC_ANGLES[random.Next(Globals.POSSIBLE_MACROSCOPIC_ANGLES.Count)],Space.Self);
        }

    }
    void Update()
    {
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
            this.angle = currentNode.GetRotation(this.gameObject);
            // switch from "-->" to "?"
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
        var imgplate = ((ImagePlate)currentNode); //added code
        imgplate.ShowIndicator();
        // Commented out because this is broken on the fix branch
        //imgplate.collapseCount++; //added code
        //imgplate.textCount.text = imgplate.collapseCount.ToString(); //added code
    }
    void Collapse() {
        if (!enteredFirstMagnet) {
            nextNode = currentNode.children[0];
            enteredFirstMagnet = !enteredFirstMagnet;
            return;
        }
        if (agentType == AgentType.SilverAtom) {
            //changed to nexnode
            int magnetRotation = currentNode.GetRotation(this.gameObject);
            print("Last rotation: " + angle + " This rotation: " + magnetRotation);
            // collapse to random node
            int choice = 0;
            if (angle != magnetRotation) {
                choice = random.Next(0, 2);
                nextNode = currentNode.children[choice];
                this.angle = nextNode.GetRotation(this.gameObject);
            }
            // collapse to node parallel with our angle
            else {
                this.angle = magnetRotation;
                if (lastCollapse == CollapseType.UpState) {
                    choice = 1;
                }
                nextNode = currentNode.children[choice];
            }
            if (choice == 0) {
                lastCollapse = CollapseType.DownState;
            } else {
                lastCollapse = CollapseType.UpState;
            }
            // make necessary visual changes
            if (!(currentNode is Source)) {
                this.arrow.transform.LookAt(nextNode.GetStartLocation);
                // adjust for model inaccuracy (arrow is facing wrong direction)
                this.arrow.transform.Rotate(-180,0,0);
                this.arrow.SetActive(true);
                this.questionMark.SetActive(false);
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
    // Check if we have a special case we need to deal with
    private bool IsSpecialCaseUpsideDown() {
        return false;
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
