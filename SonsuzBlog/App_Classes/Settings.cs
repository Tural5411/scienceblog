using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Web;

namespace SonsuzBlog.App_Classes
{
    public class Settings
    {
        public static Size SekilKicikBoy
        {
            get
            {
                Size sonuc = new Size();
                sonuc.Width = Convert.ToInt32(ConfigurationManager.AppSettings["kw"]);
                sonuc.Height = Convert.ToInt32(ConfigurationManager.AppSettings["kh"]);
                return sonuc;

            }
        }
        public static Size SekilOrtaBoy
        {
            get
            {
                Size sonuc = new Size();
                sonuc.Width = Convert.ToInt32(ConfigurationManager.AppSettings["ow"]);
                sonuc.Height = Convert.ToInt32(ConfigurationManager.AppSettings["oh"]);
                return sonuc;

            }
        }
        public static Size SekilBoyukBoy
        {
            get
            {
                Size sonuc = new Size();
                sonuc.Width = Convert.ToInt32(ConfigurationManager.AppSettings["bw"]);
                sonuc.Height = Convert.ToInt32(ConfigurationManager.AppSettings["bh"]);
                return sonuc;

            }
        }
    }
}