using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class LastScore : MonoBehaviour
{
    public TextMeshProUGUI score1;
    public TextMeshProUGUI score2;
    public TextMeshProUGUI score3;

    void Start()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        if (currentSceneName == "Gameover")
        {
            score1.text = "최종 스코어\n\n" + GameManager2.currentScore.ToString();
        }
        else if (currentSceneName == "Gameover_Lethal")
        {
            score1.text = "게임이 종료되었습니다 !";
            score2.text = GameManager.Instance.GetCurrentDay().ToString() + "일";
            score3.text = "생존하셨습니다 !";
        }
    }

}
