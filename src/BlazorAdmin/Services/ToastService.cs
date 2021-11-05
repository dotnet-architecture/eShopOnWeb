using System;
using System.Timers;

namespace BlazorAdmin.Services;

public enum ToastLevel
{
    Info,
    Success,
    Warning,
    Error
}

public class ToastService : IDisposable
{
    public event Action<string, ToastLevel> OnShow;
    public event Action OnHide;
    private Timer Countdown;
    public void ShowToast(string message, ToastLevel level)
    {
        OnShow?.Invoke(message, level);
        StartCountdown();
    }
    private void StartCountdown()
    {
        SetCountdown();
        if (Countdown.Enabled)
        {
            Countdown.Stop();
            Countdown.Start();
        }
        else
        {
            Countdown.Start();
        }
    }
    private void SetCountdown()
    {
        if (Countdown == null)
        {
            Countdown = new Timer(3000);
            Countdown.Elapsed += HideToast;
            Countdown.AutoReset = false;
        }
    }
    private void HideToast(object source, ElapsedEventArgs args)
    {
        OnHide?.Invoke();
    }
    public void Dispose()
    {
        Countdown?.Dispose();
    }
}
