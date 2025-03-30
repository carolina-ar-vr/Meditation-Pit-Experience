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
        }

        poseToActivate.SetActive(true);
        currentPose = poseToActivate;
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
}
