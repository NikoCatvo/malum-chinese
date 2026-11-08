using UnityEngine;

namespace MalumMenu;

public class AnimationsTab : ITab
{
    public string name => "动画";

    public void Draw()
    {
        GUILayout.BeginVertical(GUILayout.Width(MenuUI.windowWidth * 0.425f));

        DrawGeneral();

        GUILayout.Space(15);

        DrawClientSided();

        GUILayout.EndVertical();
    }

    private void DrawGeneral()
    {
        CheatToggles.animShields = GUILayout.Toggle(CheatToggles.animShields, " 护盾");

        CheatToggles.animAsteroids = GUILayout.Toggle(CheatToggles.animAsteroids, " 陨石");

        CheatToggles.animEmptyGarbage = GUILayout.Toggle(CheatToggles.animEmptyGarbage, " 倒垃圾");

        CheatToggles.animMedScan = GUILayout.Toggle(CheatToggles.animMedScan, " 医疗扫描");

        CheatToggles.animCamsInUse = GUILayout.Toggle(CheatToggles.animCamsInUse, " 监控使用中");

        // CheatToggles.animPet = GUILayout.Toggle(CheatToggles.animPet, " Pet");
    }

    private void DrawClientSided()
    {
        GUILayout.Label("仅本地可见", GUIStylePreset.TabSubtitle);

        CheatToggles.moonWalk = GUILayout.Toggle(CheatToggles.moonWalk, " 太空步");
    }
}
