using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Prototype : MonoBehaviour
{
    static private Prototype S;
    [Header("Set in Inspector")]
    public Text uitLevel;
    public Text uitCubes;
    public Text uitButton;
    public Vector3 puzzlePos;
    public GameObject[] puzzlesArray;
    public GameObject player;
    public Button button;

    [Header("Set Dynamically")]
    public int level;
    public int levelMax;
    public int cubeLefted = 4;
    public GameObject puzzle;
    public GameMode mode = GameMode.idle;
    

    private void Start()
    {
        S = this;
        level = 0;
        levelMax = puzzlesArray.Length;
        StartLevel();
    }
    public void StartLevel()
    {
        if (puzzle != null)
        {
            Destroy(puzzle);
        }
        puzzle = Instantiate<GameObject>(puzzlesArray[level]);
        puzzle.transform.position = puzzlePos;
        cubeLefted = 4;
        UpdateGUI();
        mode = GameMode.playing;
    }

    void UpdateGUI()
    {
        uitLevel.text = "Current Level: " + (level + 1) + " of " + levelMax;
        uitCubes.text = "Cube Lefted: " + S.cubeLefted;
    }
    private void Update()
    {
        UpdateGUI();
        if ((mode == GameMode.playing) && S.cubeLefted<= 0)
        {
            Player.disableControl();
            CameraFollow.disableFollow();
            player.transform.position = Player.initialPosition;
            player.GetComponent<Rigidbody>().isKinematic = true;
            mode = GameMode.levelEnd;
            Invoke("NextLevel", 2f);
        }

    }

    public void RestartLevel()
    {
        Player.disableControl();
        CameraFollow.disableFollow();
        player.transform.position = Player.initialPosition;
        player.GetComponent<Rigidbody>().isKinematic = true;
        StartLevel();
    }

    void NextLevel()
    {
        level++;
        if (level == levelMax)
        {
            level = 0;
        }
        StartLevel();
    }


        public static void CubeCollected()
        {
            S.cubeLefted--;
        }
    }
