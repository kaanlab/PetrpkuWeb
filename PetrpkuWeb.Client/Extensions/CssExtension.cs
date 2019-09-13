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
            switch (article.Type)
            {
                case Article.Style.Warning: return "text-warning";
                case Article.Style.Danger: return "text-danger";
                case Article.Style.Info: return "text-info";
            }

            return String.Empty;
        }

        public static string Border(Article article)
        {
            switch (article.Type)
            {
                case Article.Style.Warning: return "border-warning";
                case Article.Style.Danger: return "border-danger";
                case Article.Style.Info: return "border-info";
            }

            return String.Empty;
        }

        public static string Img(string extension)
        {
            switch (extension)
            {
                case ".doc":
                case ".docx":
                    return @"/img/site/word.png";
                case ".xls":
                case ".xlsx":
                    return @"/img/site/excel.png";
                case ".pdf":
                    return @"/img/site/pdf.png";
                case ".rtf":
                    return @"/img/site/rtf.png";
            }

            return String.Empty;
        }
    }
}
