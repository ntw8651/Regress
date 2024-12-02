using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteraction
{
    /* 상호작용을 위한 인터페이스 스크립트
     * 아래 작성된 함수 & 변수는 상속받는 개체에서 필수로 구현되어야 함
     * Interact : 사용자가 상호작용 단추를 눌렀을 때 호출됨
     * Name : 상호작용 시, 사용자가 해당 상호작용 종류를 알 수 있도록 종류를 반환(변수명 변경 고려)
     */
    public void Interact(GameObject player);
    public string Name { get; }
}
