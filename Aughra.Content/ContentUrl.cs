using System;
using System.Collections.Generic;
using System.Text;

namespace Aughra.Content
{
    public sealed class ContentUrl
    {
        public ContentUrl(string url)
        {
        }

        public static bool IsValidContentUrl(string url)
        {
            // This is busted, tests that prove that are forthcoming…
            return !string.IsNullOrWhiteSpace(url);
        }
    }
}
