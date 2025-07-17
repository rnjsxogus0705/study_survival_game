    using UnityEngine;

    public class MANAGER : MonoBehaviour
    {
        public static MANAGER instance = null;
        public static Pool_Mng POOL;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }

            POOL = GetComponentInChildren<Pool_Mng>();
        }
    }