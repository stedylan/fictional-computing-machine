using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class missile : MonoBehaviour {
    public GameObject guidedMissile;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnCollisionEnter(Collision collision)
    {
        // 销毁当前游戏物体
     
        if(collision.collider.tag == "tank1")
        {
            GameObject.Find("Tank1").GetComponent<Tank1>().life -= 20;
            Destroy(this.gameObject);
            GameObject.Find("Tank2").GetComponent<Tank2>().tools[3] = false;

        }
    }
}
