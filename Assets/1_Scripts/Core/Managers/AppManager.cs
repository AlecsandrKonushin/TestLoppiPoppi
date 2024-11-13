using Core.Main;

namespace Core.Managers
{
    public class AppManager : Singleton<AppManager>
    {
        private void Start()
        {
            Localizator.Instance.Init();
            LoadSaves();
        }

        private void LoadSaves()
        {
            LoadSaveManager.OnLoad.AddListener(() =>
            {
                LoadSaveManager.OnLoad.RemoveAllListeners();
                
                LoadSceneManager.Instance.LoadGameScene();
            });

            LoadSaveManager.LoadData();
        }
    }
}