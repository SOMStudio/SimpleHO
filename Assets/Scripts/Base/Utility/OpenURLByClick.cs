using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenURLByClick : MonoBehaviour
{
    [Header("Main")]
    [SerializeField] private string stringURL;

    public void OnClickURL()
    {
        Application.OpenURL(stringURL);
    }
}
