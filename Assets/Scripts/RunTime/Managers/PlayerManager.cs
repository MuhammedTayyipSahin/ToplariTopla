
using UnityEngine;


    public class PlayerManager : MonoBehaviour
    {

        public byte StageValue;

        internal ForceBallsToPoolCommand ForceCommand;


        [SerializeField] private PlayerMovementController movementController;
        [SerializeField] private PlayerMeshController meshController;
        [SerializeField] private PlayerPhysicsController physicsController;

         

        private PlayerData _data;


        private void Awake()
        {
            _data = GetPlayerData();
            SendDataToControllers();
            Init();
        }

        private PlayerData GetPlayerData()
        {
            return Resources.Load<CD_Player>("Data/CD_Player").playerData;
        }

        private void SendDataToControllers()
        {
            movementController.SetData(_data.movementData);
            meshController.SetData(_data.meshData);
        }

        private void Init()
        {
            ForceCommand = new ForceBallsToPoolCommand(this, _data.forceData);
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            InputSignals.Instance.onInputTaken += () => movementController.IsReadyToMove(true);
            InputSignals.Instance.onInputReleased += () => movementController.IsReadyToMove(false);
            InputSignals.Instance.onInputDragged += OnInputDragged;
            UISignals.Instance.onPlay += () => movementController.IsReadyToPlay(true);
            CoreGameSignals.Instance.onLevelSuccessful += () => movementController.IsReadyToPlay(false);
            CoreGameSignals.Instance.onLevelFailed += () => movementController.IsReadyToPlay(false);
            CoreGameSignals.Instance.onStageAreaEntered += () => movementController.IsReadyToPlay(false);
            CoreGameSignals.Instance.onStageAreaSuccessful += OnStageAreaSuccessful;
            CoreGameSignals.Instance.onFinishAreaEntered += OnFinishAreaEntered;
            CoreGameSignals.Instance.onReset += OnReset;
        }

        private void OnInputDragged(HorizontalInputParams inputParams)
        {
            movementController.UpdateInputParams(inputParams);
        }

        private void OnStageAreaSuccessful(byte value)
        {
            StageValue = ++value;
            movementController.IsReadyToPlay(true);
            meshController.ScaleUpPlayer();
            meshController.PlayConfetiParticle();
            meshController.ShowUpText();
        }

        private void OnFinishAreaEntered()
        {
            CoreGameSignals.Instance.onLevelSuccessful?.Invoke();
            //Mini Game Yazılmalı
        }


        private void OnReset()
        {
            StageValue = 0;
            movementController.OnReset();
            physicsController.OnReset();
            meshController.OnReset();
        }

        private void UnSubscribeEvents()
        {
            InputSignals.Instance.onInputTaken -= () => movementController.IsReadyToMove(true);
            InputSignals.Instance.onInputReleased -= () => movementController.IsReadyToMove(false);
            InputSignals.Instance.onInputDragged -= OnInputDragged;
            UISignals.Instance.onPlay -= () => movementController.IsReadyToPlay(true);
            CoreGameSignals.Instance.onLevelSuccessful -= () => movementController.IsReadyToPlay(false);
            CoreGameSignals.Instance.onLevelFailed -= () => movementController.IsReadyToPlay(false);
            CoreGameSignals.Instance.onStageAreaEntered -= () => movementController.IsReadyToPlay(false);
            CoreGameSignals.Instance.onStageAreaSuccessful -= OnStageAreaSuccessful;
            CoreGameSignals.Instance.onFinishAreaEntered -= OnFinishAreaEntered;
            CoreGameSignals.Instance.onReset -= OnReset;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
    }
