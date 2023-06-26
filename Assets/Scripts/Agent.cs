using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Agent : MonoBehaviour
{
    private Node currentNode, nextNode;
    private System.Random random;
    enum AgentState { WithinNode, BetweenNodes }
    enum SpinState {  }
    private AgentState agentState;
    public Agent(Node initialNode) {
        this.agentState = AgentState.WithinNode;
        this.currentNode = initialNode;
        this.random = new System.Random();
    }
    public Agent() {
        this.agentState = AgentState.WithinNode;
        this.random = new System.Random();
    }
    public void setInitNode(Node node) {
        this.currentNode = node;
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
                transform.position = Vector3.MoveTowards(transform.position, currentNode.GetEndLocation, 3f * Time.deltaTime);
                return;
            }
            if (currentNode.children != null)
            {
                nextNode = currentNode.children[random.Next(0,2)];
                agentState = AgentState.BetweenNodes;
                return;
            }
            else {
                Destroy(this.gameObject);
                if (currentNode is ImagePlate) {
                    ((ImagePlate)currentNode).ShowIndicator();
                }
            }
        }
        else if (agentState.Equals(AgentState.BetweenNodes)) {
            if (!transform.position.Equals(nextNode.GetStartLocation))
            {
                transform.position = Vector3.MoveTowards(transform.position, nextNode.GetStartLocation, 3f * Time.deltaTime);
                return;
            }
            else {
                currentNode = nextNode;
                agentState = AgentState.WithinNode;
            }
        }
        /**Debug.Log("fgji");
        if (agentState.Equals(AgentState.WithinNode)) {
            Debug.Log(transform.position);
            Debug.Log(currentNode.GetEndLocation);
            if (!transform.position.Equals(currentNode.GetEndLocation)) {
                transform.position = Vector3.Lerp(currentNode.GetStartLocation, currentNode.GetEndLocation, 10f * Time.deltaTime);
            }
        }**/
        //if(!transform.position.Equals(currentNode.get))
    }
    /**public abstract chooseNextNode() { 
    
    }**/
}
