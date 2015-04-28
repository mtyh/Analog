using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Analog.BLL
{
    public class Config
    {
        public Config()
        {
            XmlConfigUtil util = new XmlConfigUtil("D:/Analog/Config.xml");

            _main = util.Read("System", "Main");
            _host = util.Read("System", "Host");
            _scores = util.Read("System", "Scores");
            _timetable = util.Read("System", "Timetable");
            _log = util.Read("System", "Log");
        }

        public static Config init()
        {
            return new Config();
        }

        private string _log;

        public string Log
        {
            get { return _log; }
        }

        /// <summary>
        /// 主机登录
        /// </summary>
        private string _host;

        public string Host
        {
            get { return _host; }
        }

        /// <summary>
        /// 菜单页
        /// </summary>
        private string _main;

        public string Main
        {
            get { return _main; }
        }

        /// <summary>
        /// 成绩配置
        /// </summary>
        private string _scores;

        public string Scores
        {
            get { return _scores; }
        }

        /// <summary>
        /// 课程表配置
        /// </summary>
        private string _timetable;

        public string Timetable
        {
            get { return _timetable; }
        }

    }
}
