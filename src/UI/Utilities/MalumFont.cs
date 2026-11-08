using UnityEngine;

namespace MalumMenu;

/// <summary>
/// 中文字体切换（汉化 by 微生猫）。
/// IMGUI 默认字体是 Unity 内置 Arial，不含汉字，会把中文渲染成方块。
/// 这里在菜单绘制前把 GUI.skin.font 换成系统中文动态字体，失败则静默跳过。
/// </summary>
internal static class MalumFont
{
    private static readonly string[] Names =
    {
        "Microsoft YaHei", "微软雅黑", "SimHei", "黑体",
        "Noto Sans CJK SC", "PingFang SC", "Arial Unicode MS",
    };

    private static Font _font;
    private static bool _failed;

    internal static void Apply()
    {
        if (_failed)
            return;

        try
        {
            if (_font == null)
                _font = Font.CreateDynamicFontFromOSFont(Names, 15);

            if (_font == null)
            {
                _failed = true;
                return;
            }

            if (GUI.skin.font != _font)
                GUI.skin.font = _font;
        }
        catch
        {
            _failed = true;
        }
    }
}
