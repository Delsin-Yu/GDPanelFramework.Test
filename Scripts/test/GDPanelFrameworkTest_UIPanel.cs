using GdUnit4;
using System.Threading.Tasks;
using Godot;
using GodotTask;

namespace GDPanelFramework.Tests;

[TestSuite]
public class GDPanelFrameworkTest_UIPanel
{
    // [TestCase]
    // public static async Task WaitForDebugger()
    // {
    //     await GDTask.WaitUntil(() => System.Diagnostics.Debugger.IsAttached);
    // }

    [TestCase]
    public static async Task UIPanel_Test_EventFunction()
    {
        await GDTask.NextFrame();

        var resource = GD.Load<PackedScene>("res://Prefabs/UIPanel_EventFunctionTest.tscn");

        var monitor = new UIPanel_EventFunctionTest.TestMonitor();

        await resource
            .CreatePanel<UIPanel_EventFunctionTest>(initializeCallback: panel => panel.Monitor = monitor)
            .OpenPanelAsync(closePolicy: ClosePolicy.Delete);

        await GDTask.NextFrame();

        Assertions.AssertThat(monitor.Initialized).IsTrue();
        Assertions.AssertThat(monitor.Opened).IsTrue();
        Assertions.AssertThat(monitor.Closed).IsTrue();
        Assertions.AssertThat(monitor.Predelete).IsTrue();
        Assertions.AssertThat(monitor.Notification).IsTrue();
    }

    [TestCase]
    public static async Task UIPanelArg_Test_EventFunction()
    {
        var resource = GD.Load<PackedScene>("res://Prefabs/UIPanelArg_EventFunctionTest.tscn");

        var monitor = new UIPanelArg_EventFunctionTest.TestMonitor();

        const int openArg = 10;

        var closeValue = await resource
            .CreatePanel<UIPanelArg_EventFunctionTest>(initializeCallback: panel => panel.Monitor = monitor)
            .OpenPanelAsync(openArg, closePolicy: ClosePolicy.Delete);

        await GDTask.NextFrame();

        Assertions.AssertThat(monitor.Initialized).IsTrue();
        Assertions.AssertThat(monitor.Opened).IsTrue();
        Assertions.AssertThat(monitor.Closed).IsTrue();
        Assertions.AssertThat(monitor.Predelete).IsTrue();
        Assertions.AssertThat(monitor.Notification).IsTrue();

        Assertions.AssertThat(monitor.OpenValue).IsEqual(openArg);
        Assertions.AssertThat(monitor.CloseValue).IsEqual(openArg * 2);
        Assertions.AssertThat(monitor.CloseValue).IsEqual(closeValue);
    }

    [TestCase]
    public static async Task UIPanel_Test_Token()
    {
        await GDTask.NextFrame();

        var resource = GD.Load<PackedScene>("res://Prefabs/UIPanel_TokenTest.tscn");

        var monitor = new UIPanel_TokenTest.TestMonitor();

        await resource
            .CreatePanel<UIPanel_TokenTest>(initializeCallback: panel => panel.Monitor = monitor)
            .OpenPanelAsync(closePolicy: ClosePolicy.Delete);

        await GDTask.NextFrame();

        Assertions.AssertThat(monitor.PanelCloseTokenCanceled).IsTrue();
        Assertions.AssertThat(monitor.PanelCloseTweenFinishTokenCanceled).IsTrue();
        Assertions.AssertThat(monitor.PanelOpenTweenFinishTokenCanceled).IsTrue();
    }

    [TestCase]
    public static async Task UIPanelArg_Test_Token()
    {
        var resource = GD.Load<PackedScene>("res://Prefabs/UIPanelArg_TokenTest.tscn");

        var monitor = new UIPanelArg_TokenTest.TestMonitor();

        const int openArg = 10;

        var closeValue = await resource
            .CreatePanel<UIPanelArg_TokenTest>(initializeCallback: panel => panel.Monitor = monitor)
            .OpenPanelAsync(openArg, closePolicy: ClosePolicy.Delete);

        await GDTask.NextFrame();

        Assertions.AssertThat(closeValue).IsEqual(closeValue);
        
        Assertions.AssertThat(monitor.PanelCloseTokenCanceled).IsTrue();
        Assertions.AssertThat(monitor.PanelCloseTweenFinishTokenCanceled).IsTrue();
        Assertions.AssertThat(monitor.PanelOpenTweenFinishTokenCanceled).IsTrue();
    }
}