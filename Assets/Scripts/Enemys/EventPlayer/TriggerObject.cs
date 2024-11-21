using UnityEngine;

public class TriggerObject : MonoBehaviour
{
    public EventEnemyMove eventEnemyMove;  // 追跡する敵のAIスクリプトをリンク

    // プレイヤーがトリガーゾーンに入った時に呼ばれる
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // プレイヤーがトリガーゾーンに入ったら敵に追跡を開始させる
            eventEnemyMove.StartChasing();  // 追跡開始メソッドを呼び出す
        }
    }
}
