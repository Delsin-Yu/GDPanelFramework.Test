using GDPanelFramework.Panels;
using GodotTask;

namespace GDPanelFramework.Tests;

public partial class UIPanelArg_EventFunctionTest : UIPanelArg<int, int>
{
    public class TestMonitor
    {
        public int OpenValue { get; set; }
        public int CloseValue { get; set; }
        public bool Initialized { get; set; }
        public bool Opened { get; set; }
        public bool Closed { get; set; }
        public bool Predelete { get; set; }
        public bool Notification { get; set; }
    }
    
    public TestMonitor Monitor { get; set; }
    
    protected override void _OnPanelInitialize() => Monitor.Initialized = true;

    protected override void _OnPanelOpen(int value)
    {
        Monitor.Opened = true;
        Monitor.OpenValue = value;
        GDTask.NextFrame().ContinueWith(ClosePanelWithValue);
    }

    private void ClosePanelWithValue() => ClosePanel(OpenArg * 2);

    protected override void _OnPanelClose(int value)
    {
        Monitor.Closed = true;
        Monitor.CloseValue = value;
    }

    protected override void _OnPanelPredelete() => Monitor.Predelete = true;

    protected override void _OnPanelNotification(int what)
    {
        if (Monitor != null) Monitor.Notification = true;
    }
}