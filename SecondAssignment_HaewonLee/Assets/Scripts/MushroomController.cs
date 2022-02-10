using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomController : MonoBehaviour
{
    [SerializeField] private GameObject[] mushrooms;
    [SerializeField] private Transform[] spots;
    [SerializeField] private Material mushroomDead;
    [SerializeField] private Material mushroomHeadDead;
    [SerializeField] private AudioSource audioMushroom;

    private GameObject damageClone;
    private float currentTime;
    private float createTime=3.0f;
    private bool[] isGenerated; // avoid duplicating mushrooms in a same spot // This code from https://www.youtube.com/watch?v=0RNOrEh4T4E&t=599s
    private int spotIndex;

    // Start is called before the first frame update
    void Start()
    {
        isGenerated = new bool[spots.Length];
        for (int i = 0; i < isGenerated.Length; i++) // avoid duplicating mushrooms in a same spot
        {
            isGenerated[i] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;       
        if (currentTime >= createTime)
        {
           spotIndex = Random.Range(0, spots.Length);
            if (!isGenerated[spotIndex]) // avoid duplicating mushrooms in a same spot
            {
                MushroomGenerator(spotIndex);               
                currentTime = 0;                              
            }
        }
    }
    public void MushroomGenerator(int spotIndex)
    {
        int mushroomIndex = Random.Range(0, mushrooms.Length);
        damageClone = Instantiate(mushrooms[mushroomIndex], spots[spotIndex].position, spots[spotIndex].rotation);
        isGenerated[spotIndex] = true; // avoid duplicating mushrooms in a same spot

    }
    public void MushroomDamage(GameObject obj) // why...it changes a part...not entire mushrom 
    {
        obj.gameObject.GetComponent<MeshRenderer>().material = mushroomHeadDead;
    }
}
