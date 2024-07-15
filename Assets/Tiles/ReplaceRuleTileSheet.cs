using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
#if (UNITY_EDITOR) 
[ExecuteInEditMode]
public class ReplaceRuleTileSheet : MonoBehaviour
{
    #region Variables
    
        public RuleTile ruleTileToUpdate;
        public Texture replaceWith; // choose the default sprite & it will replace all of the sprites with the new sheet

        [Header("Tap to Replace")]
        public bool replace = false;
    
    #endregion

    #region Update
    
        private void Update()
        {
        
            if (replace) ReplaceRT();
        
        }
        
    #endregion

    #region Methods
        
    public void ReplaceRT()
    {
        // Get the sprites associated with this texture
        string path = AssetDatabase.GetAssetPath(replaceWith); // Get the path of the new texture
        path = path.Replace("Assets/Resources/",""); // Remove "Resources" path
        path = path.Replace(".png",""); // Remove file extension
        Sprite[] replaceSpriteList = Resources.LoadAll<Sprite>(path); // Load all sprites from the texture's path

        // Get the sprite number of the default sprite
        int dsn = -1;
        try
        {
            dsn = int.Parse(ruleTileToUpdate.m_DefaultSprite.name.Replace(ruleTileToUpdate.m_DefaultSprite.texture.name + "_", ""));
        }
        catch (FormatException)
        {
            // Debug.LogError("The Default sprite name is not in the correct format, make sure the sprites are named correctly.");
            return;
        }

        List<RuleTile.TilingRule> trList = ruleTileToUpdate.m_TilingRules; // Load all Tiling Rules from the RuleTile
        // Get the name & replace with the numbered tile of the "replaceWith" sprite's name. 
        // Set the Replaced Default tile to the same number as the original tilesheet
        foreach (RuleTile.TilingRule tr in trList)
        {
            for (int i = 0; i < tr.m_Sprites.Length; i++)
            {
                if (tr.m_Sprites[i] != null)
                {
                    // Get the sprite number
                    int sn = -1;
                    try
                    {
                        sn = int.Parse(tr.m_Sprites[i].name.Replace(tr.m_Sprites[i].texture.name + "_", "")); // sprite Number (remove
                    }
                    catch (FormatException)
                    {
                        Debug.LogError("The sprite name is not in the correct format, make sure the sprites are named correctly.");
                        return;
                    }

                    // Replace the sprite with the corresponding sprite from the new texture
                    tr.m_Sprites[i] = replaceSpriteList[sn];
                }
            }
        }

        // Replace the default sprite with the corresponding sprite from the new texture
        ruleTileToUpdate.m_DefaultSprite = replaceSpriteList[dsn];

        // Refresh the RuleTile
        EditorUtility.SetDirty(ruleTileToUpdate);
        AssetDatabase.SaveAssets();
    }
    #endregion
}
#endif
