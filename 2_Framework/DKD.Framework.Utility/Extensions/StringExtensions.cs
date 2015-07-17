using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using Microsoft.VisualBasic;

namespace DKD.Framework.Utility
{
    /// <summary>
    /// 字符串扩展
    /// </summary>
    public static class StringExtensions
    {

        #region 变量

        private static readonly Regex YesInt = new Regex("^[1-9][0-9]*$"); //正整数判断
        private static readonly Regex NoInt = new Regex("^[0-9]*$"); //整数判断
        private static readonly Regex YesPrice = new Regex(@"(^\d*\.?\d*[0-9]+\d*$)|(^[0-9]+\d*\.\d*$)"); //价格判断 
        private static readonly Regex YesSpacia = new Regex(@"(\r\n)", RegexOptions.IgnoreCase);
        private static readonly IpHelper.IPAddress IpSearch = __getIPConfig();

        private static IpHelper.IPAddress __getIPConfig()
        {
            try
            {
                //if (HttpContext.Current != null)
                //    return new IpHelper.IPAddress(HttpContext.Current.Server.MapPath(IpHelper.ConfigBase.Instance<Config.FrameworkConfig>().IPDatabasePath));
                //return new IpHelper.IPAddress(System.IO.Path.Combine((AppDomain.CurrentDomain.RelativeSearchPath ?? AppDomain.CurrentDomain.BaseDirectory), Config.ConfigBase.Instance<Config.FrameworkConfig>().IPDatabasePath));
                return null;
            }
            catch { return null; }
        }

        //以下是汉字转换成拼音常量
        private static int[] pyValue =
        {
            -20319,-20317,-20304,-20295,-20292,-20283,-20265,-20257,-20242,-20230,-20051,-20036,
            -20032,-20026,-20002,-19990,-19986,-19982,-19976,-19805,-19784,-19775,-19774,-19763,
            -19756,-19751,-19746,-19741,-19739,-19728,-19725,-19715,-19540,-19531,-19525,-19515,
            -19500,-19484,-19479,-19467,-19289,-19288,-19281,-19275,-19270,-19263,-19261,-19249,
            -19243,-19242,-19238,-19235,-19227,-19224,-19218,-19212,-19038,-19023,-19018,-19006,
            -19003,-18996,-18977,-18961,-18952,-18783,-18774,-18773,-18763,-18756,-18741,-18735,
            -18731,-18722,-18710,-18697,-18696,-18526,-18518,-18501,-18490,-18478,-18463,-18448,
            -18447,-18446,-18239,-18237,-18231,-18220,-18211,-18201,-18184,-18183, -18181,-18012,
            -17997,-17988,-17970,-17964,-17961,-17950,-17947,-17931,-17928,-17922,-17759,-17752,
            -17733,-17730,-17721,-17703,-17701,-17697,-17692,-17683,-17676,-17496,-17487,-17482,
            -17468,-17454,-17433,-17427,-17417,-17202,-17185,-16983,-16970,-16942,-16915,-16733,
            -16708,-16706,-16689,-16664,-16657,-16647,-16474,-16470,-16465,-16459,-16452,-16448,
            -16433,-16429,-16427,-16423,-16419,-16412,-16407,-16403,-16401,-16393,-16220,-16216,
            -16212,-16205,-16202,-16187,-16180,-16171,-16169,-16158,-16155,-15959,-15958,-15944,
            -15933,-15920,-15915,-15903,-15889,-15878,-15707,-15701,-15681,-15667,-15661,-15659,
            -15652,-15640,-15631,-15625,-15454,-15448,-15436,-15435,-15419,-15416,-15408,-15394,
            -15385,-15377,-15375,-15369,-15363,-15362,-15183,-15180,-15165,-15158,-15153,-15150,
            -15149,-15144,-15143,-15141,-15140,-15139,-15128,-15121,-15119,-15117,-15110,-15109,
            -14941,-14937,-14933,-14930,-14929,-14928,-14926,-14922,-14921,-14914,-14908,-14902,
            -14894,-14889,-14882,-14873,-14871,-14857,-14678,-14674,-14670,-14668,-14663,-14654,
            -14645,-14630,-14594,-14429,-14407,-14399,-14384,-14379,-14368,-14355,-14353,-14345,
            -14170,-14159,-14151,-14149,-14145,-14140,-14137,-14135,-14125,-14123,-14122,-14112,
            -14109,-14099,-14097,-14094,-14092,-14090,-14087,-14083,-13917,-13914,-13910,-13907,
            -13906,-13905,-13896,-13894,-13878,-13870,-13859,-13847,-13831,-13658,-13611,-13601,
            -13406,-13404,-13400,-13398,-13395,-13391,-13387,-13383,-13367,-13359,-13356,-13343,
            -13340,-13329,-13326,-13318,-13147,-13138,-13120,-13107,-13096,-13095,-13091,-13076,
            -13068,-13063,-13060,-12888,-12875,-12871,-12860,-12858,-12852,-12849,-12838,-12831,
            -12829,-12812,-12802,-12607,-12597,-12594,-12585,-12556,-12359,-12346,-12320,-12300,
            -12120,-12099,-12089,-12074,-12067,-12058,-12039,-11867,-11861,-11847,-11831,-11798,
            -11781,-11604,-11589,-11536,-11358,-11340,-11339,-11324,-11303,-11097,-11077,-11067,
            -11055,-11052,-11045,-11041,-11038,-11024,-11020,-11019,-11018,-11014,-10838,-10832,
            -10815,-10800,-10790,-10780,-10764,-10587,-10544,-10533,-10519,-10331,-10329,-10328,
            -10322,-10315,-10309,-10307,-10296,-10281,-10274,-10270,-10262,-10260,-10256,-10254
        };

        private static readonly string[] PyName =
        {
            "A","Ai","An","Ang","Ao","Ba","Bai","Ban","Bang","Bao","Bei","Ben",
            "Beng","Bi","Bian","Biao","Bie","Bin","Bing","Bo","Bu","Ba","Cai","Can",
            "Cang","Cao","Ce","Ceng","Cha","Chai","Chan","Chang","Chao","Che","Chen","Cheng",
            "Chi","Chong","Chou","Chu","Chuai","Chuan","Chuang","Chui","Chun","Chuo","Ci","Cong",
            "Cou","Cu","Cuan","Cui","Cun","Cuo","Da","Dai","Dan","Dang","Dao","De",
            "Deng","Di","Dian","Diao","Die","Ding","Diu","Dong","Dou","Du","Duan","Dui",
            "Dun","Duo","E","En","Er","Fa","Fan","Fang","Fei","Fen","Feng","Fo",
            "Fou","Fu","Ga","Gai","Gan","Gang","Gao","Ge","Gei","Gen","Geng","Gong",
            "Gou","Gu","Gua","Guai","Guan","Guang","Gui","Gun","Guo","Ha","Hai","Han",
            "Hang","Hao","He","Hei","Hen","Heng","Hong","Hou","Hu","Hua","Huai","Huan",
            "Huang","Hui","Hun","Huo","Ji","Jia","Jian","Jiang","Jiao","Jie","Jin","Jing",
            "Jiong","Jiu","Ju","Juan","Jue","Jun","Ka","Kai","Kan","Kang","Kao","Ke",
            "Ken","Keng","Kong","Kou","Ku","Kua","Kuai","Kuan","Kuang","Kui","Kun","Kuo",
            "La","Lai","Lan","Lang","Lao","Le","Lei","Leng","Li","Lia","Lian","Liang",
            "Liao","Lie","Lin","Ling","Liu","Long","Lou","Lu","Lv","Luan","Lue","Lun",
            "Luo","Ma","Mai","Man","Mang","Mao","Me","Mei","Men","Meng","Mi","Mian",
            "Miao","Mie","Min","Ming","Miu","Mo","Mou","Mu","Na","Nai","Nan","Nang",
            "Nao","Ne","Nei","Nen","Neng","Ni","Nian","Niang","Niao","Nie","Nin","Ning",
            "Niu","Nong","Nu","Nv","Nuan","Nue","Nuo","O","Ou","Pa","Pai","Pan",
            "Pang","Pao","Pei","Pen","Peng","Pi","Pian","Piao","Pie","Pin","Ping","Po",
            "Pu","Qi","Qia","Qian","Qiang","Qiao","Qie","Qin","Qing","Qiong","Qiu","Qu",
            "Quan","Que","Qun","Ran","Rang","Rao","Re","Ren","Reng","Ri","Rong","Rou",
            "Ru","Ruan","Rui","Run","Ruo","Sa","Sai","San","Sang","Sao","Se","Sen",
            "Seng","Sha","Shai","Shan","Shang","Shao","She","Shen","Sheng","Shi","Shou","Shu",
            "Shua","Shuai","Shuan","Shuang","Shui","Shun","Shuo","Si","Song","Sou","Su","Suan",
            "Sui","Sun","Suo","Ta","Tai","Tan","Tang","Tao","Te","Teng","Ti","Tian",
            "Tiao","Tie","Ting","Tong","Tou","Tu","Tuan","Tui","Tun","Tuo","Wa","Wai",
            "Wan","Wang","Wei","Wen","Weng","Wo","Wu","Xi","Xia","Xian","Xiang","Xiao",
            "Xie","Xin","Xing","Xiong","Xiu","Xu","Xuan","Xue","Xun","Ya","Yan","Yang",
            "Yao","Ye","Yi","Yin","Ying","Yo","Yong","You","Yu","Yuan","Yue","Yun",
            "Za", "Zai","Zan","Zang","Zao","Ze","Zei","Zen","Zeng","Zha","Zhai","Zhan",
            "Zhang","Zhao","Zhe","Zhen","Zheng","Zhi","Zhong","Zhou","Zhu","Zhua","Zhuai","Zhuan",
            "Zhuang","Zhui","Zhun","Zhuo","Zi","Zong","Zou","Zu","Zuan","Zui","Zun","Zuo"
        };


        #endregion

        #region 前台方法

        /// <summary>
        /// 返回SQL字符数据
        /// </summary>
        public static string SqlStringValue(this object value)
        {
            return value.ToString().Replace("'", "''");
        }

        /// <summary>
        /// 返回WEB程序真实的URL地址 如:http://www.baidu.com 或 http://localhost:234/cms
        /// </summary>
        public static string GetHostDomain()
        {
            return string.Format("{0}://{1}{2}", HttpContext.Current.Request.Url.Scheme, HttpContext.Current.Request.Url.Host, HttpContext.Current.Request.Url.Port == 80 ? "" : ":" + HttpContext.Current.Request.Url.Port.ToString());
        }

        /// <summary>
        /// 判断URL字符串参数是否正常，防止注入 true:为有非法字符,false:为没有
        /// </summary>
        /// <param name="param">要判断的字符</param>
        /// <returns>true:为有非法字符,false:为没有</returns>
        public static bool IsRightString(this string param)
        {
            if (string.IsNullOrEmpty(param))
                return false;


            #region 正则匹配

            //构造SQL的注入关键字符
            string[] strBadChar = {"and"
                                    ,"exec"
                                    ,"insert"
                                    ,"select"
                                    ,"delete"
                                    ,"update"
                                    ,"count"
                                    ,"from"
                                    ,"drop"
                                    ,"asc"
                                    ,"char"
                                    ,"or"
                                    //,"*"
                                    ,"%"
                                    ,";"
                                    ,":"
                                    ,"\'"
                                    ,"\""
                                    ,"-"
                                    ,"chr"
                                    ,"mid"
                                    ,"master"
                                    ,"truncate"
                                    ,"char"
                                    ,"declare"
                                    ,"SiteName"
                                    ,"net user"
                                    ,"xp_cmdshell"
                                    ,"/add"
                                    ,"exec master.dbo.xp_cmdshell"
                                    ,"net localgroup administrators"};

            //构造正则表达式
            string strRegex = ".*(";
            for (int i = 0; i < strBadChar.Length - 1; i++)
            {
                strRegex += strBadChar[i] + "|";
            }
            strRegex += strBadChar[strBadChar.Length - 1] + ").*";


            //里面定义恶意字符集合
            //验证inputData是否包含恶意集合
            if (Regex.IsMatch(param.ToUpper(), strRegex.ToUpper()))
            {
                return true;
            }
            return false;

            #endregion

        }

        /// <summary>
        /// 获取客户端真是IP，跳过代理
        /// </summary>
        /// <returns></returns>
        public static string GetUserIp()
        {

            string userIp;
            if (HttpContext.Current.Request.ServerVariables["HTTP_VIA"] == null)
            {
                userIp = HttpContext.Current.Request.UserHostAddress;
            }
            else
            {
                userIp = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            }
            return userIp;
        }

        /// <summary>
        /// 截取字符串，后可添加...
        /// </summary>
        /// <param name="str">被截取的字符串</param>
        /// <param name="subcount">需要截取的个数</param>
        /// <param name="suffix">后缀</param>
        /// <returns></returns>
        public static string SubStringTrue(this string str, int subcount, string suffix="")
        {
            if (str == null)
                return string.Empty;
            if (str.Length >= subcount)
            {
                string temp = str.Substring(0, subcount);
                temp += suffix;

                return temp;
            }
            return str;
        }

        /// <summary>
        /// 获得web.config里的配置名
        /// </summary>
        /// <param name="settingName">配置名</param>
        /// <returns></returns>
        public static string GetAppSetting(this string settingName)
        {
            return System.Configuration.ConfigurationManager.AppSettings[settingName];
        }

        /// <summary>
        /// 清除给定字符串中的回车及换行符
        /// </summary>
        /// <param name="str">要清除的字符串</param>
        /// <returns>清除后返回的字符串</returns>
        public static string ClearBr(this string str)
        {
            Match m;

            for (m = YesSpacia.Match(str); m.Success; m = m.NextMatch())
            {
                str = str.Replace(m.Groups[0].ToString(), "");
            }
            return str;
        }

        /// <summary>
        /// 从字符串的指定位置截取指定长度的子字符串
        /// </summary>
        /// <param name="str">原字符串</param>
        /// <param name="startIndex">子字符串的起始位置</param>
        /// <param name="length">子字符串的长度</param>
        /// <returns>子字符串</returns>
        public static string CoustomerString(this string str, int startIndex, int length)
        {
            if (startIndex >= 0)
            {
                if (length < 0)
                {
                    length = length * -1;
                    if (startIndex - length < 0)
                    {
                        length = startIndex;
                        startIndex = 0;
                    }
                    else
                        startIndex = startIndex - length;
                }

                if (startIndex > str.Length)
                    return "";
            }
            else
            {
                if (length < 0)
                    return "";
                if (length + startIndex > 0)
                {
                    length = length + startIndex;
                    startIndex = 0;
                }
                else
                    return "";
            }

            if (str.Length - startIndex < length)
                length = str.Length - startIndex;

            return str.Substring(startIndex, length);
        }

        /// <summary>
        /// 转换为简体中文
        /// </summary>
        public static string ToSChinese(this string str)
        {
            return Strings.StrConv(str, VbStrConv.SimplifiedChinese);
        }

        /// <summary>
        /// 转换为繁体中文
        /// </summary>
        public static string ToTChinese(this string str)
        {
            return Strings.StrConv(str, VbStrConv.TraditionalChinese);
        }

        /// <summary>
        /// 当前值为null或空时是则用value替换
        /// </summary>
        /// <param name="obj">当前值</param>
        /// <param name="value">替换值</param>
        /// <returns></returns>
        public static string DefaultValue(this string obj, string value)
        {
            return string.IsNullOrEmpty(obj) ? value : obj;
        }

        /// <summary>
        /// 返回一个HTML字符串，
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static HtmlString WithoutHtml(this string obj)
        {
            return new HtmlString(obj);
        }

        /// <summary>
        /// 重复字符串
        /// </summary>
        /// <param name="obj">要重复的字符串</param>
        /// <param name="repeatCount">重复多少次</param>
        /// <returns></returns>
        public static string StrRepeat(this string obj, int repeatCount)
        {

            var sb = new StringBuilder();
            for (int idx = 0; idx < repeatCount; idx++)
            {
                sb.Append(obj);
            }
            return sb.ToString();
        }

        /// <summary>
        /// 获取文件扩展名
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string GetFileExtension(this string obj)
        {
            string extension = "";

            try
            {
                if (!string.IsNullOrEmpty(obj))
                {
                    string[] temp = obj.Split(new[] { "." }, StringSplitOptions.RemoveEmptyEntries);

                    if (temp.Length > 0)
                        extension = temp[temp.Length - 1];
                }
            }
            catch
            { }

            return extension;
        }

        /// <summary>
        /// 获取字符串中Img标签中的值
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string[] GetImages(this string html)
        {
            // 定义正则表达式用来匹配 img 标签
            Regex regImg = new Regex(@"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>", RegexOptions.IgnoreCase);

            // 搜索匹配的字符串
            MatchCollection matches = regImg.Matches(html);

            int i = 0;
            string[] sUrlList = new string[matches.Count];

            // 取得匹配项列表
            foreach (Match match in matches)
                sUrlList[i++] = match.Groups["imgUrl"].Value;

            return sUrlList;
        }

        /// <summary>
        /// 扩展String.Format
        /// </summary>
        /// <param name="str"></param>
        /// <param name="para"></param>
        /// <returns></returns>
        public static string ToFormat(this string str, params object[] para)
        {
            return string.Format(str, para);
        }
        

        #endregion

        #region 类型判断

        /// <summary>
        /// 判断参数是否为正整数
        /// </summary>
        public static bool IsInt(this string param)
        {
            if (string.IsNullOrEmpty(param))
            {
                return false;
            }

            if (YesInt.IsMatch(param))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 判断参数是否为正整数或零
        /// </summary>
        public static bool IsIntAndZero(this string param)
        {
            if (string.IsNullOrEmpty(param))
            {
                return false;
            }

            if (NoInt.IsMatch(param))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 判断参数是否为decimal（比如价格）
        /// </summary>
        /// <param name="param">参数</param>
        public static bool IsDecimal(this string param)
        {
            if (string.IsNullOrEmpty(param))
            {
                return false;
            }

            if (YesPrice.IsMatch(param))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 检测是否符合email格式
        /// </summary>
        /// <param name="strEmail">要判断的email字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsValidEmail(this string strEmail)
        {
            if (string.IsNullOrEmpty(strEmail))
                return false;

            return Regex.IsMatch(strEmail, @"^[\w\.]+([-]\w+)*@[A-Za-z0-9-_]+[\.][A-Za-z0-9-_]");
        }

        /// <summary>
        /// 是否是手机号码
        /// </summary>
        /// <param name="src">要判断的字符串</param>
        public static bool IsMobile(this string src)
        {
            if (string.IsNullOrEmpty(src))
                return false;

            return Regex.IsMatch(src, @"^1[3458]\d{9}$", RegexOptions.IgnoreCase);
        }

        #endregion

        #region 数据库相关

        /// <summary>
        /// 去除HTML标记
        /// </summary> 
        public static string TrimHtml(this string str)
        {
            return new Regex(@"<[^>]+>|</[^>]+>").Replace(str, "");
        }

        /// <summary>
        /// 去掉特殊字符存入数据库
        /// </summary> 
        public static string FormatToDataBase(this string str)
        {
            str = str.Replace("'", "@23").Replace("--", "@24").Replace("\"", "@25").Replace("<", "@26").Replace(">", "@27");
            str = str.Replace("and", "@31").Replace("exec", "@32").Replace("insert", "@33").Replace("select", "@34").Replace("delete", "@35").Replace("chr", "@36").Replace("mid", "@37").Replace("or", "@38").Replace("truncate", "@39").Replace("truncate", "@40").Replace("truncate", "@41").Replace("declare", "@42").Replace("join", "@43").Replace("*", "@44");
            return str;
        }

        /// <summary>
        /// 从数据库还原特殊标记
        /// </summary>
        public static string FormatToHtml(this string str)
        {
            str = str.Replace("@31", "and").Replace("@32", "exec").Replace("@33", "insert").Replace("@34", "select").Replace("@35", "delete").Replace("@36", "chr").Replace("@37", "mid").Replace("@38", "or").Replace("@39", "truncate").Replace("@40", "truncate").Replace("@41", "truncate").Replace("@42", "declare").Replace("@43", "join").Replace("@44", "*");
            return str.Replace("@23", "'").Replace("@24", "--").Replace("@25", "\"").Replace("@26", "<").Replace("@27", ">");
        }

        #endregion

        #region 汉字转换成拼音

        /// <summary>
        /// GetStringSpell方法:取得汉字字符串的拼音的首字母
        /// </summary>
        /// <param name="strText">汉字字符串</param>
        /// <returns>取得汉字字符串的拼音的首字母</returns>
        public static string GetStringSpell(this string strText)
        {
            int len = strText.Length;
            string myStr = "";
            for (int i = 0; i < len; i++)
            {
                myStr += GetCharSpell(strText.Substring(i, 1));
            }
            return myStr;
        }

        /// <summary>
        /// GetCharSpell方法:取得汉字字符的拼音的首字母
        /// </summary>
        /// <param name="cnChar">汉字字符</param>
        /// <returns>取得汉字字符的拼音的首字母</returns>
        public static string GetCharSpell(this string cnChar)
        {
            byte[] arrCn = Encoding.Default.GetBytes(cnChar);
            if (arrCn.Length > 1)
            {
                int area = arrCn[0];
                int pos = arrCn[1];
                int code = (area << 8) + pos;
                int[] areacode = {  45217, 3, 45761, 46318, 46826, 47010, 47297, 47614, 
                                    48119, 48119, 49062, 49324, 49896, 50371, 50614, 50622, 50906, 51387, 51446, 52218, 52698, 
                                    52698, 52698, 52980, 53689, 54481 
                                 };
                for (int i = 0; i < 26; i++)
                {
                    int max = 55290;
                    if (i != 25) max = areacode[i + 1];
                    if (areacode[i] <= code && code < max)
                    {
                        return Encoding.Default.GetString(new[] { (byte)(65 + i) });
                    }
                }
                return "*";
            }
            return cnChar;
        }

        /// <summary>
        /// 把汉字转换成拼音(全拼)
        /// </summary>
        /// <param name="hzString">汉字字符串</param>
        /// <returns>转换后的拼音(全拼)字符串</returns>
        public static string ConvertAbc(this string hzString)
        {
            Regex regex = new Regex("^[\u4e00-\u9fa5]$");
            byte[] array;
            string pyString = "";
            int chrAsc;
            int i1;
            int i2;
            char[] noWChar = hzString.ToCharArray();

            for (int j = 0; j < noWChar.Length; j++)
            {
                if (regex.IsMatch(noWChar[j].ToString()))
                {
                    array = Encoding.Default.GetBytes(noWChar[j].ToString());
                    i1 = array[0];
                    i2 = array[1];
                    chrAsc = i1 * 256 + i2 - 65536;
                    if (chrAsc > 0 && chrAsc < 160)
                    {
                        pyString += noWChar[j];
                    }
                    else
                    {
                        if (chrAsc == -9254)
                            pyString += "Zhen";
                        else
                        {
                            for (int i = (pyValue.Length - 1); i >= 0; i--)
                            {
                                if (pyValue[i] <= chrAsc)
                                {
                                    pyString += PyName[i];
                                    break;
                                }
                            }
                        }
                    }
                }
                else
                {
                    pyString += noWChar[j].ToString();
                }
            }
            return pyString;
        }

        #endregion

        #region 其它判断

        /// <summary>
        /// 判断指定字符串在指定字符串数组中的位置
        /// </summary>
        /// <param name="strSearch">字符串</param>
        /// <param name="stringArray">字符串数组</param>
        /// <param name="caseInsensetive">是否不区分大小写, true为不区分, false为区分</param>
        /// <returns>字符串在指定字符串数组中的位置, 如不存在则返回-1</returns>
        public static int GetInArrayId(this string strSearch, string[] stringArray, bool caseInsensetive = false)
        {
            for (int i = 0; i < stringArray.Length; i++)
            {
                if (caseInsensetive)
                {
                    if (strSearch.ToLower() == stringArray[i].ToLower())
                        return i;
                }
                else if (strSearch == stringArray[i])
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// 返回字符串真实长度, 1个汉字长度为2
        /// </summary>
        /// <returns>字符长度</returns>
        public static int GetStringLength(this string str)
        {
            return Encoding.Default.GetBytes(str).Length;
        }

        /// <summary>
        /// 判断指定字符串是否属于指定字符串数组中的一个元素
        /// </summary>
        /// <param name="strSearch">字符串</param>
        /// <param name="stringArray">字符串数组</param>
        /// <param name="caseInsensetive">是否不区分大小写, true为不区分, false为区分</param>
        /// <returns>判断结果</returns>
        public static bool InArray(this string strSearch, string[] stringArray, bool caseInsensetive = false)
        {
            return GetInArrayId(strSearch, stringArray, caseInsensetive) >= 0;
        }

        /// <summary>
        /// 返回文件是否存在
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <returns>是否存在</returns>
        public static bool FileExists(this string filename)
        {
            return System.IO.File.Exists(filename);
        }
        #endregion

        #region IP地址对应区域

        /// <summary>
        /// IP地址对应区域
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string IpAddress(this string obj)
        {
            try
            {
                return IpSearch.IPLocation(obj);
            }
            catch (Exception ex)
            {
                return "未知区域";
            }
        }

        #endregion

        #region Convert string type to other types
        /// <summary>
        /// 字符串转int
        /// </summary>
        /// <param name="s"></param>
        /// <param name="defalut"></param>
        /// <returns></returns>
        public static int ToInt(this string s, int defalut = 0)
        {
            int result = defalut;
            if (int.TryParse(s, out result))
                return result;
            return defalut;
        }

        /// <summary>
        /// 字符串转bool
        /// </summary>
        /// <param name="s"></param>
        /// <param name="defalut"></param>
        /// <returns></returns>
        public static bool ToBool(this string s, bool defalut = false)
        {
            bool result = defalut;
            if (bool.TryParse(s, out result))
                return result;
            return defalut;
        }

        /// <summary>
        /// 字符串转double
        /// </summary>
        /// <param name="s"></param>
        /// <param name="defalut"></param>
        /// <returns></returns>
        public static double ToDouble(this string s, double defalut = 0)
        {
            double result = defalut;
            if (double.TryParse(s, out result))
                return result;
            return defalut;
        }

        /// <summary>
        /// 字符串转decimal
        /// </summary>
        /// <param name="s"></param>
        /// <param name="defalut"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this string s, decimal defalut = 0)
        {
            decimal result = defalut;
            if (decimal.TryParse(s, out result))
                return result;
            else
                return defalut;
        }

        /// <summary>
        /// 字符串转GUID
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static Guid ToGuid(this string s)
        {
            Guid result = Guid.Empty;
            if (Guid.TryParse(s, out result))
                return result;
            else
                return Guid.Empty;
        }

        /// <summary>
        /// 字符串转日期
        /// </summary>
        /// <param name="s"></param>
        /// <param name="defalut"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string s, DateTime defalut = new DateTime())
        {
            DateTime result = defalut;
            if (DateTime.TryParse(s, out result))
                return result;
            else
                return defalut;
        }

        /// <summary>
        /// 字符串转Enum
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="s"></param>
        /// <returns></returns>
        public static T ToEnum<T>(this string s) where T : struct
        {
            T result = default(T);
            Enum.TryParse<T>(s, true, out result);
            return result;
        }
        #endregion
    }
}
