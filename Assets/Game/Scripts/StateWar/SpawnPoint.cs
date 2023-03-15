using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.UI;

public class SpawnPoint : MonoBehaviour
{
    public static SpawnPoint instance;
    public string typePrefab;
    public GameObject prefab;
    public Material InstancedMaterial;
    public int maxSpawn;
    public string unitTag;
    public Color unitColor;
    public float spawnInterval;
    public TextMesh spawnCountUI;
    WaitForSeconds intervalCache;
    public int currentSpawn = 0;
    private readonly List<GameObject> _spawnedUnits = new List<GameObject>();
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

    void Start()
    {
        currentSpawn = 0;
        instance = this;
        intervalCache = new WaitForSeconds(spawnInterval);
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
#if UNITY_5_6_OR_NEWER
        InstancedMaterial.enableInstancing = true;
#endif
        GameObject newUnit = null;
        newUnit = Instantiate(prefab);
        //switch (typePrefab)
        //{
        //    case "Player":
        //        newInstance = PoolManager.Instance.GetObject(PoolManager.NameObject.Player) as GameObject;
        //        break;
        //    case "Enemy":
        //        newInstance = PoolManager.Instance.GetObject(PoolManager.NameObject.Enemy) as GameObject;
        //        break;
        //    default:
        //        break;
        //}
        if (newUnit != null)
        {
            MaterialPropertyBlock matpropertyBlock = new MaterialPropertyBlock();
            Color newColor = unitColor;
            matpropertyBlock.SetColor("_Color", newColor);
            newUnit.GetComponent<MeshRenderer>().SetPropertyBlock(matpropertyBlock);
            newUnit.tag = unitTag;
            newUnit.transform.position = transform.parent.position;
            newUnit.GetComponent<UnitCommand>().spawnPoint = this;
            newUnit.SetActive(true);
            newUnit.transform.parent = transform;
            currentSpawn++;
            spawnCountUI.text = currentSpawn.ToString();
            _spawnedUnits.Add(newUnit);
            yield return intervalCache;
            while (currentSpawn >= maxSpawn)
            {
                yield return null;
            }
            //SetFormation();
            StartCoroutine(Spawn());
        }
    }

    //private void FixedUpdate()
    //{
    //    SetFormation();
    //}

    private void SetFormation()
    {
        _points = Formation.EvaluatePoints().ToList();
        for (var i = 0; i < _spawnedUnits.Count; i++)
        {
            //_spawnedUnits[i].transform.position = Vector3.MoveTowards(_spawnedUnits[i].transform.position, transform.position + _points[i], 15 * Time.deltaTime);
            _spawnedUnits[i].transform.position = transform.position + _points[i];
        }
    }

    public void OnUnitDead()
    {
        currentSpawn--;
        if(spawnCountUI != null)
            spawnCountUI.text = currentSpawn.ToString();
    }
}
