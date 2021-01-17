using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public UnityEngine.UI.Text scoreLabel;
    public UnityEngine.UI.Image menu;
    public UnityEngine.UI.Button startButton;
    public static int score = 0;
    public static bool isGameStarted = false;
    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(delegate
        {
            menu.gameObject.SetActive(false);
            isGameStarted = true;
        });
    }

    // Update is called once per frame
    void Update()
    {
        scoreLabel.text = "Score: " + score;
    }
}
