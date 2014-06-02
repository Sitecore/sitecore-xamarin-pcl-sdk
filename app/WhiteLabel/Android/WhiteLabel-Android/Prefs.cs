namespace WhiteLabelAndroid
{
    using Android.Content;
    using Android.Preferences;

    public class Prefs
    {
        private readonly ISharedPreferences prefs;

        private Prefs(ISharedPreferences sharedPreferences)
        {
            this.prefs = sharedPreferences;
        }

        public static Prefs From(Context context)
        {
            return new Prefs(PreferenceManager.GetDefaultSharedPreferences(context));
        }

        public void PutString(string key, string value)
        {
            ISharedPreferencesEditor editor = this.prefs.Edit();
            editor.PutString(key, value);
            editor.Apply();
        }

        public string GetString(string key, string defaultValue)
        {
            return this.prefs.GetString(key, defaultValue);
        }
    }
}