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
        if (line.enabled == true)
        {
            SetFormation();
        }
    }

    private void SetFormation()
    {
        _points = Formation.EvaluatePoints().ToList();
        for (var i = 0; i < Gestures.listSelected.Count; i++)
        {
            if (i < _points.Count)
            {
                var moveIn = Gestures.listSelected[i].GetComponent<AgentDestinationAuthoring>();
                moveIn.enabled = false;
                moveIn.listTarget.Clear();
                moveIn.listTarget.Add(Gestures.formationCenter + _points[i]);
                moveIn.enabled = true;
                //Gestures.listSelected[i].transform.position = Vector3.MoveTowards(Gestures.listSelected[i].transform.position, Gestures.formationCenter + _points[i], 15 * Time.deltaTime);
            }
        }
    }
}
