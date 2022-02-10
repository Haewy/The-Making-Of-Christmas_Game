using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    [SerializeField] private GameObject[] food;
    [SerializeField] private Transform spot;
    private bool[] isGenerated;

    private float currentTime;
    private float timeOver = 5.0f;
    public int countFood = 0;
    private int foodIndex;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (countFood < 1) // generate one at a time
        {
            Invoke("FoodGenerator", 10.0f);
            countFood++;
        }
    }
    public void FoodGenerator()
    {
        currentTime += Time.deltaTime;
        foodIndex = Random.Range(0, food.Length);
        GameObject clone = Instantiate(food[foodIndex], spot.position, spot.rotation);
        Destroy(clone, timeOver);
        countFood = 0;
    }
}
