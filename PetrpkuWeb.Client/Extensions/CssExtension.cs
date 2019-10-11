using PetrpkuWeb.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetrpkuWeb.Client.Extensions
{
    public static class CssExtension
    {
        public static string Text(Article article)
        {
            return article.Type switch
            {
                Article.Style.Warning => "text-warning",
                Article.Style.Danger => "text-danger",
                Article.Style.Info => "text-info",

                _ => String.Empty,
            };
        }

        public static string Border(Article article)
        {
            return article.Type switch
            {
                Article.Style.Warning => "border-warning",
                Article.Style.Danger => "border-danger",
                Article.Style.Info => "border-info",

                _ => String.Empty,
            };
        }

        public static string Img(string extension)
        {
            switch (extension.ToLower())
            {
                case ".doc":
                case ".docx":
                case "application/msword":
                case "application/vnd.openxmlformats-officedocument.wordprocessingml.document":
                    return @"/img/site/word.png";
                case ".xls":
                case ".xlsx":
                case "application/vnd.ms-excel":
                    return @"/img/site/excel.png";
                case ".pdf":
                case "application/pdf":
                    return @"/img/site/pdf.png";
                case ".rtf":
                    return @"/img/site/rtf.png";
                case ".jpeg":
                case ".jpg":
                case "image/jpeg":
                    return @"/img/site/jpeg.png";
                case ".png":
                case "image/png":
                    return @"/img/site/png.png";
                case ".pptx":
                case ".ppt":
                case ".pps":
                case "application/vnd.ms-powerpoint":
                case "application/vnd.openxmlformats-officedocument.presentationml.presentation":
                    return @"/img/site/pptx.png";
            }
            return String.Empty;
        }
    }
}
