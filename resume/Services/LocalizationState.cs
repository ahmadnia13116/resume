using System.Globalization;
using System.Resources;

public class LocalizationService
{
    private readonly ResourceManager _resourceManager;

    public event Action? OnChange;

    public string CurrentCulture { get; private set; } = "en-US";

    public LocalizationService()
    {
        _resourceManager = new ResourceManager(
            "BlazorApp1.Resources.SharedResource",
            typeof(LocalizationService).Assembly);
    }

    public void SetCulture(string culture)
    {
        CurrentCulture = culture;
        var newCulture = new CultureInfo(culture);

        // 🔥 مهم: تغییر فرهنگ برای ترد فعلی (برای آپدیت آنی UI)
        CultureInfo.CurrentCulture = newCulture;
        CultureInfo.CurrentUICulture = newCulture;

        // تغییر فرهنگ پیش‌فرض برای تردهای آینده
        CultureInfo.DefaultThreadCurrentCulture = newCulture;
        CultureInfo.DefaultThreadCurrentUICulture = newCulture;

        // اطلاع به کامپوننت‌ها برای رندر مجدد
        OnChange?.Invoke();
    }

    public string this[string key]
    {
        get
        {
            return _resourceManager.GetString(key, new CultureInfo(CurrentCulture)) ?? key;
        }
    }
}