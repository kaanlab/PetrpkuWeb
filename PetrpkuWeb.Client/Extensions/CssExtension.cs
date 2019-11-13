using PetrpkuWeb.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetrpkuWeb.Client.Extensions
{
    public static class CssExtension
    {
        //public static string Text(Article article)
        //{
        //    return article.Type switch
        //    {
        //        Article.Style.Warning => "text-warning",
        //        Article.Style.Danger => "text-danger",
        //        Article.Style.Info => "text-info",

        //        _ => String.Empty,
        //    };
        //}

        //public static string Border(Article article)
        //{
        //    return article.Type switch
        //    {
        //        Article.Style.Warning => "border-warning",
        //        Article.Style.Danger => "border-danger",
        //        Article.Style.Info => "border-info",

        //        _ => String.Empty,
        //    };
        //}

        public static string Img(string extension)
        {
            var extentionImg = String.Empty;

            switch (extension.ToLower())
            {
                case ".doc":
                case ".docx":
                case "application/msword":
                case "application/vnd.openxmlformats-officedocument.wordprocessingml.document":
                    extentionImg = @"/img/site/word.png";
                    break;
                case ".xls":
                case ".xlsx":
                case "application/vnd.ms-excel":
                case "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet":
                    extentionImg = @"/img/site/excel.png";
                    break;
                case ".pdf":
                case "application/pdf":
                    extentionImg = @"/img/site/pdf.png";
                    break;
                case ".rtf":
                case "application/rtf":
                    extentionImg = @"/img/site/rtf.png";
                    break;
                case ".jpeg":
                case ".jpg":
                case "image/jpeg":
                    extentionImg = @"/img/site/jpeg.png";
                    break;
                case ".png":
                case "image/png":
                    extentionImg = @"/img/site/png.png";
                    break;
                case ".pptx":
                case ".ppt":
                case ".pps":
                case "application/vnd.ms-powerpoint":
                case "application/vnd.openxmlformats-officedocument.presentationml.presentation":
                    extentionImg = @"/img/site/pptx.png";
                    break;
            }

            return extentionImg;
        }
    }
}
