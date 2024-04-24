
using Unity.VisualScripting;
using UnityEngine;

public class Character_SkillReleasePoint : MonoBehaviour
{
    public Transform center; // ���ĵ�
    public Transform tragger; // ��������Ԥ����
    public GameObject enemy;
    private GameObject enemyChest;
    public float distance = 0.5f; // Ԥ����A��B֮��ľ���



    private void FixedUpdate()
    {
        if(enemy != null)
        {
            // ����Ԥ����B��Ҫ����ķ���
            enemyChest = enemy.transform.Find("Chest").gameObject;
            Vector3 direction = enemyChest.transform.position - center.position;
            tragger.rotation = Quaternion.LookRotation(Vector3.forward, direction);

            // ����Ԥ����B��Ҫ�����λ��
            Vector3 targetPosition = center.position + direction.normalized * distance;
            //targetPosition.y += 0.6f;

            // ��Ԥ����B�ƶ���Ŀ��λ��
            tragger.position = Vector3.Lerp(tragger.position, targetPosition, 0.5f);
        }


    }
}
