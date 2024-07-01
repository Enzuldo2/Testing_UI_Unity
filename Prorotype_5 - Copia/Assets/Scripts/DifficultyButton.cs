using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    private Button button;

    private GameMenager gameMenager; 

    public int difficulty;

    // Start is called before the first frame update
    void Start()
    {
        gameMenager = GameObject.Find("Game Menager").GetComponent<GameMenager>();
        button = GetComponent<Button>();
        button.onClick.AddListener(SetDifficulty);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetDifficulty()
    {
        gameMenager.StartGame(difficulty); 
    }
}
