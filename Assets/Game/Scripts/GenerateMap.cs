using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GenerateMap : MonoBehaviour
{
    protected static GenerateMap instance;
    public static GenerateMap Instance => instance;

    public Vector3 scaleMap;

    [Header("Input")]
    public int number;
    private Vector2 sizeMap = new Vector2(30,36);

    [Header("References")]
    public Cube cubePrefab;
    public List<Cube> listCube = new List<Cube>();
    public List<Cube> listSafeCube = new List<Cube>();

    public class Data
    {
        public List<CubeData> listCubeData = new List<CubeData>();
    }

    [System.Serializable]
    public class CubeData
    {
        public Vector3 pos;
        public Cube.CubeType cubeType;
    }

    public List<Cube> ListRedCube()
    {
        List<Cube> _result = new List<Cube>();
        for(int i = 0; i < listCube.Count; i++)
        {
            if (listCube[i].isRed) _result.Add(listCube[i]);
        }

        return _result;
    }

    private void Awake()
    {
        instance = this;
    }

    public void LoadDataFromResource()
    {
        int r = DataManager.Instance.LevelGame;
        if (r >= 5) r = Random.Range(0, 5);
        transform.position = Vector3.zero;

        listCube.Clear();
   
        string _path = Resources.Load<TextAsset>("Data/Data_" + r).text;
        Data data = JsonUtility.FromJson<Data>(_path);

        for (int i = 0; i < data.listCubeData.Count; i++)
        {
            SpawnCubeWithPrefab(data.listCubeData[i].pos, data.listCubeData[i].cubeType);
        }

        transform.position = new Vector3(-(sizeMap.x - 1) * 0.5f, 0.0f, -(sizeMap.y - 1) * 0.5f);
        scaleMap = new Vector3(Mathf.Abs(transform.position.x), 0.0f, Mathf.Abs(transform.position.z));

        HandleSafeCube();
    }

    private void HandleSafeCube()
    {
        listSafeCube.Clear();
        for(int i = 0;i < listCube.Count; i++)
        {
            if (listCube[i].cubeType == Cube.CubeType.Green) listSafeCube.Add(listCube[i]);
        }
    }

    public void SpawnCubeWithPrefab(Vector3 _pos, Cube.CubeType _cubeType = Cube.CubeType.Black)
    {
        GameObject _go = PoolManager.Instance.GetObject(PoolManager.NameObject.Cube) as GameObject;
        _go.transform.SetParent(this.transform);
        Cube _cube = _go.GetComponent<Cube>();
        _cube.ActiveCube(_pos, _cubeType);
        listCube.Add(_cube);
    }

#if UNITY_EDITOR
    public void Refresh()
    {
        while (listCube.Count > 0)
        {
            if (listCube[0] == null)
            {
                listCube.Clear();
                break;
            }
            DestroyImmediate(listCube[0].gameObject);
            listCube.RemoveAt(0);
        }
    }

    [NaughtyAttributes.Button]
    public void Generate()
    {
        Refresh();
        for (int h = 0;h < sizeMap.y; h++)
        {
            for (int w = 0; w < sizeMap.x; w++)
            {
                Vector3 pos = new Vector3(w, 0.0f, h);
                SpawnCube(pos);
            }
        }    
    }

    public void SpawnCube(Vector3 _pos,Cube.CubeType _cubeType = Cube.CubeType.Black)
    {
        Cube _cube = UnityEditor.PrefabUtility.InstantiatePrefab(cubePrefab, transform) as Cube;
        _cube.ActiveCube(_pos, _cubeType);
        listCube.Add(_cube);
    }

    [NaughtyAttributes.Button]
    public void SaveData()
    {
        Data newData = new Data();

        for(int i = 0; i < listCube.Count; i++)
        {
            CubeData cubeData = new CubeData();
            cubeData.pos = listCube[i].transform.position;
            cubeData.cubeType = listCube[i].cubeType;
            newData.listCubeData.Add(cubeData);
        }

        string jsonData = JsonUtility.ToJson(newData, true);
        string jsonSavePath = Application.dataPath + "/Resources/Data/Data_" + number + ".json";
        Debug.Log(jsonSavePath);
        try
        {
            File.WriteAllText(jsonSavePath, jsonData);
        }
        catch
        {
            Debug.Log("Save data error .");
        }

        Refresh();
    }

    [NaughtyAttributes.Button]
    public void LoadData()
    {
        Refresh();
        string _path = Resources.Load<TextAsset>("Data/Data_" + number).text;
        Data data = JsonUtility.FromJson<Data>(_path);

        for (int i = 0; i < data.listCubeData.Count; i++)
        {
            SpawnCube(data.listCubeData[i].pos, data.listCubeData[i].cubeType);
        }
    }
#endif
}
