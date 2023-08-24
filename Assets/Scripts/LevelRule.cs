using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu]
public class LevelRule : ScriptableObject
{
    [SerializeField] private int m_StormStartRound;
    public int stormStartRound => m_StormStartRound;

    [SerializeField] private int m_SizeX;
    public int sizeX => m_SizeX;

    [SerializeField] private int m_SizeY;
    public int sizeY => m_SizeY;

    [SerializeField] [HideInInspector] private int[] m_GridID;
    public int[] gridID => m_GridID;


#if UNITY_EDITOR

    [CustomEditor(typeof(LevelRule))] [SerializeField]
    public class LevelRuleEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            LevelRule levelRule = (LevelRule)target;

            if (levelRule.m_SizeX < 1)
            {
                levelRule.m_SizeX = 1;
            }
            if (levelRule.m_SizeY < 1)
            {
                levelRule.m_SizeY = 1;
            }

            if (levelRule.m_SizeX * levelRule.m_SizeY != levelRule.m_GridID.Length)
            {
                levelRule.m_GridID = new int[levelRule.m_SizeX * levelRule.m_SizeY];
            }


            for (int i = 0; i < levelRule.m_SizeX; i++)
            {
                EditorGUILayout.BeginHorizontal();
                for (int j = 0; j < levelRule.m_SizeY; j++)
                {
                    levelRule.m_GridID[j + i * levelRule.m_SizeY] = EditorGUILayout.IntField(levelRule.m_GridID[j + i * levelRule.m_SizeY], GUILayout.MaxWidth(20), GUILayout.MaxHeight(20));
                }
                EditorGUILayout.EndHorizontal();
            }
        }
    }
#endif
}
