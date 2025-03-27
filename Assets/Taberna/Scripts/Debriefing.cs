using UnityEngine;

public class Debriefing : MonoBehaviour
{
    [SerializeField] GameObject infOxxo;
    [SerializeField] GameObject pointer;
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
        pointer.SetActive(false);
    }
}
