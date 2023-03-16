using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ProjectDawn.Navigation.Sample.Zerg;
using ProjectDawn.Navigation.Sample.Scenarios;

public class UnitFormation : MonoBehaviour
{
    public LineRenderer line;
    private List<Vector3> _points = new List<Vector3>();
    public FormationBase _formation;
    public Transform formationPos;

    public FormationBase Formation
    {
        get
        {
            if (_formation == null) _formation = GetComponent<FormationBase>();
            return _formation;
        }
        set => _formation = value;
    }

    private void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //if (line.enabled == true)
        //{
        //    SetFormation();
        //}

        if (Input.GetMouseButtonUp(0) && Gestures.listSelected.Count > 0)
        {
            SetFormation();
        }
    }

    private void SetFormation()
    {
        Debug.LogError("Run");
        var direction = line.GetPosition(line.positionCount - 1) - line.GetPosition(0);
        formationPos.transform.position = new Vector3(Gestures.formationCenter.x, 0, Gestures.formationCenter.z);
        formationPos.transform.rotation = Quaternion.LookRotation(direction);
        formationPos.transform.eulerAngles = new Vector3(0, formationPos.transform.eulerAngles.y, 0);
        //_points = Formation.EvaluatePoints().ToList();
        var index = 0;
        for (var i = 0; i < Gestures.listSelected.Count; i++)
        {
            //if (i < _points.Count)
            //{
                var moveIn = Gestures.listSelected[i].GetComponent<AgentDestinationAuthoring>();
                moveIn.enabled = false;
                moveIn.listTarget.Clear();
                //moveIn.listTarget.Add(Gestures.formationCenter + _points[i]);
                moveIn.listTarget.Add(formationPos.GetChild(index).position);
                index++;
                if(index >= formationPos.childCount) { index = 0; }
                moveIn.enabled = true;
                //Gestures.listSelected[i].transform.position = Vector3.MoveTowards(Gestures.listSelected[i].transform.position, Gestures.formationCenter + _points[i], 15 * Time.deltaTime);
            //}
        }
    }
}
