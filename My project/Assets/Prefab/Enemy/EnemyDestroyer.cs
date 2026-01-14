using UnityEngine;

public class EnemyDestroyer : MonoBehaviour
{
    [Header("効果音")]
    [SerializeField] private AudioClip destroySE;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy")) return;

        // ★ 効果音だけを再生するオブジェクトを生成
        PlaySE(collision.transform.position);

        // Enemy を削除
        Destroy(collision.gameObject);
    }

    private void PlaySE(Vector3 position)
    {
        GameObject seObj = new GameObject("EnemyDestroySE");
        seObj.transform.position = position;

        AudioSource source = seObj.AddComponent<AudioSource>();
        source.clip = destroySE;
        source.volume = 1f;
        source.spatialBlend = 0f; // 2D音
        source.Play();

        Destroy(seObj, destroySE.length);
    }
}
