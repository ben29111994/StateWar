using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conquest : MonoBehaviour
{
    public List<GameObject> listTeam = new List<GameObject>();
    public GameObject currentTeam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (currentTeam.GetComponent<SpawnPoint>().currentSpawn < 5 && !other.CompareTag(currentTeam.tag) && !other.CompareTag("Ground"))
        {
            Debug.LogError(other.tag + " " + currentTeam.tag);
            switch (other.tag)
            {
                case "Player":
                    GameManager.Instance.Capture(1);
                    currentTeam.SetActive(false);
                    currentTeam = listTeam[0];
                    currentTeam.SetActive(true);
                    transform.GetComponentInChildren<SpriteRenderer>().color = currentTeam.GetComponent<SpawnPoint>().unitColor;
                    break;
                case "Enemy1":
                    GameManager.Instance.Capture(-1);
                    currentTeam.SetActive(false);
                    currentTeam = listTeam[1];
                    currentTeam.SetActive(true);
                    transform.GetComponentInChildren<SpriteRenderer>().color = currentTeam.GetComponent<SpawnPoint>().unitColor;
                    break;
            }
        }
    }
}
