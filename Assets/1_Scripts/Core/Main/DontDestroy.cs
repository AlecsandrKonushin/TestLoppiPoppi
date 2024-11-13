using UnityEngine;

namespace Core.Main
{ 
    public class DontDestroy : MonoBehaviour
    {
        public void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
