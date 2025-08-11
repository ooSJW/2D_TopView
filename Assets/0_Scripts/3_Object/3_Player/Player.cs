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
    using System.ComponentModel;
    using Unity.VisualScripting;
    using UnityEngine;
    using static project02.PlayerStatData;

    public partial class Player : CombatObjectBase // Data Field
    {
        [field: SerializeField] public PlayerInput PlayerInput { get; private set; }
        [field: SerializeField] public PlayerJoystickInput PlayerJoystickInput { get; private set; }
        [field: SerializeField] public PlayerMovement PlayerMovement { get; private set; }
        [field: SerializeField] public PlayerCombat PlayerCombat { get; private set; }
        [field: SerializeField] public PlayerAnimation PlayerAnimation { get; private set; }
        [SerializeField] private Collider2D playerCollider;

        public List<CombatObjectBase> hitEnemyList;
    }

    public partial class Player : CombatObjectBase // Scan
    {
        [field: SerializeField] LayerMask targetLayer;
        private Transform target;
        private float scanRange = 50;
        private RaycastHit2D[] targetArray;

        public Transform Target
        {
            get
            {
                SetTarget();
                return target;
            }
            private set => target = value;
        }

        private void SetTarget()
        {
            targetArray = Physics2D.CircleCastAll(transform.position, scanRange, Vector2.zero, 0, targetLayer);
            Target = GetNearestEnemy();
        }
        private Transform GetNearestEnemy()
        {
            Transform nearestEnemy = null;
            float distance = scanRange;

            if (targetArray != null)
            {
                for (int i = 0; i < targetArray.Length; i++)
                {
                    Vector3 position = transform.position;
                    Vector3 targetPosition = targetArray[i].transform.position;
                    float currentDistance = Vector3.Distance(position, targetPosition);
                    if (distance > currentDistance)
                    {
                        distance = currentDistance;
                        nearestEnemy = targetArray[i].transform;
                    }
                }
            }
            return nearestEnemy;
        }
    }

    public partial class Player : CombatObjectBase // Data Property
    {
        private PlayerState playerState = PlayerState.Idle;
        public PlayerState PlayerState
        {
            get => playerState;
            set
            {
                if (playerState != value)
                    playerState = value;
                if (playerState == PlayerState.Death)
                {
                    playerCollider.enabled = false;
                    PlayerAnimation.PlayerDeath();
                }
            }
        }

        private PlayerStatInformation playerStatInformation;
        public PlayerStatInformation PlayerStatInformation
        {
            get => playerStatInformation;
            private set
            {
                playerStatInformation = new PlayerStatInformation()
                {
                    index = value.index,
                    name = value.name,
                    iconPath = value.iconPath,
                    characterDescription = value.characterDescription,
                    cost = value.cost,
                    moveSpeed = value.moveSpeed,
                    maxHp = value.maxHp,
                    attackPower = value.attackPower,
                    level = value.level,
                    maxExp = value.maxExp,
                    maxExpIncreasePercent = value.maxExpIncreasePercent,
                    criticalPercent = value.criticalPercent,
                    criticalIncreasePercent = value.criticalIncreasePercent,
                };
                Level = value.level;
                MaxHp = value.maxHp;
                HP = value.maxHp;
                MaxExp = value.maxExp;
                CalculateDamage(PlayerStatInformation);
            }
        }

        private PlayerUI playerUI;
        public PlayerUI PlayerUI { get => playerUI; set => playerUI = value; }

        private int level;
        public int Level
        {
            get => level;
            private set
            {
                level = value;
                MaxExp += (int)(MaxExp * playerStatInformation.maxExpIncreasePercent);
                PlayerUI.RefreshLevel();
                MainSystem.Instance.UIManager.UIContoller.SetActiveLevelUpUI();
            }
        }

        public override int HP
        {
            get => hp;
            set
            {
                if (value >= hp)
                    hp = value > maxHp ? maxHp : value;
                else if (value > 0)
                {
                    hp = value;
                    IsHit = true;
                }
                else
                {
                    hp = 0;
                    PlayerState = PlayerState.Death;
                    // MainSystem.Instance.UIManager.UIContoller.GameOver();
                }
                playerUI.RefreshHp();
            }
        }

        private int maxHp;
        public int MaxHp
        {
            get => maxHp;
            set
            {
                maxHp = value;
                playerUI.RefreshHp();
            }
        }

        private int maxExp;
        public int MaxExp { get => maxExp; set => maxExp = value; }

        private int exp;
        public int Exp
        {
            get => exp;
            set
            {
                exp = value;
                while (exp >= maxExp)
                {
                    exp -= MaxExp;
                    Level++;
                    PlayerUI.RefreshExp();
                }
                PlayerUI.RefreshExp();
            }
        }

        private int killCount;
        public int KillCount
        {
            get => killCount;
            set
            {
                killCount = value;
                PlayerUI.RefreshKillCount();
            }
        }
        private bool isHit;
        public bool IsHit
        {
            get => isHit;
            set
            {
                if (isHit != value)
                    isHit = value;
                if (isHit)
                    StartCoroutine(PlayerAnimation.Invincible());
            }
        }

        private Vector2 inputVector;
        public Vector2 InputVector
        {
            get => inputVector;
            set => inputVector = value;
        }

        private bool canMove;
        public bool Moveable { get => canMove; set => canMove = value; }

        public PlayerName PlayerName { get; private set; }

    }
    public partial class Player : CombatObjectBase // Initialize
    {
        private void Allocate()
        {
            // 객체 생성시 생성되는 객체에서 캐릭터에 알맞게 초기화 하도록 구현.ㄴ
            hitEnemyList = new List<CombatObjectBase>();
            PlayerName = Enum.Parse<PlayerName>(name);
            SignUpPlayerUI();
            PlayerStatInformation = MainSystem.Instance.DataManager.PlayerStatData.GetData(PlayerName);
        }
        public override void Initialize()
        {
            base.Initialize();

            Allocate();
            Setup();
            PlayerInput.Initialize(this);
            PlayerMovement.Initialize(this);
            PlayerAnimation.Initialize(this);
            PlayerCombat.Initialize(this);
            PlayerUI.RefreshPlayerUI();
        }
        private void Setup()
        {
            PlayerJoystickInput = MainSystem.Instance.PoolManager.Spawn("PlayerJoystick", null, default, true).GetComponent<PlayerJoystickInput>();
            PlayerJoystickInput.Initialize(this);
            Moveable = true;
            playerCollider.enabled = true;
        }
    }
    public partial class Player : CombatObjectBase // Main
    {
        //void OnDrawGizmos()
        //{
        //    Gizmos.color = Color.green;
        //    Gizmos.DrawWireSphere(transform.position, scanRange);  // scanRange에 해당하는 원 그리기
        //}

        private void Update()
        {
            //if (PlayerState != PlayerState.Death)
            //    // PlayerInput.Progress();
        }
        private void FixedUpdate()
        {
            if (PlayerState != PlayerState.Death)
                PlayerMovement.Move();
        }
        private void LateUpdate()
        {
            if (PlayerState != PlayerState.Death)
                PlayerAnimation.LateProgress();
        }
    }
    public partial class Player : CombatObjectBase // Property
    {
        private void SignUpPlayerUI()
        {
            PlayerUI = MainSystem.Instance.UIManager.UIContoller.PlayerUI;
            PlayerUI.Player = this;
        }

        public void GetShoes(float percent)
        {
            PlayerMovement.MoveSpeed += PlayerMovement.MoveSpeed * percent;
        }
        public void GetArmor(float percent)
        {
            MaxHp += (int)(MaxHp * percent);
        }
        public void GetGlove(float percent)
        {
            CalculateDamage(PlayerStatInformation, percent);
        }
        public void EndInvincible()
        {
            IsHit = false;
            gameObject.layer = LayerMask.NameToLayer("Player");
        }
    }
}
