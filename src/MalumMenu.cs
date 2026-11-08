using System.IO;
using BepInEx;
using BepInEx.Unity.IL2CPP;
using UnityEngine.SceneManagement;
using System;
using UnityEngine;
using UnityEngine.Analytics;
using System.Collections.Generic;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;

namespace MalumMenu;

[BepInAutoPlugin]
[BepInProcess("Among Us.exe")]
public partial class MalumMenu : BasePlugin
{
    public Harmony Harmony { get; } = new(Id);
    public static MalumMenu Plugin;
    public new static ManualLogSource Log;
    public static readonly string ProfilePath = Path.Combine(Paths.ConfigPath, "MalumProfile.txt");

    public static MenuUI menuUI;
    public static ConsoleUI consoleUI;
    public static RolesUI rolesUI;
    public static OverloadUI overloadUI;
    public static DoorsUI doorsUI;
    public static TasksUI tasksUI;
    public static ProtectUI protectUI;
    public static KeybindListener keybindListener;

    public static string malumVersion = "3.3.1";
    public static List<string> supportedAU = new List<string> { "2026.9.29" };
    public static bool isPanicked = false;
    public static bool inStealthMode = false;

    public static ConfigEntry<string> menuKeybind;
    public static ConfigEntry<string> menuHtmlColor;
    public static ConfigEntry<bool> menuOpenOnMouse;
    public static ConfigEntry<bool> menuKeepSubwindowsOpen;
    public static ConfigEntry<bool> menuAllowClickThrough;
    public static ConfigEntry<string> spoofLevel;
    public static ConfigEntry<string> spoofPlatform;
    public static ConfigEntry<bool> spoofDeviceId;
    public static ConfigEntry<bool> noTelemetry;
    public static ConfigEntry<string> guestFriendCode;
    public static ConfigEntry<bool> guestMode;
    public static ConfigEntry<bool> autoLoadProfile;
    public static ConfigEntry<string> configEditor;
    public static ConfigEntry<int> adaptMaxStrength;
    public static ConfigEntry<float> adaptMaxCooldown;
    public static ConfigEntry<float> attackLogDelay;
    public static ConfigEntry<int> defaultStrength;
    public static ConfigEntry<float> defaultCooldown;
    public static ConfigEntry<int> killSwitchLvl;

    public override void Load()
    {
        Log = base.Log;
        Plugin = this;

        // Loads config settings
        menuKeybind = Config.Bind("MalumMenu.GUI",
                                "Keybind",
                                "Delete",
                                "用于开关菜单界面的按键。支持的按键列表见：https://docs.unity3d.com/Packages/com.unity.tiny@0.16/api/Unity.Tiny.Input.KeyCode.html");

        menuHtmlColor = Config.Bind("MalumMenu.GUI",
                                "Color",
                                "",
                                "自定义菜单界面的颜色，支持 HTML 颜色代码（例如 #FF6600）");

        menuOpenOnMouse = Config.Bind("MalumMenu.GUI",
                                "OpenOnMouse",
                                false,
                                "开启后，菜单界面总是在当前鼠标位置弹出");

        menuKeepSubwindowsOpen = Config.Bind("MalumMenu.GUI",
                                "KeepSubwindowsOpen",
                                false,
                                "开启后，关闭菜单界面时不会自动关闭它打开的子窗口");

        menuAllowClickThrough = Config.Bind("MalumMenu.GUI",
                                "AllowClicksThrough",
                                true,
                                "开启后，点击会穿透菜单界面，可以直接操作菜单后面的游戏界面");

        autoLoadProfile = Config.Bind("MalumMenu.Profile",
                                "AutoLoadProfile",
                                false,
                                "开启后，启动游戏时会自动加载你保存的按键与开关配置");

        configEditor = Config.Bind("MalumMenu.Config",
                                "ConfigEditor",
                                "notepad.exe",
                                "使用「打开配置」时用来打开配置文件的程序。可以是任意可执行文件，建议用文本编辑器");

        // GuestMode config settings are commented out as the cheats are broken in latest updates

        // guestMode = Config.Bind("MalumMenu.GuestMode",
        //                         "GuestMode",
        //                         false,
        //                         "When enabled, a new guest account will generate every time you start the game, allowing you to bypass account bans and PUID detection");

        // guestFriendCode = Config.Bind("MalumMenu.GuestMode",
        //                         "FriendName",
        //                         "",
        //                         "The username that will be used when setting a friend code for your guest account. IMPORTANT: Can only be used with GuestMode, needs to be ≤ 10 characters, and cannot include special characters/discriminator (#1234)");

        spoofLevel = Config.Bind("MalumMenu.Spoofing",
                                "Level",
                                "",
                                "在联机游戏中向他人显示的自定义等级，用来隐藏你的真实平台。注意：等级只能是 1 到 100001 之间的整数，小数无效");

        spoofPlatform = Config.Bind("MalumMenu.Spoofing",
                                "Platform",
                                "",
                                "在联机大厅中向他人显示的自定义游戏平台，用来隐藏你的真实平台。支持的平台列表见：https://skeld.js.org/enums/_skeldjs_constant.Platform.html");

        spoofDeviceId = Config.Bind("MalumMenu.Privacy",
                                "HideDeviceId",
                                true,
                                "开启后，会向 Among Us 隐藏你的唯一设备 ID，将来或许有助于绕过硬件封禁");

        noTelemetry = Config.Bind("MalumMenu.Privacy",
                                "NoTelemetry",
                                true,
                                "开启后，阻止 Among Us 通过 Unity Analytics 收集你的游戏数据并发送给 Innersloth");

        // adaptMaxStrength = Config.Bind("MalumMenu.Overload",
        //                         "AdaptMaxStrength",
        //                         18000,
        //                         new ConfigDescription(
        //                             "Maximum total number of RPCs sent during one overload cycle in AutoAdapt mode. Automatically divided between targets and reduced based on ping. IMPORTANT: Only goes from 1 to 100K RPCs",
        //                             new AcceptableValueRange<int>(1, 100000)
        //                         ));

        // adaptMaxCooldown = Config.Bind("MalumMenu.Overload",
        //                         "AdaptMaxCooldown",
        //                         1f,
        //                         new ConfigDescription(
        //                             "Maximum time (in seconds) for one full overload cycle to complete in AutoAdapt mode. Automatically distributed across targets (more targets = shorter delay per target). IMPORTANT: Only goes from 0s to 10s",
        //                             new AcceptableValueRange<float>(0f, 10f)
        //                         ));

        // attackLogDelay = Config.Bind("MalumMenu.Overload",
        //                         "AttackLogDelay",
        //                         2f,
        //                         "Minimum time (in seconds) between attack logs in normal (non-verbose) mode");

        // defaultStrength = Config.Bind("MalumMenu.Overload",
        //                         "DefaultStrength",
        //                         18000,
        //                         new ConfigDescription(
        //                             "Default number of malformed RPCs sent to each target during an overload cycle. Overridden if AutoAdapt mode is enabled. IMPORTANT: Only goes from 1 to 100K RPCs",
        //                             new AcceptableValueRange<int>(1, 100000)
        //                         ));

        // defaultCooldown = Config.Bind("MalumMenu.Overload",
        //                         "DefaultCooldown",
        //                         1f,
        //                         new ConfigDescription(
        //                             "Default cooldown (in seconds) between each target during an overload cycle. Overridden if AutoAdapt mode is enabled. IMPORTANT: Only goes from 0s to 10s",
        //                             new AcceptableValueRange<float>(0f, 10f)
        //                         ));

        // killSwitchLvl = Config.Bind("MalumMenu.Overload",
        //                         "DefaultKillSwitchLevel",
        //                         1,
        //                         new ConfigDescription(
        //                             "Default level used by kill switch. Each level adds 500 ms to the max allowed ping before overload stops. Helps avoid lagging / disconnects. IMPORTANT: Only goes from level 1 (500 ms) to 6 (3000 ms)",
        //                             new AcceptableValueRange<int>(1, 6)
        //                         ));

        // Enabled by default
        CheatToggles.unlockFeatures = true;
        CheatToggles.freeCosmetics = true;
        CheatToggles.avoidPenalties = true;

        // Enabled by default
        CheatToggles.olAutoAdapt = true;
        CheatToggles.olKillSwitch = true;
        CheatToggles.olAutoStop = true;
        CheatToggles.olAutoClear = true;
        CheatToggles.olLogStartStop = true;
        CheatToggles.olLogAttack = true;
        CheatToggles.olLogAddRemove = true;
        CheatToggles.olLogDisconnect = true;

        Harmony.PatchAll();

        // UI
        menuUI = AddComponent<MenuUI>();
        consoleUI = AddComponent<ConsoleUI>();
        doorsUI = AddComponent<DoorsUI>();
        tasksUI = AddComponent<TasksUI>();
        protectUI = AddComponent<ProtectUI>();
        // overloadUI = AddComponent<OverloadUI>();
        // rolesUI = AddComponent<RolesUI>();

        // Components
        keybindListener = AddComponent<KeybindListener>();

        // Disables Telemetry (haven't fully tested if it works, but according to Unity docs it should)
        if (noTelemetry.Value)
        {
            Analytics.enabled = false;
            Analytics.deviceStatsEnabled = false;
            PerformanceReporting.enabled = false;
        }

        // Create profile file if it is missing
        if (!File.Exists(ProfilePath))
        {
            CheatToggles.SaveTogglesToProfile();
        }

        // Auto load profile on start if needed
        if (autoLoadProfile.Value)
        {
            CheatToggles.LoadTogglesFromProfile();
        }

        SceneManager.add_sceneLoaded((Action<Scene, LoadSceneMode>) ((scene, _) =>
        {
            if (scene.name == "MainMenu" && !(inStealthMode || isPanicked))
            {
                // Warns about unsupported AU versions
                if (!supportedAU.Contains(Application.version))
                {
                    Utils.ShowPopup("\n当前 MalumMenu 版本与当前 Among Us 版本不兼容\n\n请安装匹配的版本，以免出问题");
                }
            }
        }));
    }
}
