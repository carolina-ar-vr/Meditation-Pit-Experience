using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoseSwitcher : MonoBehaviour
{
    public GameObject[] poses;

    public void ActivatePose(GameObject poseToActivate)
    {
        foreach (GameObject pose in poses)
        {
            pose.SetActive(pose == poseToActivate);
        }
    }
}
