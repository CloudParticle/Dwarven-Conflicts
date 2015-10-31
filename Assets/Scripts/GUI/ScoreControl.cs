using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreControl : MonoBehaviour {
    //TODO: Set score for specific player.
    public Text log;
    private int score = 15;

    void Start () {
        initScore(0);
        //renderScore();
    }

    void initScore (int player) {
        print("Start count: " + score);
        log = GameObject.Find("Log" + player).GetComponent<Text>();
    }

    public void setScore (int scoreValue, int player) {
        score = scoreValue;
        renderScore(player);
    }

    public int subtractScore (int player) {
        if (score >  0) {
            score--;
        }
        renderScore(player);
        return score;
    }

    private void renderScore (int player) {
        print("P" + player + " score = " + score);
        log.text = score.ToString();
    }
}