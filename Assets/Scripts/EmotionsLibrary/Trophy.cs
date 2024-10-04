
//TODO: Change the achievement completion checks to an event bases system 
/*
    * All universal trophy data (such as sounds and UI elements) are contained
    * within the TrophyManager class rather than this. */


using System;
using UnityEngine;

namespace WhizKid.EmotionsLibrary
{
    [Serializable]
    [RequireComponent(typeof(Texture2D))]
    public class Trophy
    {
        #region Public Fields

        [Header("UI")]
        public GameObject bookTrophySprite;
        public GameObject achievementPage;


        [Header("Textures & Sprites")]
        public Texture2D lockedTexture;     //* set in inspector
        public Texture2D unlockedTexture;   //* set in inspector

        [HideInInspector] public Sprite lockedSprite;
        [HideInInspector] public Sprite unlockedSprite;

        #endregion  
        #region Serialized Private Fields

        //? Private
        [Header("Core Data")]
        [SerializeField] private int _id;
        [SerializeField] private string _name;

        [SerializeField] private bool _achieved;

        #endregion
        #region Properties

        public int ID { get { return _id; } }
        public string Name { get { return _name; } }
        public bool Achieved { get { return _achieved; } set { _achieved = value; } }

        #endregion
        
        public void InitTrophy()
        {
            if (!lockedTexture || !unlockedTexture)
            {
                Debug.Log($"Achievement {_id} textures not set");
                return;
            }

            lockedSprite = Sprite.Create(
                    lockedTexture,
                    new Rect(0.0f, 0.0f, lockedTexture.width, lockedTexture.height),
                    new Vector2(0.5f, 0.5f), 100.0f);

            unlockedSprite = Sprite.Create(
                unlockedTexture,
                new Rect(0.0f, 0.0f, unlockedTexture.width, unlockedTexture.height),
                new Vector2(0.5f, 0.5f), 100.0f);
        }
    }
}
