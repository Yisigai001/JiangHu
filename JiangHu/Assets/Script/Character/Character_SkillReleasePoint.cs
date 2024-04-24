
using Unity.VisualScripting;
using UnityEngine;

public class Character_SkillReleasePoint : MonoBehaviour
{
    public Transform center; // 中心点
    public Transform tragger; // 跟随鼠标的预制体
    public GameObject enemy;
    private GameObject enemyChest;
    public float distance = 0.5f; // 预制体A和B之间的距离



    private void FixedUpdate()
    {
        if(enemy != null)
        {
            // 计算预制体B需要朝向的方向
            enemyChest = enemy.transform.Find("Chest").gameObject;
            Vector3 direction = enemyChest.transform.position - center.position;
            tragger.rotation = Quaternion.LookRotation(Vector3.forward, direction);

            // 计算预制体B需要跟随的位置
            Vector3 targetPosition = center.position + direction.normalized * distance;
            //targetPosition.y += 0.6f;

            // 将预制体B移动到目标位置
            tragger.position = Vector3.Lerp(tragger.position, targetPosition, 0.5f);
        }


    }
}
