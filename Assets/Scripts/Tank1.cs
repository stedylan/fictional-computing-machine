using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank1 : MonoBehaviour {
    const int toolNum = 5;
    private int speed;
    public GameObject tank1;
    public Camera camera;
    public int life = 100;
    public  int attack2 = 10;
   private int isMoving = 0;
    public GameObject goBullet;
    private Vector3 bulletPos;
    private Quaternion bulletRotation;
    private int key;
    public bool[] tools;
    private int attTime;
    private Texture2D blood;
    private float smoothSpeed = 6f;
    private float distanceH = 10f;
    private float distanceV = 6f;
    private Texture2D hp;
    private Texture2D att;
    private Texture2D missile;
    private Texture2D attState;
    private GameObject guided1;
    private GameObject guidedMi1;
    
    
	// Use this for initialization
	void Start () {
        tools = new bool[toolNum]{false,false,false,false,false};
        goBullet = Resources.Load("missile") as GameObject;
        speed = 30;
        blood = Resources.Load("blood") as Texture2D;
        hp = Resources.Load("Hp++") as Texture2D;
        att = Resources.Load("att") as Texture2D;
        missile = Resources.Load("dd") as Texture2D;
        attState = Resources.Load("Att++") as Texture2D;
        guided1 = Resources.Load("guidedMissile 1") as GameObject;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("tool1guided"))
        {
            if (tools[4] == true)
            {
                tools[3] = true;
                guidedMi1 = Instantiate(guided1);
                guidedMi1.transform.position = tank1.transform.position;
                tools[4] = false;
            }
        }
        if (tools[3] == true)
        {
            guidedMi1.transform.LookAt(GameObject.Find("Tank2").transform);
            guidedMi1.transform.position = guidedMi1.transform.position + 100 * guidedMi1.transform.forward * Time.deltaTime;
        }
            if (Input.GetButtonDown("Vertical1") || Input.GetButtonDown("Horizontal1"))
                isMoving = 1;
            if (Input.GetButtonDown("Fire1"))
            {
                bulletPos = GameObject.Find("fire").transform.position;
                bulletRotation = new Quaternion(0, 0, 0, 0);
              Instantiate(goBullet, bulletPos, bulletRotation);

            }
            if (isMoving == 1)
            {
                Vector3 direction = -Input.GetAxis("Vertical1") * transform.forward;
                tank1.transform.position = transform.position + speed * direction * Time.deltaTime;
                if (tank1.transform.localEulerAngles.z >= 89 && tank1.transform.localEulerAngles.z <= 91 || tank1.transform.localEulerAngles.z <= 271 && tank1.transform.localEulerAngles.z >= 269)
                {
                    if (Input.GetAxis("Horizontal1") > 0)
                        tank1.transform.localEulerAngles += new Vector3(0, 0, 90);

                    if (Input.GetAxis("Horizontal1") < 0)
                        tank1.transform.localEulerAngles -= new Vector3(0, 0, 90);
                }
                else if (tank1.transform.localEulerAngles.z >= 179 && tank1.transform.localEulerAngles.z <= 181 || tank1.transform.localEulerAngles.z <= -179 && tank1.transform.localEulerAngles.z >= -181)
                {
                    if (Input.GetAxis("Horizontal1") > 0)
                        tank1.transform.localEulerAngles += new Vector3(0, 0, 180);

                    if (Input.GetAxis("Horizontal1") < 0)
                        tank1.transform.localEulerAngles -= new Vector3(0, 0, 180);
                }
                else
                { 
                if (Input.GetAxis("Horizontal1") > 0)
                    tank1.transform.Rotate(0, 2, 0);
                if (Input.GetAxis("Horizontal1") < 0)
                    tank1.transform.Rotate(0, -2, 0);
                }
            }
            if (life <= 0)
            {
                Destroy(tank1);
            }

	}

  
    void OnCollisionEnter(Collision collision)
    {
        // 销毁当前游戏物体

        if (collision.collider.tag == "tank2bullet")
        {
            life -= attack2;
        }
        if (collision.collider.tag == "box")
        {
            key = Random.Range(1, 4);
            if (key == 1)
                tools[0] = true;
            else if (key == 2)
                tools[1] = true;
            else if (key == 3)
                tools[4] = true;

        }
    }

    // 碰撞结束
    void OnCollisionExit(Collision collision)
    {
       
    }

    // 碰撞持续中
    void OnCollisionStay(Collision collision)
    {
      

    }
    void Box()
    {
        if (tools[2] == true)
            GUI.DrawTexture(new Rect(70, 40, 20, 20), attState);
            if (tools[0] == true)
                GUI.DrawTexture(new Rect(10, 80, 40, 40), hp);
            if (tools[1] == true)
                GUI.DrawTexture(new Rect(10, 120, 40, 40), att);
            if (tools[4] == true)
                GUI.DrawTexture(new Rect(10, 160, 40, 40), missile);

        
    }
    void LateUpdate()
    {
        changePosition();
    }
    private void changePosition()
    {
        Vector3 nextPos = this.gameObject.transform.forward  * distanceH + this.gameObject.transform.up * distanceV + this.gameObject.transform.position;
       //Vector3 nextPos = new Vector3(10,6,0) + this.gameObject.transform.position;
        camera.transform.position = Vector3.Lerp(camera.transform.position, nextPos, smoothSpeed * Time.deltaTime); //平滑插值
        camera.transform.LookAt(GameObject.Find("Cube").transform);
        //camera.transform.localEulerAngles = new Vector3(11, camera.transform.localEulerAngles.y, camera.transform.localEulerAngles.z);
    }
    void OnGUI()
    {
        
        float lifeOn = (float)life / 100;
        GUI.DrawTexture(new Rect(400, 300, 450 * lifeOn, 20), blood);
        Box();
        if (Input.GetButtonDown("tool1hp"))
        {
            if (tools[0] == true)
            {
                if (life <= 70)
                    life += 30;
                else
                    life = 100;
                tools[0] = false;
            }
        }
        if (Input.GetButtonDown("tool1att"))
        {
            if (tools[1] == true)
            {
                attTime = (int)Time.time;
                tools[2] = true;
                GameObject.Find("Tank2").GetComponent<Tank2>().attack1 = 30;
                tools[1] = false;
            }
        }
        if ((int)Time.time - 5 == attTime)
        {
            tools[2] = false;
            GameObject.Find("Tank2").GetComponent<Tank2>().attack1 = 10;
        }
     
       
    }

}
