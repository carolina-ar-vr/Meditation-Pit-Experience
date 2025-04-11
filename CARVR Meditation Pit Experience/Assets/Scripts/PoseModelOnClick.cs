using UnityEngine;

public class PoseModelOnClick : MonoBehaviour
{
    public MenuController menuController;

    void OnMouseDown()
    {
        OnModelClick();
    }

    void OnModelClick()
    {
        if (menuController != null)
        {
            menuController.OnPoseModelClick();
        }
    }
}
