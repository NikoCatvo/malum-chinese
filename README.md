# MalumMenu 3.3.1 汉化版

<div align="center">
  <img src="https://scp222thj.dev/static/images/malumLogo.png" alt="MalumMenu"/>
</div>

> **汉化 by 微生猫**

MalumMenu 是一个基于 [BepInEx 6](https://github.com/BepInEx/BepInEx) 的 Among Us 客户端菜单模组，把移动、透视、职业增强、破坏、聊天、动画、控制台日志、房主工具等功能整合进一个游戏内 GUI。

本仓库是基于上游 [scp222thj/MalumMenu](https://github.com/scp222thj/MalumMenu) **v3.3.1** 的**简体中文汉化版本**，对应游戏版本 **Among Us 2026.9.29（v19）**。

## 汉化改动

- **界面全中文**：菜单标题、11 个标签页、所有开关与按钮、5 个子窗口，共 214 条界面字符串
- **中文字体切换内置**：IMGUI 默认字体是 Unity 内置 Arial，不含汉字。新增 `UI/Utilities/MalumFont.cs`，在菜单构建样式前把 `GUI.skin.font` 换成系统中文动态字体，失败静默跳过
- **两处汉化署名**：菜单标题栏 `MalumMenu v3.3.1 · 汉化 微生猫`，菜单左下角 `汉化 by 微生猫`
- **配置说明汉化**：`MalumMenu.cfg` 里 11 条设置项描述改中文，**键名保持英文**，不会破坏已有配置
- **游戏内通知与日志汉化**：地图缺少系统提示、大厅击杀禁用提示、房间代码已复制、控制台里的击杀 / 管道 / 变形 / 任务日志
- **术语统一**：Role → 职业、Vent → 管道、Eject → 逐出、Lobby → 大厅

## 安装

1. 给 Among Us 安装 **BepInEx 6（IL2CPP，x64）**，确认游戏能正常启动、`BepInEx/plugins` 目录已生成
2. 删除 `BepInEx/plugins` 里旧的 `MalumMenu.dll`（原版或旧汉化版），避免同一个 GUID 冲突
3. 把 [Releases](https://github.com/NikoCatvo/malum-chinese/releases/latest) 里的 `MalumMenu.dll` 放进 `BepInEx/plugins`
4. 启动游戏，进大厅后按 `Delete` 开关菜单

### 改键位

配置文件在 `BepInEx/config/MalumMenu.cfg`：

```ini
[MalumMenu.GUI]

## The keyboard key used to toggle the GUI on and off.
# 用于开关菜单界面的按键。
# Setting type: String
# Default value: Delete
Keybind = Delete
```

把 `Delete` 换成任意 Unity KeyCode 名称，例如 `F1`、`Insert`、`RightControl`。

> 中文显示成「口口口」方块，说明系统里没有可用中文字体。模组会依次尝试 `Microsoft YaHei` / `微软雅黑` / `SimHei` / `黑体` / `Noto Sans CJK SC` / `PingFang SC` / `Arial Unicode MS`，Windows 上一般命中前两个。

### 菜单打不开

1. 确认游戏版本是 **2026.9.29**。版本不匹配时模组会弹窗提示「当前 MalumMenu 版本与当前 Among Us 版本不兼容」
2. 确认 `BepInEx/plugins` 里只有一个 `MalumMenu.dll`
3. 看 `BepInEx/LogOutput.log`，搜 `MalumMenu`，插件没加载会有明确报错
4. 换回上游原版对比：原版也打不开 → 与汉化无关，是游戏版本或 BepInEx 的问题

## 功能

| 标签 | 内容 |
| --- | --- |
| 移动 | 移速、穿墙、反转操作、传送到光标 / 玩家 |
| 透视 | 查看玩家信息 / 职业 / 幽灵、无阴影、任务箭头、公开票数、大厅信息、相机缩放 / 观战 / 自由相机、连线（船员 / 内鬼 / 幽灵 / 尸体 / 按颜色 / 按距离）、小地图 |
| 职业 | 伪造职业与存活、内鬼击杀距离、变形者动画与时长、船员任务菜单、追踪者、工程师、科学家、侦探 |
| 飞船 | 无法修复的灯光、召集 / 关闭会议、自动报告尸体、自动开门、破坏（反应堆 / 氧气 / 灯光 / 通信 / 蘑菇混战 / 孢子 / 破坏地图）、门菜单、管道解锁与踢出 |
| 聊天 | 启用聊天、绕过链接屏蔽、降低发送限制、解锁额外字符、更长消息、解锁剪贴板 |
| 动画 | 护盾、陨石、倒垃圾、医疗扫描、监控使用中、太空步 |
| 控制台 | 显示控制台、记录死亡 / 变形 / 管道 / 任务 / 游戏状态、清空与复制日志 |
| 房主专用 | 隐身击杀、击杀任何人、无击杀冷却、保护菜单、击杀玩家 / 传送击杀 / 击杀所有船员 / 内鬼 / 所有人、强制开始、不结束游戏、跳过会议、免疫投票、逐出玩家 |
| 被动 | 免费装扮、避免处罚、解锁额外功能、断线复制房间代码、伪装日期为 4 月 1 日 |
| 模式 | RGB 模式、隐身模式、紧急模式 |
| 配置 | 打开 / 重载配置、保存到档案 / 从档案加载 |

> 部分功能需要你是房主，或依赖当前游戏版本，或仅适用于私有房间。

## 热键

| 按键 | 功能 |
| --- | --- |
| `Delete` | 打开 / 关闭菜单（可在 `MalumMenu.cfg` 改） |

## 编译

需要 .NET SDK 8。`AmongUs` 必须指向游戏目录，里面要有 BepInEx 6 (IL2CPP) 生成的 interop 参考程序集：

```bash
dotnet build src/MalumMenu.csproj -c Release -p:AmongUs="/path/to/Among Us"
```

产物在 `src/bin/Release/net6.0/MalumMenu.dll`，构建脚本会自动复制到 `BepInEx/plugins/`。

`AmongUs` 指向的目录下必须有 `BepInEx/interop/`，跑过一次 BepInEx 就会自动生成。上游工程原本依赖 `AmongUs.GameLibs.Steam` 这个纯托管 dummy 包，在 macOS / Linux 上编译 IL2CPP 模组会缺引用，所以这里改成走真实 interop。

## 汉化改动清单

| 文件 | 改动 |
| --- | --- |
| `src/UI/Utilities/MalumFont.cs` | **新增**。系统中文动态字体切换，失败静默降级 |
| `src/UI/Utilities/GUIStylePreset.cs` | 新增 `Credit` 样式（11px、居中、橙色），用于左下角署名 |
| `src/UI/Windows/MenuUI.cs` | `InitStyles()` 调 `MalumFont.Apply()`；标题栏加署名；`WindowFunction()` 末尾画左下角署名 |
| `src/UI/Windows/Tabs/*.cs` | 11 个标签页的标题、开关、按钮文本 |
| `src/UI/Windows/{Doors,Console,Roles,Tasks,Protect}UI.cs` | 5 个子窗口的标题与文本 |
| `src/Patches/*.cs` | 控制台日志、版本不兼容弹窗、房间代码复制提示 |
| `src/Cheats/*.cs` | 游戏内通知（地图缺少系统、大厅击杀禁用） |
| `src/MalumMenu.cs` | 11 条配置项描述汉化（键名不动） |

### 修掉的一处汉化副作用

`TasksUI` 原本用 `taskText.Contains("You are dead")` 过滤游戏生成的任务文本。这段文本由游戏自己的 `TranslationController` 按游戏语言生成——汉化时如果直接把判断条件也翻掉，英文游戏下过滤就失效了。

改成中英文两种都判：

```csharp
if (taskText.Contains("You are dead") || taskText.Contains("你已死亡")) continue;
if (taskText.Contains("Sabotage and kill") || taskText.Contains("破坏并击杀")) continue;
```

## 有意**没有**翻译的内容

| 内容 | 原因 |
| --- | --- |
| 职业名（`Impostor` / `Crewmate` / `Tracker` …） | 走游戏自带 `TranslationController.GetString()`，会跟随游戏语言，硬编码反而和游戏内不一致 |
| 平台名（`Steam` / `Epic Games` / `iPhone` …） | 要和游戏原生显示一致，否则伪造平台功能对不上 |
| `STRMISS` | `TranslationController` 的缺失哨兵字符串 |
| `"Background"` | `popup.transform.Find("Background")` 的 Unity 对象名，不是显示文本 |
| `MainMenu` / `MatchMaking` | 场景名，`SceneManager.LoadScene()` 用的 |
| `Mouse ScrollWheel` | Input 轴名 |
| `OverloadTab` / `OverloadUI` | 上游已注释掉的死代码（`// _tabs.Add(new OverloadTab());`），运行时不可达 |

## 免责

- 本汉化基于 **GPL-3.0** 发布，与原项目同协议。原项目：https://github.com/scp222thj/MalumMenu
- 这是**作弊模组**。在公开房间使用会被举报、封号，**风险自负**。
- 只在**自己开的房 / 和朋友玩的私房**里用。
- 汉化只改显示文本，没有改动任何作弊逻辑，也没有加任何联网、上报、后门代码。
- 原项目作者：**scp222thj**、**astra1dev**。出问题请先确认原版能不能跑，再找汉化作者。

---

**汉化：微生猫** · [GitHub](https://github.com/NikoCatvo) · [Telegram](https://t.me/catvo) · mikucatovo@gmail.com
