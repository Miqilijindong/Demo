using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject gameResultPancel;
    public Text GameResultText;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeScorse(int scores)
    {
        Text text = transform.GetComponentInChildren<Text>();
        text.text = "分数:" + scores;
    }

    public void showGameResult(bool isWin)
    {
        gameResultPancel.SetActive(true);
        if (isWin)
        {
            GameResultText.text = "你赢了";
        } else
        {
            GameResultText.text = "你输了";
        }
    }
}
