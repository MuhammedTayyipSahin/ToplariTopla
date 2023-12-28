using UnityEngine;

public class LevelManager : MonoBehaviour
{
    
    
    [SerializeField] private Transform levelHolder;
    [SerializeField] private int totalLevelCounter;

    private OnLevelLoaderCommand _onLevelLoaderCommand;
    private OnLevelDestroyerCommand _onLevelDestroyerCommand;
     
    private int _currentLevel; 
    private LevelData _levelData; 

    private void Awake()
    {
       _levelData = GetLevelData();
       _currentLevel  = GetActiveLevel();

    }

    void Init(){
        _onLevelLoaderCommand = new OnLevelLoaderCommand(levelHolder);
        _onLevelDestroyerCommand = new OnLevelDestroyerCommand(levelHolder);    
    }

    

    public void LevelLoader(int levelIndex){
        _onLevelLoaderCommand.Execute(levelIndex);
    }

    private int GetActiveLevel()
    {
        return _currentLevel;

        //Easy saving gelicek..
    }

    private LevelData GetLevelData()
    {
        return Resources.Load<CD_level>("Data/CD_Level").levels[_currentLevel]; 
    }

    void OnEnable()
    {
        SubscribeEvents();
    
    }

    private void SubscribeEvents()
    {
        CoreGameSignals.Instance.onLevelInitialize += _onLevelLoaderCommand.Execute;
        CoreGameSignals.Instance.onClearActiveLevel += _onLevelDestroyerCommand.Destroy; 
        CoreGameSignals.Instance.onGetLevelValue += OnGetLevelValue;
        CoreGameSignals.Instance.onNextLevel += OnNextLevel;
        CoreGameSignals.Instance.onRestartLevel += OnRestartLevel;

    }
    private void UnsubscribeEvents()
    {
        CoreGameSignals.Instance.onLevelInitialize -= _onLevelLoaderCommand.Execute;
        CoreGameSignals.Instance.onClearActiveLevel -= _onLevelDestroyerCommand.Destroy; 
        CoreGameSignals.Instance.onGetLevelValue -= OnGetLevelValue;
        CoreGameSignals.Instance.onNextLevel -= OnNextLevel;
        CoreGameSignals.Instance.onRestartLevel -= OnRestartLevel;

    }


    private void OnDisable()
    {   
        UnsubscribeEvents();
    }

    public int OnGetLevelValue(){
        return _currentLevel;
    }

    void Start()
    {
        CoreGameSignals.Instance.onLevelInitialize?.Invoke( _currentLevel % totalLevelCounter );
        CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelType.Start,1);
    }

    public void OnNextLevel(){
        _currentLevel++;
        CoreGameSignals.Instance.onClearActiveLevel?.Invoke();
        CoreGameSignals.Instance.onReset?.Invoke();
        CoreGameSignals.Instance.onLevelInitialize?.Invoke(_currentLevel % totalLevelCounter);
         
    }

    public void OnRestartLevel(){
        CoreGameSignals.Instance.onClearActiveLevel?.Invoke();
        CoreGameSignals.Instance.onReset?.Invoke();
        CoreGameSignals.Instance.onLevelInitialize?.Invoke(_currentLevel % totalLevelCounter);
    }



}