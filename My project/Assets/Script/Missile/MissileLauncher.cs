using UnityEngine;

public class MissileLauncher : MonoBehaviour
{
    [SerializeField] Transform player;      // �v���C���[
    [SerializeField] Transform firePoint;    // �~�T�C���̔��ˈʒu
    [SerializeField] GameObject missilePrefab; // �~�T�C���v���n�u
    [SerializeField] float attackRange = 10f; // ���˂���ŒZ����

    private bool isMissileActive = false; // �~�T�C�����A�N�e�B�u���ǂ���

    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(player.position, transform.position);

        if (distance <= attackRange && !isMissileActive)
        {
            FireMissile();
        }
    }

    void FireMissile()
    {
        var missile = Instantiate(missilePrefab, firePoint.position, firePoint.rotation);
        isMissileActive = true;

        // HomingMissile �� Target ���w����\
        var homing = missile.GetComponent<HomingMissile>();

        if (homing)
        {
            homing.transform.rotation = firePoint.rotation;
        }

        // �~�T�C�����j�󂳂ꂽ���ɒʒm
        missile.AddComponent<MissileTracker>().Init(this);
    }

    // �~�T�C�������ł������ɍĔ��ˉ\�ɂ���
    public void OnMissileDestroyed()
    {
        isMissileActive = false;
    }
}

public class MissileTracker : MonoBehaviour
{
    private MissileLauncher launcher;

    public void Init(MissileLauncher launcher)
    {
        this.launcher = launcher;
    }

    private void OnDestroy()
    {
        if (launcher)
        {
            launcher.OnMissileDestroyed();
        }
    }
}
