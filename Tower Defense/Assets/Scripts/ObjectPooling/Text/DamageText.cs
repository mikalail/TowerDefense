using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageText : MonoBehaviour {

    public int poolingIndex=0;
    public string damage=" ";
    public Text txt;
    public DamageTextPooling pool;
    public Animator anim;
    AnimatorClipInfo[] animclip;

    // Use this for initialization
    void Awake () {

        anim = GetComponent<Animator>();
        txt = GetComponent<Text>();

    }
    private void OnEnable()
    {
        StartCoroutine(LifeSpan());
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void Damage(int dmg)
    {
        damage = "-" + dmg;
       
        txt.text = damage;
    }

    IEnumerator LifeSpan()
    {
        animclip = anim.GetCurrentAnimatorClipInfo(0);
        Debug.Log(animclip[0].clip.length);
        yield return new WaitForSeconds(animclip[0].clip.length);
        pool.DisableObject(poolingIndex, gameObject);

    }
}
