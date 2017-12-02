using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenShop : MonoBehaviour {

    public Canvas Canvas;

    void OnMouseDown()
    {
        if (Canvas == false)
        {
            Canvas.gameObject.SetActive(true);
            Debug.Log ("Canvas hidden");
        }
        else
        {
            Canvas.gameObject.SetActive(false);
            Debug.Log ("Canvas hidden");
        }
    }
}
