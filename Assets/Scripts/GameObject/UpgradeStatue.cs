using UnityEngine;
using UnityEngine.UI;

public class UpgradeStatue : MonoBehaviour
{
    [SerializeField]
    private Text _displayText;
    [SerializeField]
    private MainUIManager mainUIManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") 
        {
            mainUIManager.OpenUpgradeUI();
            Time.timeScale = 0;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        //if (collision.tag == "Player") 
        //{
        //    mainUIManager.CloseUpgradeUI();
        //}
    }
}
