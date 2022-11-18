using UnityEngine;

/// <summary>HPÇì™è„Ç…ï\é¶ </summary>
public class UIController_Overlay : MonoBehaviour
{

    [SerializeField]
    private Transform targetTfm;

    private RectTransform myRectTfm;
    private Vector3 offset = new Vector3(0, 0.8f, 0);

    void Start()
    {
        myRectTfm = GetComponent<RectTransform>();
    }

    void Update()
    {
        myRectTfm.position
            = RectTransformUtility.WorldToScreenPoint(Camera.main, targetTfm.position + offset);
    }
}