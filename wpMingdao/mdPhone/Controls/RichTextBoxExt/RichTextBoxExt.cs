using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;

namespace mdPhone.Controls.RichTextBoxExt
{
    public class RichTextBoxExt : RichTextBox
    {
        #region 富文本Text

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text", typeof(string), typeof(RichTextBoxExt), new PropertyMetadata(default(string), TextChangedCallback));

        private static void TextChangedCallback(DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var richTextBox = (RichTextBoxExt)dependencyObject;

            var text = (string)dependencyPropertyChangedEventArgs.NewValue;
            var p = richTextBox.ConvertToElement(text);
            richTextBox.Blocks.Clear();
            richTextBox.Blocks.Add(p);
        }


        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        #endregion

        //1、由于RichTextBox的Xaml属性不支持图片，所以没办法直接通过RichTextBox的Xaml属性直接处理
        //      这里通过构造XAML并使用XamlReader进行读取转换达到富文本的目的
        //      富文本包括：文本，图片，链接三种元素
        //          我们只需要分别对图片和链接进行处理就可以
        /// <summary>
        /// 将文字转为富文本（文字+图片表情+链接）
        /// </summary>
        public Paragraph ConvertToElement(string input)
        {
            if (input == null)
            {
                return new Paragraph();
            }

            //链接
            var mc = Regex.Matches(input, @"http(s)://[\x21-\x7e-[\s]]+|http(s)://[\x21-\x7e-[\s]]+|http(s)://[\x21-\x7e-[\s]]+$");

            //记录是否重复
            var matchs = new List<string>();

            foreach (Match m in mc)
            {
                if (matchs.Contains(m.Value))
                {
                    //如果有重复匹配项，则跳过
                    continue;
                }

                //这里链接用蓝色显示，不加下划线（注意，这里使用系统的浏览器IE打开）
                input = input.Replace(m.Value.Substring(0, m.Value.Length),
                    string.Format(@"<Hyperlink NavigateUri=""{0}"" MouseOverTextDecorations=""None"" MouseOverForeground=""Blue"" Foreground=""#FF0D143E"" TargetName=""_blank"" >{0}</Hyperlink>",
                        m.Value));

                matchs.Add(m.Value);
            }
            matchs.Clear();

         

            //替换@人
            input = Regex.Replace(input, "###(.+?)###", (m) =>
            {
                string rPost = m.ToString().Replace("#", "");
                string userName = rPost.Split('|')[1];
                //return "@" + userName;

                return string.Format(@"<Span Foreground=""#FF0D143E"">@{0}</Span> ", userName);

                //return string.Format(@"<Hyperlink    Foreground=""#0066cc"">@{0}</Hyperlink>", userName);

            });

           // input = input.Replace("$$","%");

            //替换@
            input = Regex.Replace(input, "(\\${3})(.+?)(\\${3})", (m) =>
            {
                string rPost2 = m.ToString().Replace("$", "");
                string group = rPost2.Split('|')[1];
                //return "@" + userName;

                return string.Format(@"<Span Foreground=""#FF0D143E"" >@{0}</Span> ", group); 
            });

            //表情字典
            var dict = EmotionDictionary;

            //构造正则模式串（匹配表情）
            var builder = new StringBuilder();
            foreach (var key in dict.Keys)
            {
                builder.Append(key.Replace("[", @"\[").Replace("]", @"\]"));//.Replace("{", @"\{").Replace("}", @"\}"));
                builder.Append("|");
            }
            //定义一个Regex对象实例
            var r = new Regex(builder.ToString().Substring(0, builder.Length - 1));
            mc = r.Matches(input);
            foreach (Match m in mc)
            {
                //表情替换图片
                input = input.Replace(m.Value, string.Format(@"
                                <InlineUIContainer>
                                    <Border>
                                        <Image Source=""/Assets/Emotions/{0}"" Width=""30"" Height=""30""/>
                                    </Border>
                                </InlineUIContainer>
                    ", dict[m.Value]));
            }
            input = input.Replace("<End>","{End}").Replace("-","").Replace("&","and");

            var xaml = string.Format(@"<Paragraph 
                                        xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""
                                        xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml"">
                                    <Paragraph.Inlines>
                                    <Run></Run>
                                      {0}
                                    </Paragraph.Inlines>
                                </Paragraph>", input);

            return (Paragraph)XamlReader.Load(xaml);
        }

        #region 表情字典

        private static Dictionary<string, string> emotionDictionary;
        public static Dictionary<string, string> EmotionDictionary
        {
            get
            {
                if (emotionDictionary == null)
                {
                    emotionDictionary = new Dictionary<string, string>();
                    //可扩展图片 类型
                    var files = new[] { "md"};

                    foreach (var file in files)
                    {
                        using (var stream = Application.GetResourceStream(
                            new Uri(string.Format("Assets/Emotions/{0}.txt", file), UriKind.Relative)).Stream)
                        {
                            using (var reader = new StreamReader(stream))
                            {
                                var line = reader.ReadLine();
                                while (line != null)
                                {
                                    var res = line.Split(',');
                                    emotionDictionary.Add(res[1], res[0]);
                                    line = reader.ReadLine();
                                }
                            }
                        }
                    }
                }
                return emotionDictionary;
            }
        }

        #endregion
    }
}
