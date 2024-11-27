using UnityEngine;

public class Inter : MonoBehaviour, IInteraction
{
    public string Name { get; private set; } = "III";
    public GameObject player;

    // 대화 이벤트 이름
    [SerializeField] string eventName = null;
    // 위에서 선언한 TalkData 배열
    [SerializeField] TalkData[] talkDatas = null;
    
    public TalkData[] GetObjectDialogue()
    {
        return DialogueParseR.GetDialogue(eventName);
    }
    
    public void Interact(GameObject player)
    {
        TalkData[] talkDatas = GetObjectDialogue();
        if (talkDatas != null)
        {
            player.GetComponent<DialogueParseR>().InteractDialogue(talkDatas);
        }
    }
}