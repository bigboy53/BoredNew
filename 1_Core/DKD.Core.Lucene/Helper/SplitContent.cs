using System;
using System.Collections.Generic;
using System.Text;
using Lucene.Net.Analysis.PanGu;
using PanGu;
using PanGu.HighLight;

namespace DKD.Core.Lucene
{
    public class SplitContent
    {
        public static string SplitWords(string keywords, PanGuTokenizer ktTokenizer)
        {
            var result = new StringBuilder();
            ICollection<WordInfo> words = ktTokenizer.SegmentToWordInfos(keywords);
            foreach (WordInfo word in words)
            {
                if (word == null)
                {
                    continue;
                }
                result.AppendFormat("{0}^{1}.0 ", word.Word, (int)Math.Pow(3, word.Rank));
            }
            return result.ToString().Trim();
        }

        public static string HightLight(string keyword, string content)
        {
            //创建HTMLFormatter,参数为高亮单词的前后缀
            var simpleHtmlFormatter = new SimpleHTMLFormatter("<font style=\"font-style:normal;color:#cc0000;\"><b>", "</b></font>");
            //创建 Highlighter ，输入HTMLFormatter 和 盘古分词对象Semgent
            var highlighter = new Highlighter(simpleHtmlFormatter, new Segment());
            //设置每个摘要段的字符数
            highlighter.FragmentSize = 1000;
            //获取最匹配的摘要段
            return highlighter.GetBestFragment(keyword, content);
        }
    }
}
