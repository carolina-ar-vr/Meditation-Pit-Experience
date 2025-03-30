using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoseSwitcher : MonoBehaviour
{
    public GameObject posesModel;
    public GameObject defaultPose;

    private GameObject currentPose;

    void Start()
    {
        SetDefaultPose();
    }

    public void ActivatePose(GameObject poseToActivate)
    {
        // switches pose
        if (currentPose != null)
        {
            currentPose.SetActive(false);
            Debug.Log("Deactivated pose: " + currentPose.name);
        }

        poseToActivate.SetActive(true);
        currentPose = poseToActivate;
        Debug.Log("Activated pose: " + poseToActivate.name);
    }

    private void SetDefaultPose()
    {
        // hides other pose models
        foreach (Transform pose in posesModel.transform)
        {
            pose.gameObject.SetActive(false);
        }

        if (defaultPose != null)
        {
            defaultPose.SetActive(true);
            currentPose = defaultPose;
        }
    }

    public void TestButtonPress()
    {
        Debug.Log("Button Pressed!");
    }
}
