/*
	* Coder :
	* Last Update :
	* Information
*/
namespace project02
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using static project02.EnemyStatData;

    public partial class Enemy : CombatObjectBase // Data Field
    {
        [field: SerializeField] public EnemyMovement EnemyMovement { get; private set; }
        [field: SerializeField] public EnemyAnimation EnemyAnimation { get; private set; }

        [field: SerializeField] private Collider2D enemyCollider;
        public Transform PlayerTransform { get; private set; }


        private float cameraZPosition;
        private float yPosition;
    }

    public partial class Enemy : CombatObjectBase // Data Property
    {
        private EnemyState enemyState;
        public EnemyState EnemyState
        {
            get => enemyState;
            set
            {
                if (value != enemyState)
                {
                    enemyState = value;
                    if (enemyState == EnemyState.Death)
                        Dead();

                }

            }
        }

        private EnemyStatInformation enemyStatInformation;
        public EnemyStatInformation EnemyStatInformation
        {
            get => enemyStatInformation;
            private set
            {
                enemyStatInformation = new EnemyStatInformation()
                {
                    index = value.index,
                    name = value.name,
                    maxHp = value.maxHp,
                    attackPower = value.attackPower,
                    moveSpeed = value.moveSpeed,
                    dropItem = value.dropItem,
                    dropPercent = value.dropPercent,
                    criticalPercent = value.criticalPercent,
                    criticalIncreasePercent = value.criticalIncreasePercent,
                    dropGold = value.dropGold,
                };
                hp = value.maxHp;
                CalculateDamage(EnemyStatInformation);
            }
        }

        public override int HP
        {
            get => hp;
            set
            {
                if (hp != value && EnemyState != EnemyState.Death)
                {
                    hp = value;
                    if (hp > 0)
                    {
                        MainSystem.Instance.SoundManager.SoundController.SoundEffect.PlaySfx(AudioClipName.EnemyHit);
                        EnemyMovement.KnockBack();
                        EnemyAnimation.GetDamage();
                    }
                    else
                    {
                        MainSystem.Instance.SoundManager.SoundController.SoundEffect.PlaySfx(AudioClipName.EnemyDeath);
                        EnemyState = EnemyState.Death;
                    }

                }
            }
        }

    }
    public partial class Enemy : CombatObjectBase // Initialize
    {
        private void Allocate()
        {
            int index = (int)Enum.Parse<EnemyName>(name);
            //EnemyStatInformation = MainSystem.Instance.DataManager.EnemyStatData.GetData(index.ToString());
            EnemyStatInformation = MainSystem.Instance.DataManager.EnemyStatData.GetData(Enum.Parse<EnemyName>(name));
            enemyCollider = GetComponent<Collider2D>();
        }
        public override void Initialize()
        {
            base.Initialize();

            Allocate();
            Setup();
            EnemyMovement.Initialize(this);
            EnemyAnimation.Initialize(this);
        }
        private void Setup()
        {
            PlayerTransform = MainSystem.Instance.PlayerManager.Player.transform;
            EnemyState = EnemyState.Move;
            enemyCollider.enabled = true;
            yPosition = 0;
            cameraZPosition = Mathf.Abs(Camera.main.transform.position.z) - 1;
        }
    }
    public partial class Enemy : CombatObjectBase // Main
    {
        private void FixedUpdate()
        {
            if (EnemyState != EnemyState.Death)
                EnemyMovement.FixedProgress();
        }
        private void LateUpdate()
        {
            if (EnemyState != EnemyState.Death)
            {
                EnemyAnimation.LateProgress();
                SetZPosition();
            }
        }
    }
    public partial class Enemy : CombatObjectBase // Property
    {
        private void Dead()
        {
            enemyCollider.enabled = false;
            MainSystem.Instance.PlayerManager.Player.KillCount++;
            MainSystem.Instance.EnemyManager.SignDownEnemy(this);

            SpawnItem();
            MainSystem.Instance.GameManager.GameController.GoldFromThisRaid += EnemyStatInformation.dropGold;
            MainSystem.Instance.PoolManager.Despawn(gameObject);
        }
        private void SpawnItem()
        {
            float dropItemCount = EnemyStatInformation.dropItem.Length;
            float dropPercent = 0;
            float random = UnityEngine.Random.Range(0, 1.0f);
            for (int i = 0; i < dropItemCount; i++)
            {
                dropPercent = EnemyStatInformation.dropPercent[i];
                if (random <= dropPercent)
                {
                    MainSystem.Instance.ItemManager.ItemController.SpawnDropItem(EnemyStatInformation.dropItem[i], null, transform.position);
                    break;
                }
            }
        }

        private void SetZPosition()
        {
            float currentYPosition = transform.position.y;
            if (Mathf.Abs(yPosition - currentYPosition) >= 0.1f)
            {
                yPosition = currentYPosition;
                float playerYPosition = PlayerTransform.position.y;
                float zPosition = Mathf.Clamp((yPosition - playerYPosition), -cameraZPosition, cameraZPosition);
                Vector3 newPosition = transform.position;
                newPosition.z = zPosition;
                transform.position = newPosition;
            }
        }
    }
    public partial class Enemy : CombatObjectBase // TriggerEvent
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                Player player = collision.GetComponent<Player>();
                if (!player.IsHit)
                    SendDamage(player, EnemyStatInformation);
            }
        }
    }
}
