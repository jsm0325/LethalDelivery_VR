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
            score1.text = "���� ���ھ�\n\n" + GameManager2.currentScore.ToString();
        }
        else if (currentSceneName == "Gameover_Lethal")
        {
            score1.text = "������ ����Ǿ����ϴ� !";
            score2.text = GameManager.Instance.GetCurrentDay().ToString() + "��";
            score3.text = "�����ϼ̽��ϴ� !";
        }
    }

}
