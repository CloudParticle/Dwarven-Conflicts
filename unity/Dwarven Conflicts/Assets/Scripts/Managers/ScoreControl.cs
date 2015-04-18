using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreControl : MonoBehaviour {
    public Text log;
    private int score = 15;

    void Start () {
        initScore();
        renderScore();
    }

    void initScore () {
        print("Start count: " + score);
    }

    public void setScore (int scoreValue, int player) {
        score = scoreValue;
        renderScore();
    }

    public int subtractScore (int player) {
        if (score >  0) {
            score -= 1;
        }
        renderScore();
        return score;
    }

    private void renderScore () {
        print("score: " + score);
        log.text = score.ToString();
    }
}