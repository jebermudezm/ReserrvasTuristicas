namespace RSI.Mvc.Web.Controllers.Helper
{
    public interface IGlobalCachingProvider
    {
        void AddItem(string key, object value);
        object GetItem(string key);
        object GetItem(string key, bool remove);
    }
}