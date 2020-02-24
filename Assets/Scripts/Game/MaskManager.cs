using UnityEngine;

public class MaskManager : MonoBehaviour
{
    [Header("Main")]
    [SerializeField] private GameObject maskGO;

    public void ShowMask()
    {
        maskGO.SetActive(true);
    }

    public void HideMask()
    {
        maskGO.SetActive(false);
    }
}
