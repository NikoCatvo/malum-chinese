using UnityEngine;

namespace MalumMenu;

public class ShipTab : ITab
{
    public string name => "飞船";

    public void Draw()
    {
        GUILayout.BeginHorizontal();

        GUILayout.BeginVertical(GUILayout.Width(MenuUI.windowWidth * 0.425f));

        DrawGeneral();

        GUILayout.Space(15);

        DrawSabotage();

        GUILayout.EndVertical();

        GUILayout.BeginVertical();

        DrawVents();

        GUILayout.EndVertical();

        GUILayout.EndHorizontal();
    }

    private void DrawGeneral()
    {
        CheatToggles.unfixableLights = GUILayout.Toggle(CheatToggles.unfixableLights, " 无法修复的灯光");

        // CheatToggles.reportBody = GUILayout.Toggle(CheatToggles.reportBody, " Report Body");

        CheatToggles.callMeeting = GUILayout.Toggle(CheatToggles.callMeeting, " 召集会议");

        CheatToggles.closeMeeting = GUILayout.Toggle(CheatToggles.closeMeeting, " 关闭会议");

        CheatToggles.autoReportBodies = GUILayout.Toggle(CheatToggles.autoReportBodies, " 自动报告尸体");

        CheatToggles.autoOpenDoorsOnUse = GUILayout.Toggle(CheatToggles.autoOpenDoorsOnUse, " 使用时自动开门");
    }

    private void DrawSabotage()
    {
        GUILayout.Label("破坏", GUIStylePreset.TabSubtitle);

        CheatToggles.reactorSab = GUILayout.Toggle(CheatToggles.reactorSab, " 反应堆");

        CheatToggles.oxygenSab = GUILayout.Toggle(CheatToggles.oxygenSab, " 氧气");

        CheatToggles.elecSab = GUILayout.Toggle(CheatToggles.elecSab, " 灯光");

        CheatToggles.commsSab = GUILayout.Toggle(CheatToggles.commsSab, " 通信");

        CheatToggles.showDoorsMenu = GUILayout.Toggle(CheatToggles.showDoorsMenu, " 显示门菜单");

        CheatToggles.mushSab = GUILayout.Toggle(CheatToggles.mushSab, " 蘑菇混战");

        CheatToggles.mushSpore = GUILayout.Toggle(CheatToggles.mushSpore, " 触发孢子");

        CheatToggles.sabotageMap = GUILayout.Toggle(CheatToggles.sabotageMap, " 打开破坏地图");
    }

    private void DrawVents()
    {
        GUILayout.Label("管道", GUIStylePreset.TabSubtitle);

        CheatToggles.unlockVents = GUILayout.Toggle(CheatToggles.unlockVents, " 解锁管道");

        CheatToggles.kickVents = GUILayout.Toggle(CheatToggles.kickVents, " 把所有人踢出管道");

        CheatToggles.walkInVents = GUILayout.Toggle(CheatToggles.walkInVents, " 在管道中行走");
    }
}
