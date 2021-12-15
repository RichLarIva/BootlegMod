using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnProps : MonoBehaviour
{
    [Header("Props")]
    public GameObject[] props;

    [Header("Player Stuff")]
    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnProp(int i)
    {
        //Working testing code, basically it will instantiate infront of the player allowing props to be spawned,
        //once I done enough I have to come up with a way to spawn props by their number in a list/array allowing me to optimize this incase I wanna add a new prop to the game.
        Instantiate(props[i], player.transform.position + (transform.forward * 8), player.transform.rotation);
        Debug.Log("Spawned prop");
    }

    /*
    public void SpawnCar()
    {
        Instantiate(carProp, player.transform.position + (transform.forward * 8), player.transform.rotation);
    }
    */
}
