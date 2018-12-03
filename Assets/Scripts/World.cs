using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour {
    public GameObject newPlane;
    public GameObject plane;
    private Vector3 pos;
    private Quaternion rot;
    private int freshPlane;
    private int statePlane;
    bool createStation = true;
    public bool log = false;
    private Texture2D bg;
    private Texture2D start;
    private Texture2D over;
	// Use this for initialization
	void Start () {
        bg = Resources.Load("img1") as Texture2D;
        start = Resources.Load("timg") as Texture2D;
        over = Resources.Load("gameover") as Texture2D;
	}
	
	// Update is called once per frame
	void Update () {
        freshPlane = (int)Time.time + 1;
        
        if (GameObject.Find("Small Passenger Plane(Clone)") != null)
        {
            createStation = false;
        }
        else
            createStation = true;
        if (freshPlane % 5 == 0)
        {
            if (createStation)
            {
                newPlane = Resources.Load("Small Passenger Plane") as GameObject;
                pos = new Vector3(70.15f, 90.95f, 600.41f);
                rot = new Quaternion(0, 0, 0, 0);
                plane = Instantiate(newPlane, pos, rot);
                plane.transform.localEulerAngles += new Vector3(0, 135, 0);//生成飞机
            }
            
        }
        
	}
    void OnGUI()
    {
        if (log == false)
        {
            GUIStyle font = new GUIStyle();
            font.fontSize = 100;   
            GUI.DrawTexture(new Rect(0,0,1280,720),bg);
            if(GUI.Button(new Rect(500, 400, 200, 100),start))
            {
                log = true;
            }
        }
        if(GameObject.Find("Tank1")==null)
        {
            GUI.DrawTexture(new Rect(0, 0, 1280, 360), over);
        }
        if (GameObject.Find("Tank2") == null)
        {
            GUI.DrawTexture(new Rect(0,345, 1280, 360), over);
        }
    }
}
