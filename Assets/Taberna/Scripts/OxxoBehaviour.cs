using UnityEngine;

public class OxxoBehaviour : MonoBehaviour
{
    [SerializeField] Debriefing debriefing;
    [SerializeField] GameObject infOxxo;
    [SerializeField] GameObject pointer;
    [SerializeField] int oxxoID;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && pointer.activeSelf) {
            infOxxo.SetActive(true);
            debriefing.SetCurrentOxxoID(oxxoID);
        }
    }
}
