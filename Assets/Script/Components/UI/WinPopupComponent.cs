using UnityEngine;
using UnityEngine.UI;
using Assets.Script.Core.Events;

public class WinPopupComponent : MonoBehaviour
{
    void Start()
    {
        transform.Find("ResetBtn").GetComponent<Button>().onClick.AddListener(ResetRound);
        transform.Find("NextBtn").GetComponent<Button>().onClick.AddListener(NextRound);
    }

    private void ResetRound()
    {
        EventManager.Instance.Dispatch<string>("ResetRound", null);
        EventManager.Instance.Dispatch<string>("ClosePopup", this.gameObject.name);
    }

    private void NextRound()
    {
        EventManager.Instance.Dispatch<string>("LoadNextRound", null);
        EventManager.Instance.Dispatch<string>("ClosePopup", this.gameObject.name);
    }
}
