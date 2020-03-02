using UnityEngine;

namespace KartGame.KartSystems
{
    /// <summary>
    /// A basic gamepad implementation of the IInput interface for all the input information a kart needs.
    /// </summary>
    public class SteeringInput : MonoBehaviour, IInput
    {

        LogitechGSDK.LogiControllerPropertiesData properties;

        public float xAxes;

        public float Acceleration
        {
            get { return m_Acceleration; }
        }
        public float Steering
        {
            get { return m_Steering; }
        }
        public bool BoostPressed
        {
            get { return m_BoostPressed; }
        }
        public bool FirePressed
        {
            get { return m_FirePressed; }
        }
        public bool HopPressed
        {
            get { return m_HopPressed; }
        }
        public bool HopHeld
        {
            get { return m_HopHeld; }
        }

        float m_Acceleration;
        float m_Steering;
        bool m_HopPressed;
        bool m_HopHeld;
        bool m_BoostPressed;
        bool m_FirePressed;

        bool m_FixedUpdateHappened;

        private void Start()
        {
            //print(LogitechGSDK.LogiSteeringInitialize(false));
        }

        void Update ()
        {

            if (LogitechGSDK.LogiUpdate() && LogitechGSDK.LogiIsConnected(0)) {
                LogitechGSDK.DIJOYSTATE2ENGINES rec;
                rec = LogitechGSDK.LogiGetStateUnity(0);

                xAxes = rec.lX / 32768f; // -1 0 1

                m_Steering = xAxes;
                
                m_Acceleration = ((-rec.lY + 32767f) / 65535f) + ((rec.lRz - 32767f) / 65535f);
            }

            if (m_FixedUpdateHappened)
            {
                m_FixedUpdateHappened = false;

                m_HopPressed = false;
                m_BoostPressed = false;
                m_FirePressed = false;
            }

        }

        void FixedUpdate ()
        {
            m_FixedUpdateHappened = true;
        }
    }
}