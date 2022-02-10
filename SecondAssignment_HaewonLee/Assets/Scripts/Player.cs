using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public static Player instance =null;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject collidedObj; // to save collided objs
    
    public GameObject mushroomcontroller;
    public GameObject foodmanager;
    public GameObject player;

    // To move player
    private float hAxis; 
    private float vAxis;
    private float speed= 4f;

    // To Harvest 
    private bool harvest = false;
    [SerializeField] private GameObject[] mushrooms;
    [SerializeField] public int points;
    private int bestPoints =0;

    // To take Food (Health)
    [SerializeField] private GameObject[] food;
    public int health =50; // default health 
    [SerializeField] private int maxHealth = 100;

    // To get Damage
    private float currentTime;
    private float timeup = 90.0f;
    private float damageTime =10.0f;

    // UI
    // Health
    [SerializeField] private Text txtHealth;
    private string pretxtHealth = "Health: ";
    // Mushroom (points)
    [SerializeField] private Text txtMushroom;
    private string pretxtMushroom = "Mushroom: ";
    // Timer
    [SerializeField] private Text txtTime;
    private string pretxtTime = "Time: ";

    public GameObject canvasGameover; // GameOver panel
    public GameObject textBest; // Best text
    public GameObject textEat; // Eat something text

    //Audio
    [SerializeField] private AudioClip harvestSound = null;
    private AudioSource audioH;


    public bool isOver = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        audioH = GetComponent<AudioSource>(); // cache AudioSource
    }
    // Start is called before the first frame update
    void Start()
    {
        bestPoints = PlayerPrefs.GetInt("Points", 0);
        canvasGameover.SetActive(false);
        textBest.SetActive(false);
        textEat.SetActive(false);
        animator = GetComponent<Animator>();
        txtHealth.text = pretxtHealth + health.ToString("D3");
        txtMushroom.text = pretxtMushroom + points.ToString("D6");
        txtTime.text = pretxtTime + timeup;
    }


    // Update is called once per frame
    void Update()
    {
        hAxis = Input.GetAxis("Horizontal");
        vAxis = Input.GetAxis("Vertical");
        harvest = Input.GetButtonDown("Harvest"); // h button
        
        OnMove();
        OnHarvest();
        OnEat();
        OnAlert();
        OnTimer();
        currentTime += Time.deltaTime;
        if (currentTime>=damageTime)
        {
            OnDamage();
            currentTime = 0;
            if (health<=0)
            {
                Gameover();
            }
        }        
    }
    public void Gameover()
    {
        isOver = true;
        mushroomcontroller.SetActive(false);
        foodmanager.SetActive(false); // it does not stop...!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        player.SetActive(false);
        canvasGameover.SetActive(true);
        if (points>bestPoints)
        {
            PlayerPrefs.SetInt("Points", points); // save the new best points
            PlayerPrefs.Save();
        }
    }
    public void OnTimer()
    {
        timeup -= Time.deltaTime;
        txtTime.text = string.Format("Time: {0:N2} ", timeup);
        if (timeup <= 0.01f)
        {
            timeup = 0f;
            txtTime.text = string.Format("Time: {0:N2} ", timeup);
            Gameover();

        }

    }
    void OnMove()
    {
        Vector3 moveVec = new Vector3(hAxis, 0, vAxis).normalized;
        transform.position += moveVec * speed * Time.deltaTime;
        animator.SetBool("Run", moveVec != Vector3.zero);
        transform.LookAt(transform.position + moveVec); // To make the player to turn positions when it moves
    }
    public void OnHarvest()
    {
        if (harvest)
        {
            animator.SetTrigger("Harvest");
            
            if (collidedObj.tag == "Mushroom")
            {
                Item item = collidedObj.GetComponent<Item>();
                switch (item.type)
                {
                    case Item.Type.Mushroom1:
                        points += item.values;
                        Destroy(collidedObj);
                        PlayHarvestSound();
                        break;
                    case Item.Type.Mushroom2:
                        points += item.values;
                        Destroy(collidedObj);
                        PlayHarvestSound();
                        break;
                    case Item.Type.Mushroom3:
                        points += item.values;
                        Destroy(collidedObj);
                        PlayHarvestSound();
                        break;
                    default:
                        break;
                }
                if (points>bestPoints)
                {
                    textBest.SetActive(true);
                }
                txtMushroom.text = pretxtMushroom + points.ToString("D6"); // update mushroom points
            }
        }
    }
    public void OnEat()
    {
        if (harvest)
        {
            if (collidedObj.tag == "Food")
            {
                Item item = collidedObj.GetComponent<Item>(); // to get the Item Script
                switch (item.type)
                {
                    case Item.Type.Waffle:
                        health += item.values;
                        if (health > maxHealth)
                        {
                            health = maxHealth;
                        }
                        Destroy(collidedObj);
                        break;
                    case Item.Type.Milk:
                        health += item.values;
                        if (health > maxHealth)
                        {
                            health = maxHealth;;
                        }
                        Destroy(collidedObj);
                        break;
                    default:
                        break;
                }
                txtHealth.text = pretxtHealth + health.ToString("D3"); // upadate health points
            }
        }
    }
    public void OnDamage()
    {
        health -= 10;
        txtHealth.text = pretxtHealth + health.ToString("D3");
    }

    public void OnDeath()
    {
        if (health <= 0)
        {
            Gameover();
        }
    }
    public void OnAlert()
    {
        if (health <= 20)
        {
            textEat.SetActive(true);
        }
        else textEat.SetActive(false);
    }
    public void PlayHarvestSound()
    {
        audioH.clip = harvestSound;
        audioH.PlayOneShot(audioH.clip);
        //audioGun.PlayOneShot(audioGun.clip)
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Mushroom")
        {
            collidedObj = other.gameObject;
            Debug.Log(collidedObj.name);
        }
        else if(other.tag == "Food")
        {
            collidedObj = other.gameObject;
            Debug.Log(collidedObj.name);
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Mushroom" || other.tag == "Food")
        {
            collidedObj = null; // after collision empty
        }
    }

}
