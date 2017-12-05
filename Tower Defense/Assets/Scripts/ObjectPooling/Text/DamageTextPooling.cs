using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTextPooling : MonoBehaviour {
    public GameObject textRefrence;
    public List<GameObject> textPool;
    public List<bool> textinUse;
    public Transform parent;
    bool textPoolPopulated;

    // Use this for initialization
    void Start()
    {
        textinUse = new List<bool>();
        textPool = new List<GameObject>();
        textPoolPopulated = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject InstantiateObject(Vector3 instatiatePoint,int value)
    {
        int index;
        GameObject placeHolder;
        if (textPoolPopulated)
        {
            for (int i = 0; i < textPool.Count && textPool.Count != 0; i++)
            {

                if (!textinUse[i])
                {

                    textPool[i].SetActive(true);
                    textPool[i].transform.position = instatiatePoint;
                    placeHolder = textPool[i];
                    textinUse[i] = true;
                    placeHolder.GetComponent<DamageText>().Damage(value);
                    return placeHolder;
                }
            }
        }
        placeHolder = AddObjectToPool(instatiatePoint,value);
        textPoolPopulated = true;


        return placeHolder;
    }

    GameObject AddObjectToPool(Vector3 instatiatePoint,int value)
    {
        int index = 0;
        GameObject newText;

        if (textPoolPopulated)
        {
            index = textPool.Count;
        }
        else
        {
            index = 0;
        }


        newText = GameObject.Instantiate(textRefrence, instatiatePoint, Quaternion.identity);
        newText.transform.SetParent(parent);
        newText.GetComponent<DamageText>().poolingIndex = index;
        newText.GetComponent<Transform>().name = "DamageText" + index;
        newText.GetComponent<Transform>().SetParent(parent);
        newText.GetComponent<DamageText>().pool=transform.GetComponent<DamageTextPooling>();
        newText.GetComponent<DamageText>().Damage(value);
        textPool.Add(newText);
        textinUse.Add(true);

        return newText;
    }

    public void DisableObject(int index, GameObject self)
    {
        textPool[index].SetActive(false);
        textinUse[index] = false;
    }
}
