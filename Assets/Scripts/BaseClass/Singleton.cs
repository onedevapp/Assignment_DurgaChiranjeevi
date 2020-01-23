﻿using UnityEngine;

namespace OneDevApp
{
    /// <summary>
    /// Singleton.
    /// </summary>
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        // ======================== Variables ======================== //

        #region Variables

        /// <summary>
        /// The is persistence.
        /// </summary>
        [SerializeField] private bool IsPersistence;

        /// <summary>
        /// The m instance.
        /// </summary>
        protected static T m_Instance;

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>The instance.</value>
        public static T Instance
        {
            get { return m_Instance; }
        }

        #endregion


        // ======================== Functional ====================== //

        #region Functional 

        /// <summary>
        /// Awake this instance.
        /// </summary>
        protected virtual void Awake()
        {
            if (IsPersistence)
            {
                if (ReferenceEquals(m_Instance, null))
                {
                    m_Instance = this as T;

                    DontDestroyOnLoad(gameObject);
                }
                else if (!ReferenceEquals(m_Instance, this as T))
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                m_Instance = this as T;
            }
        }

        protected virtual void OnDestroy()
        {
            m_Instance = null;
        }

        #endregion
    }
}