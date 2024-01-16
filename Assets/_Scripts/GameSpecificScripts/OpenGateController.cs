using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OpenGateController : MonoBehaviour
{
    private ReferenceManager referenceManager;
    private BallsInTubeController ballsInTubeController;

    public List<GameObject> cloneBallsRecieved = new List<GameObject>();
    public bool isActiveBridge = false;

    public enum GateNumber { OneBall, TwoBall, ThreeBall }
    public GateNumber gateNumber = GateNumber.OneBall;

    public enum GateType { BridgeBall, GateRightLong, GateLeftLong, GateCurved, GateRightShort }
    public GateType gateType = GateType.BridgeBall;

    private void Start()
    {
        ballsInTubeController = FindObjectOfType<BallsInTubeController>();
        referenceManager = ReferenceManager.Instance;
    }

    public void AddCloneBall(GameObject cloneBall)
    {
        cloneBallsRecieved.Add(cloneBall);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.CloneBall))
        {
            if (gateNumber == GateNumber.OneBall && gateType == GateType.GateRightShort)
            {
                if (cloneBallsRecieved.Count >= 1)
                {
                    gameObject.transform.GetChild(1).gameObject.SetActive(true);
                    gameObject.transform.GetChild(0).gameObject.SetActive(true);

                    gameObject.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    gameObject.transform.GetChild(2).gameObject.transform.GetChild(1).gameObject.SetActive(true);

                    gameObject.transform.GetChild(2).GetComponent<BoxCollider>().enabled = true;

                    gameObject.transform.GetChild(3).gameObject.SetActive(true);
                }
            }
            else if (gateNumber == GateNumber.TwoBall && gateType == GateType.GateRightLong)
            {
                if (cloneBallsRecieved.Count >= 2)
                {
                    gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    gameObject.transform.GetChild(1).gameObject.SetActive(true);

                    gameObject.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    gameObject.transform.GetChild(2).gameObject.transform.GetChild(1).gameObject.SetActive(true);

                    gameObject.transform.GetChild(2).GetComponent<BoxCollider>().enabled = true;

                    gameObject.transform.GetChild(3).gameObject.SetActive(true);
                }
            }
            else if (gateNumber == GateNumber.TwoBall &&  gateType == GateType.GateLeftLong)
            {
                if (cloneBallsRecieved.Count >= 2)
                {
                    gameObject.transform.GetChild(1).gameObject.SetActive(true);
                    gameObject.transform.GetChild(0).gameObject.SetActive(true);

                    gameObject.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    gameObject.transform.GetChild(2).gameObject.transform.GetChild(1).gameObject.SetActive(true);

                    gameObject.transform.GetChild(2).GetComponent<BoxCollider>().enabled = true;

                    gameObject.transform.GetChild(3).gameObject.SetActive(true);
                }
            }
            else if (gateNumber == GateNumber.TwoBall && gateType == GateType.BridgeBall)
            {
                if (cloneBallsRecieved.Count >= 2)
                {
                    gameObject.transform.GetChild(0).DOLocalMoveY(5f, .3f).Play();
                    isActiveBridge = true;

                    gameObject.transform.GetChild(1).gameObject.SetActive(true);
                    gameObject.transform.GetChild(2).gameObject.SetActive(true);
                    gameObject.transform.GetChild(3).gameObject.SetActive(true);
                    gameObject.transform.GetChild(4).gameObject.SetActive(true);

                }
                else
                {
                    isActiveBridge = false;
                }
            }
            else if (gateNumber == GateNumber.ThreeBall && gateType == GateType.GateCurved)
            {
                if (cloneBallsRecieved.Count >= 3)
                {
                    gameObject.transform.GetChild(1).gameObject.SetActive(true);
                    gameObject.transform.GetChild(0).gameObject.SetActive(true);

                    gameObject.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    gameObject.transform.GetChild(2).gameObject.transform.GetChild(1).gameObject.SetActive(true);
                    
                    gameObject.transform.GetChild(2).GetComponent<BoxCollider>().enabled = true;

                    gameObject.transform.GetChild(3).gameObject.SetActive(true);
                } 
            }
        }
    }
}
