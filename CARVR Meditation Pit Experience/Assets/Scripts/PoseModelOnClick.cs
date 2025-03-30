using UnityEngine;

public class PoseModelOnClick : MonoBehaviour
{
    public MenuController menuController;

    void OnModelClick()
    {
        if (menuController != null)
        {
            menuController.OnPoseModelClick();
        }
    }
}
