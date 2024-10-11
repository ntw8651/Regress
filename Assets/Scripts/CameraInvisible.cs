using UnityEngine;

public class CameraInvisible : MonoBehaviour
{
    public GameObject player;
    public void LateUpdate()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized; // 플레이어를 향하는 방향 벡터
        float distance = Vector3.Distance(player.transform.position, transform.position); // 플레이어와 카메라 사이의 거리
        RaycastHit[] hits = Physics.RaycastAll(transform.position, direction, distance,
            1 << LayerMask.NameToLayer("EnvironmentObject")); // EnvironmentObject 레이어에 속한 모든 객체에 대해 레이캐스트 수행

        for (int i = 0; i < hits.Length; i++)
        {
            TransparentObject[] obj = hits[i].transform.GetComponentsInChildren<TransparentObject>(); // 맞은 객체의 자식 객체들 중 TransparentObject 컴포넌트를 가진 객체들 가져오기

            for (int j = 0; j < obj.Length; j++)
            {
                obj[j]?.BecomeTransparent(); // 객체가 null이 아닌 경우 투명화 메서드 호출
            }
        }
    }
}