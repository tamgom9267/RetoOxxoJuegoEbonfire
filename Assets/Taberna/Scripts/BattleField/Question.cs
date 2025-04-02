[System.Serializable]
public class Question
{
    public string questionText;
    public string[] options; // 4 opciones
    public int correctIndex; // índice de la respuesta correcta (0 a 3)
}
