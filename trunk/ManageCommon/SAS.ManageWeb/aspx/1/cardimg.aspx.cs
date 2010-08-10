using System;
using System.IO;
using System.Text;
using System.Data;
using System.Drawing;

using SAS.Logic;
using SAS.Common;
using SAS.Config;
using SAS.Entity;

namespace SAS.ManageWeb
{
    /// <summary>
    /// 企业名片图片
    /// </summary>
    public class cardimg : CompanyPage
    {
        #region 变量声明
        /// <summary>
        /// 企业名片id
        /// </summary>
        public int qycardid = SASRequest.GetInt("qycardid", -1);
        /// <summary>
        /// 企业信息
        /// </summary>
        protected Companys companyinfo;
        /// <summary>
        /// 名片模板ID
        /// </summary>
        protected int cardtempid = 0;
        /// <summary>
        /// 配置文件id
        /// </summary>
        protected int cardconfigid = 0;
        #endregion

        protected override void ShowPage()
        {
            pagetitle = "企业图片式名片";
            
            if (qycardid == -1)
            {
                AddErrLine("无效的企业名片ID");
                return;
            }

            companyinfo = Companies.GetCompanyCacheInfo(qycardid);
            if (companyinfo == null)
            {
                AddErrLine("不存在的企业名片");
                return;
            }
            UpdateMetaInfo(companyinfo.En_name + "," + companyinfo.En_phone, Utils.CutString(Utils.RemoveHtml(companyinfo.En_desc), 0, 60), "");
            if (companyinfo.Configid == 0) cardconfigid = 1;

            CardConfigInfo cci = CardConfigs.GetCardConfigCacheInfo(cardconfigid);
            if (cci == null) cci = CardConfigs.GetCardConfigCacheInfo(1);

            cardtempid = cci.tid;

            CardTemplateInfo cti = Templates.GetCardTemplateItem(cardtempid);
            if (cti == null)
            {
                AddErrLine("名片信息不存在");
                return;
            }
            string[] curparm = cti.currentfile.Split('|');
            if (curparm.Length == 0) AddErrLine("参数传递错误！");
            if (!Utils.IsImgFilename(curparm[0])) AddErrLine("参数传递错误！");
            if (IsErr()) return;
            //当请求文件不存则创建新名片
            string fullfilename = string.Format(@"{0}/cardimg/{1}/{2}", BaseConfigs.GetSitePath, cardtempid, qycardid.ToString() + curparm[0]);
            string backfilename = string.Format(@"{0}/cardtemplate/{1}/{2}", BaseConfigs.GetSitePath, cti.directory, curparm[0]);
            if (!File.Exists(Utils.GetMapPath(fullfilename)))
            {
                if (!File.Exists(Utils.GetMapPath(backfilename)))
                {
                    AddErrLine("名片模板文件不存在");
                    return;
                }

                if (!Directory.Exists(Utils.GetMapPath(BaseConfigs.GetSitePath + "cardimg/" + cardtempid)))
                {
                    Directory.CreateDirectory(Utils.GetMapPath(BaseConfigs.GetSitePath + "cardimg/" + cardtempid));
                }

                Image bimage = null;
                MemoryStream m_ms = null;
                try
                {
                    FileStream b_fs = new FileStream(Utils.GetMapPath(backfilename), FileMode.Open);
                    byte[] b_bt = new byte[int.Parse(b_fs.Length.ToString())];
                    b_fs.Read(b_bt, 0, int.Parse(b_fs.Length.ToString()));
                    b_fs.Close();
                    b_fs.Dispose();
                    m_ms = new MemoryStream(b_bt);
                    bimage = Image.FromStream(m_ms);
                    string watertexts = companyinfo.En_name + ",联 系 人：" + companyinfo.En_contact + ",联系电话：" + companyinfo.En_phone + ",经营模式：" + SAS.Entity.EnumCatch.GetCompanyType(companyinfo.En_type) + ",地    址：" + companyinfo.En_address;
                    string xposes = "12,12,12,12,12";
                    string yposes = "65,92,113,134,155";
                    string fontnames = "宋体,宋体";
                    string fontsizes = "20,12";
                    string fontcolors = "#375b1b,#000";
                    string errormsg = LogicUtils.AddCardSignText(bimage, Utils.GetMapPath(fullfilename), watertexts, xposes, yposes, 80, fontnames, fontsizes, fontcolors);
                    if (errormsg != "")
                    {
                        AddErrLine(errormsg);
                        return;
                    }
                    //LogicUtils.AddImageSignText(bimage, Utils.GetMapPath(fullfilename), "试试水印", 4, 80, "经典超圆简", 20);
                }
                catch (Exception ex)
                {
                    AddErrLine("名片生成出问题");
                    return;
                }
                finally
                {
                    if (m_ms != null)
                    {
                        m_ms.Close();
                        m_ms.Dispose();
                    }
                    if (bimage != null)
                        bimage.Dispose();
                }

            }
            
            Utils.ResponseFile(Utils.GetMapPath(fullfilename), fullfilename, Utils.GetFileExtName(fullfilename));
        }
    }
}
