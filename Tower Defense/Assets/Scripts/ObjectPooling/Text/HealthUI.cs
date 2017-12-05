using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour {

    int health;
    string myText;
    Text txt;

	// Use this for initialization
	void Awake ()
    {
        txt = GetComponent<Text>();

    }
	
	// Update is called once per frame
	void Update () {


	}

    void TextColor()
    {
        if(health<=0)
        {
            txt.color = Color.red;
        }

        else if (health>0&& health<25)
        {
            txt.color = Color.green;
        }
        else if (health > 25 && health < 50)
        {
            txt.color = Color.yellow;
        }
        else if (health > 50 && health < 75)
        {
            txt.color = Color.blue;
        }
        else if (health > 75 && health <= 100)
        {
            txt.color = Color.magenta;
        }
    }


    public void SetHealth(int newHealth)
    {
        health = newHealth;
        myText = "" + health;
        txt.text=myText;
        TextColor();

    }

    public void Damage(int dmg)
    {
        health = health - dmg;
        if(health<=0)
        {
            myText = "DEAD";
            txt.text = myText;
            TextColor();
        }
        else
        {
            myText = ""+health;
            txt.text = myText;
            TextColor();
        }
    }
}
