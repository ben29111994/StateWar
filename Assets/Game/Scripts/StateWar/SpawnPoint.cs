using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using UnityEngine.UI;

namespace GoPencils.Project.StateWar
{
    public class SpawnPoint : MonoBehaviour
    {
        public string typePrefab;
        public Material InstancedMaterial;
        public int maxSpawn;
        public string unitTag;
        public Color unitColor;
        public float spawnInterval;
        public TextMesh spawnCountUI;
        WaitForSeconds intervalCache;
        public static int currentSpawn = 0;

        void Start()
        {
            intervalCache = new WaitForSeconds(spawnInterval);
            StartCoroutine(Spawn());
        }

        IEnumerator Spawn()
        {
#if UNITY_5_6_OR_NEWER
            InstancedMaterial.enableInstancing = true;
#endif
            GameObject newInstance = null;
            switch (typePrefab)
            {
                case "Player":
                    newInstance = PoolManager.Instance.GetObject(PoolManager.NameObject.Player) as GameObject;
                    break;
                case "Enemy":
                    newInstance = PoolManager.Instance.GetObject(PoolManager.NameObject.Enemy) as GameObject;
                    break;
                default:
                    break;
            }
            if (newInstance != null)
            {
                MaterialPropertyBlock matpropertyBlock = new MaterialPropertyBlock();
                Color newColor = unitColor;
                matpropertyBlock.SetColor("_Color", newColor);
                newInstance.GetComponent<MeshRenderer>().SetPropertyBlock(matpropertyBlock);
                newInstance.tag = unitTag;
                newInstance.transform.position = transform.parent.position;
                newInstance.SetActive(true);
                currentSpawn++;
                spawnCountUI.text = currentSpawn.ToString();
                yield return intervalCache;
                while (currentSpawn >= maxSpawn)
                {
                    yield return null;
                }
                StartCoroutine(Spawn());
            }
        }
    }
}
