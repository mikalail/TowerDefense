using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class townHealth : MonoBehaviour {
    int baseHealth = 100;
    int currentHealth=100;
    AudioSource audio;
    public List<AudioClip> clips;
    TextMesh tm;
    GameManager gm;
    public GameObject ex;
    //public int count;

	// Use this for initialization
	void Start () {
        tm = GetComponent<TextMesh>();
        tm.text = currentHealth + " ";
        audio = GetComponent<AudioSource>();
        gm = FindObjectOfType<GameManager>();
        ex.transform.gameObject.active = false;
    }
	
    void Update()
    {
        transform.forward = Camera.main.transform.forward;
    }

    public int current()
    {
        return tm.text.Length;
    }
    public void decrease (int ammount)
    {
        if (currentHealth - ammount > 0)
        {
            currentHealth = currentHealth - ammount;
            if (currentHealth<50&& currentHealth>25)
            {
                tm.color = Color.yellow;
            }
            else if (currentHealth < 25)
            {
                tm.color = Color.red;
            }
         
         tm.text = currentHealth + "";
         audio.PlayOneShot(clips[Mathf.RoundToInt(Random.Range(0, clips.Count))]);
        }

        else
        {
            tm.color = Color.red;
            tm.text ="Dead";
            ex.transform.gameObject.active = true;
            gm.EndGame();
            audio.PlayOneShot(clips[Mathf.RoundToInt(Random.Range(0, clips.Count))]);
        }

    }

   public void Reset()
    {

        currentHealth = baseHealth;
        ex.transform.gameObject.active = false;
        
    }

}
