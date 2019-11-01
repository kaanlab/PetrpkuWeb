using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetrpkuWeb.Client.Extensions
{
    public static class StringExtension
    {
        public const string ACCEPT_DOC = ".doc,.docx,application/msword,application/vnd.openxmlformats-officedocument.wordprocessingml.document";
        public const string ACCEPT_XLS = ".xls,.xlsx,application/vnd.openxmlformats-officedocument.spreadsheetml.sheet,application/vnd.ms-excel";
        public const string ACCEPT_PDF = ".pdf,application/pdf";
        public const string ACCEPT_PPT = ".pptx,.ppt,.pps,application/vnd.ms-powerpoint,application/vnd.openxmlformats-officedocument.presentationml.presentation";
        public const string ACCEPT_RTF = ".rtf,application/rtf";
        public const string ACCEPT_JPG = ".jpeg,.jpg,image/jpeg";
        public const string ACCEPT_PNG = ".png,image/png";

        public const string ACCEPT_IMAGES = ACCEPT_JPG + "," + ACCEPT_PNG;
        public const string ACCEPT_ALL_FILES = ACCEPT_DOC + "," + ACCEPT_XLS + "," + ACCEPT_PDF + "," + ACCEPT_PPT + "," + ACCEPT_RTF + "," + ACCEPT_IMAGES;
        
        public static string Truncate(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength) + "...";
        }
    }
}
