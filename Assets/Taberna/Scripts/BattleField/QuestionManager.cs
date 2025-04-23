using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.Networking;

public class QuestionManager : MonoBehaviour
{
    public GameObject questionPanel;
    public Text questionText;

    public Text textA;
    public Text textB;
    public Text textC;
    public Text textD;

    public PreguntasTabernaGet apiFacil;
    public PreguntasTabernaGet apiDificil;

    private bool isHardQuestion;
    private PreguntaTaberna currentQuestion;
    private TurnManager turnManager;
    private bool isHealing;

    void Start()
    {
        questionPanel.SetActive(false);
    }

    public void ShowQuestion(TurnManager manager, bool useHard, bool healing = false)
    {
        turnManager = manager;
        isHealing = healing;
        isHardQuestion = useHard;

        List<PreguntaTaberna> pool = useHard ? apiDificil.preguntas : apiFacil.preguntas;

        if (pool.Count == 0)
        {
            Debug.LogWarning("No hay preguntas cargadas desde la API.");
            return;
        }

        currentQuestion = pool[Random.Range(0, pool.Count)];

        // Actualizar UI
        questionText.text = currentQuestion.pregunta;
        textA.text = currentQuestion.respuesta1;
        textB.text = currentQuestion.respuesta2;
        textC.text = currentQuestion.respuesta3;
        textD.text = currentQuestion.respuesta4;

        questionPanel.SetActive(true);
    }

    // Asignar estas funciones en el Inspector a cada botón
    public void AnswerA() { Answer(currentQuestion.respuesta1); }
    public void AnswerB() { Answer(currentQuestion.respuesta2); }
    public void AnswerC() { Answer(currentQuestion.respuesta3); }
    public void AnswerD() { Answer(currentQuestion.respuesta4); }

    private void Answer(string seleccion)
    {
        bool correcta = seleccion.Trim().ToLower() == currentQuestion.respuestaCorrecta.Trim().ToLower();
        questionPanel.SetActive(false);
        turnManager.ReceiveAnswer(correcta, isHealing, isHardQuestion);
    }
}
