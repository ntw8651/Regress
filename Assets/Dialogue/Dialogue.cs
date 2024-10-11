using UnityEngine;

[System.Serializable] // 구조체가 인스펙터 창에 보이게 하기 위한 작업
public struct TalkData
{
    public string name; // 대사 치는 캐릭터 이름
    public string[] contexts; // 대사 내용
}
public class Dialogue : MonoBehaviour
{

    // 대화 이벤트 이름
    [SerializeField] string eventName = null;
    // 위에서 선언한 TalkData 배열
    [SerializeField] TalkData[] talkDatas = null;
    
    public TalkData[] GetObjectDialogue()
    {
        return DialogueParse.GetDialogue(eventName);
    }
}