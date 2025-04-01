using UnityEngine;

public class Debriefing : MonoBehaviour
{
    [SerializeField] GameObject infOxxo;
    [SerializeField] int currentOxxoID = -1;
    [SerializeField] GameObject pointer1;
    [SerializeField] GameObject pointer2;
    [SerializeField] GameObject pointer3;
    [SerializeField] GameObject pointer4;
    [SerializeField] GameObject pointer5;
    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startBattle() {
        GameControl.Instance.goBattle();
    }

    public void nevermind() {
        infOxxo.SetActive(false);
        if (currentOxxoID == 1)
            pointer1.SetActive(false);
        else if (currentOxxoID == 2)
            pointer2.SetActive(false);
        else if (currentOxxoID == 3)
            pointer3.SetActive(false);
        else if (currentOxxoID == 4)
            pointer4.SetActive(false);
        else if (currentOxxoID == 5)
            pointer5.SetActive(false);
    }
    public void SetCurrentOxxoID(int id)
    {
        currentOxxoID = id;
    }
}
