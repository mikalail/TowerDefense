using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public List<GameObject> Nodes;
    int children;
    // Use this for initialization


    void Awake()
    {

        children = this.transform.childCount;
        for (int i = 0; i < children; i++)
        {

            Nodes.Add(transform.GetChild(i).gameObject);

        }

    }


}
