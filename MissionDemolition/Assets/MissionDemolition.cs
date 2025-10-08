using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum GameMode
{
    idle,
    playing,
    levelEnd
}

public class MissionDemolition : MonoBehaviour
{
    static private MissionDemolition S; // singleton

    [Header("Inscribed")]
    public TextMeshProUGUI uitLevel;
    public TextMeshProUGUI uitShots;
    public Vector3 castlePos;
    public GameObject[] castles;

    [Header("Dynamic")]
    public int level;
    public int levelMax;
    public int shotsTaken;
    public GameMode mode = GameMode.idle;
    public GameObject castle;
    public string showing = "Show Slingshot";


    // Start is called before the first frame update
    void Start()
    {
        S = this; // Set the singleton
        level = 0;
        shotsTaken = 0;
        levelMax = castles.Length;
        StartLevel();
    }

    void StartLevel()
    {
        // If there is a castle, get rid of it
        if (castle != null)
        {
            Destroy(castle);
        }
        // Destroy old projectiles
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Projectile");
        foreach (GameObject pTemp in gos)
        {
            Destroy(pTemp);
        }

        // Instantiate the new castle
        castle = Instantiate(castles[level]);
        castle.transform.position = castlePos;
        shotsTaken = 0;
        Goal.goalMet = false;
        Debug.Log(Goal.goalMet);
        UpdateGUI();
        mode = GameMode.playing;
        Debug.Log("In StartLevel()");
    }

    void UpdateGUI()
    {
        uitLevel.text = "Level: " + (level + 1) + " of " + levelMax;
        uitShots.text = "Shots: " + shotsTaken;
      
       
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGUI();
        // check for level end
        if (mode == GameMode.playing && Goal.goalMet)
        {
            Debug.Log(mode);
            Debug.Log(Goal.goalMet);

            Debug.Log("In Update()");
            mode = GameMode.levelEnd;
            Debug.Log(mode);
            Invoke("NextLevel", 2f);
        
        }
    }

    void NextLevel()
    {
        level++;
        if (level == levelMax)
        {
            level = 0;
            shotsTaken = 0;
        }

        StartLevel();
    }
}
