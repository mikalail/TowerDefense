using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGuiManager : MonoBehaviour
{
    public Transform parent;
    public DamageTextPooling textPooling;
    public GameObject Health;
    Vector2 screenPosition;

    // Use this for initialization
    void Start()
    {
        textPooling = GetComponent<DamageTextPooling>();
        textPooling.parent = transform;
    }

    // Update is called once per frame
    void Update()
    {   screenPosition = Camera.main.WorldToScreenPoint(parent.transform.position);
        transform.position =screenPosition;
        Health.transform.position = screenPosition+new Vector2(0,20);
        
    }

    public void SetHealth(int newHealth)
    {
        Health.GetComponent<HealthUI>().SetHealth(newHealth);
    }

    public void Damage(int dmg)
    {
        textPooling.InstantiateObject(screenPosition, dmg);
        Health.GetComponent<HealthUI>().Damage(dmg);
    }

}