using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Analog.BLL
{
    
    public class Service
    {
        public static Config Config = Config.init();

        /// <summary>
        /// 判断是否登录
        /// </summary>
        /// <param name="pagebody"></param>
        /// <param name="identify"></param>
        /// <returns></returns>
        public static bool isLog(string pagebody)
        {
            
            bool _boot = false;
            if (pagebody.IndexOf(Config.Main) > -1)
                _boot=true;
            return _boot;
        }

        /// <summary>
        /// 获取Web Form 的__VIEWSTATE值
        /// </summary>
        /// <returns></returns>
        public string VIEWSTATE(string pageBaby)
        {
            string _viewstate = "";


            return _viewstate;
        }

        /// <summary>
        /// 获取成绩表
        /// </summary>
        public static void scores()
        {

        }

        /// <summary>
        /// 获取课程表
        /// </summary>
        public static void timetable()
        {

        }
    }
}
