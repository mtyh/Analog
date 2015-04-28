using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Analog.BLL;
using Winista.Text.HtmlParser;
using Winista.Text.HtmlParser.Lex;
using Winista.Text.HtmlParser.Util;

namespace Analog
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string postData = "__VIEWSTATE=dDwtMTM2MTgxNTk4OTs7PkB6%2BSk5sY52cPSnKtSRXI8QsKFr&TextBox1=2011060701309&TextBox2=13434034377feiyu&ddl_js=%D1%A7%C9%FA&Button1=+%B5%C7+%C2%BC+";
            BasicInfo logInfo = new BasicInfo("http://" + Service.Config.Host + "/default3.aspx", "post", postData, null);
            logInfo.Request();

            if (logInfo.Status)
            {
                Service.isLog(logInfo.PageBody);
                String s = Service.Config.Scores;
                String s1 = Service.Config.Log;
                String s2 = Service.Config.Main;

                BasicInfo logInfo2 = new BasicInfo("http://" + Service.Config.Host + "/xscjcx.aspx?xh=2011060701309", "post", "", logInfo.Collection);
                logInfo2.Request();


                #region 分析网页html节点
                Lexer lexer = new Lexer(logInfo2.PageBody);
                Parser parser = new Parser(lexer);
                NodeList htmlNodes = parser.Parse(null);
                this.treeView1.Nodes.Clear();
                this.treeView1.Nodes.Add("root");

                IList list=new ArrayList();

                TreeNode treeRoot = this.treeView1.Nodes[0];
                for (int i = 0; i < htmlNodes.Count; i++)
                {
                    this.RecursionHtmlNode(list,treeRoot, htmlNodes[i], false);
                }

                #endregion
            }
        }



        private void RecursionHtmlNode(IList list,TreeNode treeNode, INode htmlNode, bool siblingRequired)
        {
            if (htmlNode == null || treeNode == null) return;

            TreeNode current = treeNode;
            TreeNode content;
            //current node
            if (htmlNode is ITag)
            {
                ITag tag = (htmlNode as ITag);
                if (!tag.IsEndTag())
                {
                    string nodeString = tag.TagName;
                    Hashtable hash=new Hashtable();
                    if (tag.Attributes != null && tag.Attributes.Count > 0)
                    {
                        hash.Add(tag.TagName, tag.Attributes);
                        if (tag.Attributes["ID"] != null)
                        {
                            nodeString = nodeString + " { id=\"" + tag.Attributes["ID"].ToString() + "\" }";

                            if (tag.Attributes["ID"].ToString() == "Label1")
                            {
                                string s="";
                            }
                        }
                        if (tag.Attributes["HREF"] != null)
                        {
                            nodeString = nodeString + " { href=\"" + tag.Attributes["HREF"].ToString() + "\" }";
                        }
                        if (tag.Attributes["NAME"] != null)
                        {
                            nodeString = nodeString + " { NAME=\"" + tag.Attributes["NAME"].ToString() + "\" }";
                        }
                        if (tag.Attributes["VALUE"] != null)
                        {
                            nodeString = nodeString + " { VALUE=\"" + tag.Attributes["VALUE"].ToString() + "\" }";
                        }
                    }

                    current = new TreeNode(nodeString);
                    treeNode.Nodes.Add(current);
                }
            }

            //获取节点间的内容
            if (htmlNode.Children != null && htmlNode.Children.Count > 0)
            {
                this.RecursionHtmlNode(list,current, htmlNode.FirstChild, true);
                content = new TreeNode(htmlNode.FirstChild.GetText());
                treeNode.Nodes.Add(content);
            }

            //the sibling nodes
            if (siblingRequired)
            {
                INode sibling = htmlNode.NextSibling;
                while (sibling != null)
                {
                    this.RecursionHtmlNode(list,treeNode, sibling, false);
                    sibling = sibling.NextSibling;
                }
            }
        }
    }
}