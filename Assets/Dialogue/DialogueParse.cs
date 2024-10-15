using System;
using TMPro;
using System.Collections.Generic;
using UnityEngine;

public class DialogueParse : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text contextText;
    // 대사를 저장할 구조체
    private static Dictionary<string, TalkData[]> DialoueDictionary = 
        new Dictionary<string, TalkData[]>();
    
    // [SerializeField] 는 private로 선언된 변수도 인스펙터 창에 노출되게 해줍니다.
    [SerializeField] private TextAsset csvFile = null;

    public static TalkData[] GetDialogue(string eventName)
    {
        return DialoueDictionary[eventName];
    }

    private void Start()
    {
        SetTalkDictionary();
    }


    public void SetTalkDictionary()
    {
        // 아래 한 줄 빼기
        // string csvText = csvFile.text.Substring(0, csvFile.text.Length - 1); -> 맥북이라 그런건지는 모르겠지만 대사 바꿀때마다 아래 줄바꿈이 없어서 이 코드를 뺌 & 아래 코드 추가
        string csvText = csvFile.text; // 줄바꿈 생기면 위에 주석처리된 코드로 바꾸기
        // 줄바꿈(한 줄)을 기준으로 csv 파일을 쪼개서 string배열에 줄 순서대로 담음
        string[] rows = csvText.Split(new char[] { '\n' });

        // 엑셀 파일 1번째 줄은 편의를 위한 분류이므로 i = 1부터 시작
        for (int i = 1; i < rows.Length; i++)
        {
            // A, B, C열을 쪼개서 배열에 담음
            string[] rowValues = rows[i].Split(new char[] { ',' });

            // 유효한 이벤트 이름이 나올때까지 반복
            if (rowValues[0].Trim() == "" || rowValues[0].Trim() == "end") continue;

            List<TalkData> talkDataList = new List<TalkData>();
            string eventName = rowValues[0];
            Debug.Log(eventName);

            while(rowValues[0].Trim() != "end") // talkDataList 하나를 만드는 반복문
            {
                // 캐릭터가 한번에 치는 대사의 길이를 모르므로 리스트로 선언
                List<string> contextList = new List<string>();

                TalkData talkData;
                talkData.name = rowValues[1]; // 캐릭터 이름이 있는 B열

                do // talkData 하나를 만드는 반복문
                {
                    contextList.Add(rowValues[2].ToString());
                    if(++i < rows.Length) 
                        rowValues = rows[i].Split(new char[] { ',' });
                    else break;
                } while (rowValues[1] == "" && rowValues[0] != "end");

                talkData.contexts = contextList.ToArray();
                talkDataList.Add(talkData);
            }

            DialoueDictionary.Add(eventName, talkDataList.ToArray()); // 이벤트 이름과 대사들을 딕셔너리에 추가
        }
    }
    
    public void DebugDialogue(TalkData[] talkDatas)
    {
        for (int i = 0; i < talkDatas.Length; i++)
        {
            // 캐릭터 이름 출력
            Debug.Log(talkDatas[i].name);
            nameText.text = talkDatas[i].name;
            // 대사들 출력
            foreach (string context in talkDatas[i].contexts)
            {
                Debug.Log(context);
                contextText.text = context;
            } 
            
        }
    }
}