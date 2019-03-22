using UnityEngine;

namespace DuloGames.UI
{
    public class Demo_LookAtCamera : MonoBehaviour
    {
         private Camera m_Camera;

        protected void Awake()
        {
             this.m_Camera = Camera.main;
        }

        void Update()
        {
            if (this.m_Camera)
                transform.rotation = Quaternion.LookRotation(this.m_Camera.transform.forward);
        }
    }
}
