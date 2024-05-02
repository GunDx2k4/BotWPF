using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotWPF.Bot.Core
{
    public class MarkdownDiscord
    {
        public static string ItalicsText(string text)
        {
            return $"*{text}*";
        }

        public static string UnderlineText(string text)
        {
            return $"_{text}_";
        }

        public static string BoldText(string text)
        {
            return $"**{text}**";
        }

        public static string StrikethroughText(string text)
        {
            return $"~~{text}~~";
        }

        public static string SpoilerhText(string text)
        {
            return $"|{text}|";
        }

        public static string HeadersText(string text, int level = 1)
        {
            switch (level)
            {
                case 1:
                    return $"# {text}";
                case 2:
                    return $"## {text}";
                case 3:
                    return $"### {text}";
                default:
                    return $"{text}";
            }
        }

        public static string MaskedLinks(string text, string link)
        {
            return $"[{text}]({link})";
        }

        public static string HighlightText(string text)
        {
            return $"`{text}`";
        }

        public static string CodeBlock(string text)
        {
            return $"```{text}```";
        }

        public static string BlockQuotes(string text)
        {
            return $"> {text}";
        }

        public static string BlockQuotesAll(string text)
        {
            return $">>> {text}";
        }
    }
}
