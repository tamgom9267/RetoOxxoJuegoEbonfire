using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class QuestionManager : MonoBehaviour
{
    public GameObject questionPanel;
    public Text questionText;

    public Text textA;
    public Text textB;
    public Text textC;
    public Text textD;

    private List<Question> easyQuestions;
    private List<Question> hardQuestions;

    private bool isHardQuestion;
    private Question currentQuestion;
    private PlayerCombat playerCombat;
    private bool isHealing;

    void Start()
    {
        questionPanel.SetActive(false); // Ocultar el panel al inicio

        easyQuestions = new List<Question>
        {
            new Question {
                questionText = "¿Cuánto es 5 + 3?",
                options = new string[] { "7", "8", "9", "10" },
                correctIndex = 1
            },
            new Question {
                questionText = "¿Cuánto es 6 - 2?",
                options = new string[] { "2", "3", "4", "5" },
                correctIndex = 2
            },
            new Question {
                questionText = "¿Cuánto es 9 ÷ 3?",
                options = new string[] { "1", "2", "3", "4" },
                correctIndex = 2
            }
        };

        hardQuestions = new List<Question>
        {
            new Question {
                questionText = "¿Cuánto es (3^2) + (2^3)?",
                options = new string[] { "15", "17", "13", "11" },
                correctIndex = 0
            },
            new Question {
                questionText = "¿Cuál es la raíz cuadrada de 81?",
                options = new string[] { "8", "9", "10", "11" },
                correctIndex = 1
            },
            new Question {
                questionText = "¿Cuánto es 7 × (4 + 2)?",
                options = new string[] { "42", "38", "48", "44" },
                correctIndex = 0
            }
        };
    }

    public void ShowQuestion(PlayerCombat player, bool useHard, bool healing = false)
    {
        playerCombat = player;
        isHealing = healing;
        isHardQuestion = useHard;

        List<Question> pool = useHard ? hardQuestions : easyQuestions;
        currentQuestion = pool[Random.Range(0, pool.Count)];

        // Actualizar UI
        questionText.text = currentQuestion.questionText;
        textA.text = currentQuestion.options[0];
        textB.text = currentQuestion.options[1];
        textC.text = currentQuestion.options[2];
        textD.text = currentQuestion.options[3];

        questionPanel.SetActive(true);
    }

    // Asignar estas funciones en el Inspector a cada botón
    public void AnswerA() { Answer(0); }
    public void AnswerB() { Answer(1); }
    public void AnswerC() { Answer(2); }
    public void AnswerD() { Answer(3); }

    private void Answer(int index)
    {
        bool correct = index == currentQuestion.correctIndex;
        questionPanel.SetActive(false);
        playerCombat.ReceiveAnswer(correct, isHealing, isHardQuestion);
    }
}
