using System;
using System.Globalization;

namespace Aughra.Content
{
    // Design notes:
    //
    // This type defines all the various rules around what constitutes a valid content
    // URL. The static method can be used to test the validity of a potential content URL,
    // or an instance can be created, enforcing content URL rules at construction and
    // avoiding having to validate and re-validate a particulare URL.
    //
    // The type should probably also store content URL's in a normalized form. Wait until later
    // to add that functionality, since it's not clear yet how or when it might be used.

    public sealed class ContentUrl
    {
        public ContentUrl(string url)
        {
            if (!ContentUrl.IsValidContentUrl(url))
            {
                throw new ArgumentException("Invalid content URL", nameof(url));
            }

            this.Url = url;
        }

        public string Url
        {
            get;
            private set;
        }

        public override string ToString()
        {
            return this.Url;
        }

        public static bool IsValidContentUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                return false;
            }

            Uri uri;
            if (!Uri.TryCreate(url, UriKind.Absolute, out uri))
            {
                return false;
            }

            return
                (uri.Fragment.Length == 0) &&
                (uri.UserInfo.Length == 0) &&
                (string.Compare(uri.Scheme, "https", ignoreCase: true, culture: CultureInfo.InvariantCulture) == 0);
        }
    }
}
