using UnityEngine;
using UnityEngine.EventSystems;

public class PoseModelOnClick : MonoBehaviour, IPointerClickHandler
{
    public MenuController menuController;

    void OnMouseDown()
    {
        OnModelClick();
    }

    public void OnPointerClick(PointerEventData eventData){
        Debug.Log("Pointer clicked");
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
