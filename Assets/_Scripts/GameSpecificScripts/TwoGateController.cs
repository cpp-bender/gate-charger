using System.Collections.Generic;
using UnityEngine;

public class TwoGateController : MonoBehaviour
{
    public OpenGateController.GateType gateType = OpenGateController.GateType.BridgeBall;
    public OpenGateController.GateNumber gateNumber = OpenGateController.GateNumber.OneBall;

    public bool isRightOpen;
    public bool isLeftOpen;

    public OpenGateController openGateController;

    public void AddCloneBall(GameObject cloneBall)
    {
        openGateController.cloneBallsRecieved.Add(cloneBall);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.CloneBall))
        {

            Debug.Log(openGateController.cloneBallsRecieved.Count);
            if (gateNumber == OpenGateController.GateNumber.TwoBall && gateType == OpenGateController.GateType.GateRightLong)
            {
                if (openGateController.cloneBallsRecieved.Count >= 2)
                {
                    Debug.Log(openGateController.cloneBallsRecieved.Count);
                    isRightOpen = true;
                    gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    gameObject.transform.GetChild(1).gameObject.SetActive(true);
                }
                else if (openGateController.cloneBallsRecieved.Count < 2)
                {
                    isRightOpen = false;
                }
            }
            else if (gateNumber == OpenGateController.GateNumber.TwoBall && gateType == OpenGateController.GateType.GateLeftLong)
            {
                if (openGateController.cloneBallsRecieved.Count >= 2)
                {
                    isLeftOpen = true;
                    gameObject.transform.GetChild(1).gameObject.SetActive(true);
                    gameObject.transform.GetChild(0).gameObject.SetActive(true);
                }
                else if (openGateController.cloneBallsRecieved.Count < 2)
                {
                    isLeftOpen = false;
                }
            }
        }
    }
}
