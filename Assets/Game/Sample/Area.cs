using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Area : MonoBehaviour
{
    public int Unit;
    float Timer;

    public TextMeshPro MyNumber;

    public int Manager;
    public int Attack;
    public GameObject Target;
    public GameObject UnitPrefab;
    public bool CanMake;

    public Color32 CL1;
    public Color32 CL2;
    public SpriteRenderer Inside;

    // Start is called before the first frame update
    void Start()
    {
        MyNumber.text = "0";
    }

    float SecondTimer;
    // Update is called once per frame
    void Update()
    {
        if (CanMake == false)
        {
            Timer += 2 * Time.deltaTime;
        }

        if (CanMake == true)
        {
            SecondTimer += 1 * Time.deltaTime;

            if (SecondTimer >= 3)
            {
                CanMake = false;
                SecondTimer = 0;
            }
        }


        if (Timer >= 1)
        {
            Unit += 1;
            MyNumber.text = Unit.ToString();

            Timer = 0;
        }
        if (Input.GetMouseButtonDown(0))
        {
            OnMouseDown();
        }

        if (Input.GetMouseButtonUp(0))
        {
            OnMouseUp();
        }

        if (Manager == 1)
        {
            TouchPosition.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x , Input.mousePosition.y , +10));

            GetComponent<LineRenderer>().SetPosition(0,this.transform.position);
            GetComponent<LineRenderer>().SetPosition(1, TouchPosition.transform.position);
        }


        if (Attack == 2)
        {
            CanMake = true;
            StartCoroutine(MakeUnit());
            Attack = 3;
        }
    }

    IEnumerator MakeUnit()
    {
        while(Unit > 0)
        {
            yield return new WaitForSeconds(0.3f);

            GameObject U = Instantiate(UnitPrefab , this.transform.position , Quaternion.identity);
            U.GetComponent<Unit>().Target = Target;
            U.transform.tag = this.gameObject.transform.tag;
            U.transform.name = "Unit";
            U.GetComponent<Unit>().CL = CL1;
            U.GetComponent<Unit>().InsideCL = CL2;
            U.GetComponent<Unit>().Sender = this.transform.name;
            Unit -= 1;
            MyNumber.text = Unit.ToString();

            SecondTimer = 0;

            if(Unit == 0)
            {
                Attack = 0;
                CanMake = false;
            }
        }
    }


    public GameObject OBG;
    public GameObject TouchPosition;
    void OnMouseDown() 
    {
        TouchPosition = Instantiate(OBG,Vector3.zero,Quaternion.identity);
        TouchPosition.GetComponent<TouchPosition>().MyAreaName = this.transform.name;

        TouchPosition.GetComponent<TouchPosition>().MyArea = this.gameObject;
        TouchPosition.GetComponent<TouchPosition>().MyTag = this.gameObject.transform.tag;

        Manager = 1;
    }

    void OnMouseUp() 
    {
        Manager = 0;
        Destroy(TouchPosition);
        GetComponent<LineRenderer>().SetPosition(0,Vector3.zero);
        GetComponent<LineRenderer>().SetPosition(1,Vector3.zero);

        if(Attack == 1)
        {
            Attack = 2;
        }
    }




    void OnTriggerEnter(Collider other) 
    {
        if(other.transform.tag != this.transform.tag && other.transform.tag != "Touch")
        {
            Destroy(other.gameObject);

            if(Unit > 0)
            {
                Unit -= 1;
                MyNumber.text = Unit.ToString();

                CanMake = true;
            }
            else
            {
                transform.tag = other.transform.tag;
                GetComponent<SpriteRenderer>().color = other.GetComponent<Unit>().CL;
                Inside.color = other.GetComponent<Unit>().InsideCL;
            }
            
        }
        if(other.transform.tag == this.transform.tag && other.transform.tag != "Touch")
        {
            if(other.GetComponent<Unit>().Sender != this.transform.name)
            {
                Destroy(other.gameObject);
                Unit += 1;
                MyNumber.text = Unit.ToString();

                CanMake = true;
            }
            
        }
    }

}
