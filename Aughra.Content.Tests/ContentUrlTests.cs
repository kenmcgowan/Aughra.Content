using System;
using System.Collections.Generic;
using Xunit;

namespace Aughra.Content.Tests
{
    public sealed class ContentUrlTests
    {
        #region test data sources
        public static IEnumerable<object[]> Parameters_InvalidContentUrl
        {
            get
            {
                yield return new object[] { (string)null };
                yield return new object[] { "" };
                yield return new object[] { "\t   \r\n" };
                yield return new object[] { "http://you-might-think-this-would-work-but-it-shouldnt" };
                yield return new object[] { "ftp://yeah-dont-even-bother" };
                yield return new object[] { "gopher://does-anyone-still-use-this" };
                yield return new object[] { "https:/hey-wheres-the-other-slash" };
                yield return new object[] { "https://?wheres-the-rest" };
                yield return new object[] { "https://#just-a-fragment" };
                yield return new object[] { "https://kenmcgowan.com#no-fragments-please" };
                yield return new object[] { "https://someuser@someother.com/no-user-names-please" };
                yield return new object[] { "https://someuser:somepassword@yetanother.com/no-user-names-especially-not-with-passwords-sheesh" };
            }
        }

        public static IEnumerable<object[]> Parameters_ValidContentUrl
        {
            get
            {
                yield return new object[] { "https://some.com" };
                yield return new object[] { "https://some-other.com/" };
                yield return new object[] { "https://content-place.net/index.html" };
                yield return new object[] { "https://blam.co/index.html?name=something" };
                yield return new object[] { "https://itsa-me-mar.io/index.html?name=something else" };
                yield return new object[] { "https://gamepalace.com/index.html?name=something%20else%20yet" };
                yield return new object[] { "https://some.tld.com/index.html" };
                yield return new object[] { "https://some.over.wrought.host.name.biz/index.html" };
                yield return new object[] { "https://sod-off.co.uk" };
            }
        }
        #endregion

        [Theory]
        [MemberData(nameof(ContentUrlTests.Parameters_InvalidContentUrl))]
        public void IsValidContentUrl_InvalidContentUrl_ReturnsFalse(string url)
        {
            var contentUrlIsValid = ContentUrl.IsValidContentUrl(url);
            Assert.False(contentUrlIsValid);
        }

        [Theory]
        [MemberData(nameof(ContentUrlTests.Parameters_ValidContentUrl))]
        public void IsValidContentUrl_ValidUrl_ReturnsTrue(string url)
        {
            var contentUrlIsValid = ContentUrl.IsValidContentUrl(url);
            Assert.True(contentUrlIsValid);
        }

        [Theory]
        [MemberData(nameof(ContentUrlTests.Parameters_InvalidContentUrl))]
        public void ContentUrlCtor_InvalidUrl_ThrowsArgumentException(string url)
        {
            Assert.Throws<ArgumentException>("url", () =>
            {
                new ContentUrl(url);
            });
        }

        [Theory]
        [MemberData(nameof(ContentUrlTests.Parameters_ValidContentUrl))]
        public void ContentUrlCtor_ValidUrl_ReturnsSuccessfully(string url)
        {
            var contentUrl = new ContentUrl(url);
            Assert.Equal(url, contentUrl.Url);
        }

        [Theory]
        [MemberData(nameof(ContentUrlTests.Parameters_ValidContentUrl))]
        public void ToString_ValidUrl_ReturnsOriginalUrl(string url)
        {
            var contentUrl = new ContentUrl(url);
            Assert.Equal(url, contentUrl.ToString());
        }
    }
}
