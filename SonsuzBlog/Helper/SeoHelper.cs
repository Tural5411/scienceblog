using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace SonsuzBlog.Helper
{
    public class SeoHelper
    {
        public static string ToSeoUrl(string IncomingText)
        {
            IncomingText = IncomingText.Replace("Ş", "s");
            IncomingText = IncomingText.Replace("ş", "s");
            IncomingText = IncomingText.Replace("İ", "i");
            IncomingText = IncomingText.Replace("I", "i");
            IncomingText = IncomingText.Replace("ə", "e");
            IncomingText = IncomingText.Replace("Ə", "e");
            IncomingText = IncomingText.Replace("ı", "i");
            IncomingText = IncomingText.Replace("ö", "o");
            IncomingText = IncomingText.Replace("Ö", "o");
            IncomingText = IncomingText.Replace("ü", "u");
            IncomingText = IncomingText.Replace("Ü", "u");
            IncomingText = IncomingText.Replace("Ç", "c");
            IncomingText = IncomingText.Replace("ç", "c");
            IncomingText = IncomingText.Replace("ğ", "g");
            IncomingText = IncomingText.Replace("Ğ", "g");
            IncomingText = IncomingText.Replace(" ", "-");
            IncomingText = IncomingText.Replace("---", "-");
            IncomingText = IncomingText.Replace("?", "");
            IncomingText = IncomingText.Replace("/", "");
            IncomingText = IncomingText.Replace(".", "");
            IncomingText = IncomingText.Replace("'", "");
            IncomingText = IncomingText.Replace("#", "");
            IncomingText = IncomingText.Replace("%", "");
            IncomingText = IncomingText.Replace("&", "");
            IncomingText = IncomingText.Replace("*", "");
            IncomingText = IncomingText.Replace("!", "");
            IncomingText = IncomingText.Replace("@", "");
            IncomingText = IncomingText.Replace("+", "");

            IncomingText = IncomingText.ToLower();
            IncomingText = IncomingText.Trim();

            // butun herifleri kiçilt
            string encodedUrl = (IncomingText ?? "").ToLower();

            // & ile " " yer deyistirmek
            encodedUrl = Regex.Replace(encodedUrl, @"&+", "and");

            // " " karakterlerini silmek
            encodedUrl = encodedUrl.Replace("'", "");

            // lazimsiz karakterleri sil
            encodedUrl = Regex.Replace(encodedUrl, @"[^a-z0-9]", "-");

            // tekrar edenleri sil
            encodedUrl = Regex.Replace(encodedUrl, @"-+", "-");

            // karakterlerin arasina tire qoy
            encodedUrl = encodedUrl.Trim('-');

            return encodedUrl;
        }
    }
}