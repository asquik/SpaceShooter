using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UniRx;

public class PlayerShootingController : MonoBehaviour
{

    [SerializeField]
    private GameObject boltPrefab;
    [SerializeField]
    private Transform boltSpawnPos;
    private float fireRate;
    IDisposable subscription;

    void Start()
    {
        Invoke("CreateObservable", 0.25f);
    }

    private void CreateObservable()
    {
        subscription = Observable.EveryUpdate()
            .Where(_ => Input.GetAxisRaw("Fire1") != 0 ||
                Input.GetAxisRaw("Fire2") != 0 ||
                Input.GetAxisRaw("Fire3") != 0 ||
                Input.GetAxisRaw("Jump") != 0)
            .ThrottleFirst(TimeSpan.FromMilliseconds(fireRate))
            .Subscribe(_ =>
            {

                Instantiate(boltPrefab, boltSpawnPos.position, boltSpawnPos.rotation);

            }).AddTo(this);
    }

    public void SetFireRate(int _fireRate)
    {
        fireRate = _fireRate;
    }

    public void Stop()
    {
        subscription.Dispose();
    }
    
}
