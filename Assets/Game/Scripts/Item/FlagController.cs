using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagController : MonoBehaviour
{
    protected static FlagController instance;
    public static FlagController Instance => instance;

    private const int MaximumFlagAmountOnScene = 6;
    private int currentFlagAmount;
    [HideInInspector] public List<Item> listItem = new List<Item>();

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public void StartGenerate()
    {
        StartCoroutine(C_StartGenerate());
    }

    private IEnumerator C_StartGenerate()
    {
        while (true)
        {
            while (IsMaximumFlag()) yield return null;

            Item newFlag = GetItemFromPool();
            newFlag.Active(RandomPositionOnCube());
            listItem.Add(newFlag);

            yield return new WaitForSeconds(GetTimeDelay());
        }
    }

    public void StopGenerate()
    {
        StopAllCoroutines();
    }

    public void HideAll()
    {

    }

    public void HideItem(Item item)
    {
        listItem.Remove(item);
    }

    private Item GetItemFromPool()
    {
        GameObject _go = PoolManager.Instance.GetObject(PoolManager.NameObject.Item) as GameObject;
        Item item = _go.GetComponent<Item>();
        return item;
    }

    private Vector3 RandomPositionOnCube()
    {
        float scaleX = GenerateMap.Instance.scaleMap.x - 1.0f;
        float scaleZ = GenerateMap.Instance.scaleMap.z - 1.0f;
        Vector3 _result = Vector3.zero;

        while (true)
        {
            _result.x = Random.Range(-scaleX, scaleX);
            _result.z = Random.Range(-scaleZ, scaleZ);

            if (CanSpawn(_result)) break;
        }

        return _result;
    }

    private bool IsMaximumFlag()
    {
        return listItem.Count >= MaximumFlagAmountOnScene ? true : false;
    }

    private bool CanSpawn(Vector3 _pos)
    {
        Ray ray = new Ray(_pos + Vector3.up * 10.0f, Vector3.down);
        RaycastHit[] hits = Physics.SphereCastAll(ray, 2.0f, 20.0f);
        foreach(RaycastHit hit in hits)
        {
            if (hit.collider.CompareTag("Cube"))
            {
                Cube _cube = hit.collider.gameObject.GetComponent<Cube>();
                if (_cube.cubeType == Cube.CubeType.Green) return false;
            }
            else if (hit.collider.CompareTag("Flag"))
            {
                return false;
            }
        }

        return true;
    }

    private float GetTimeDelay()
    {
        return 0.4f;
    }
}
