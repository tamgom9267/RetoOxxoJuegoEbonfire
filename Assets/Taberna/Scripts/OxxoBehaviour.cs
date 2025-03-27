using UnityEngine;

public class OxxoBehaviour : MonoBehaviour
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && pointer.activeSelf) {
            infOxxo.SetActive(true);
        }
    }
}
