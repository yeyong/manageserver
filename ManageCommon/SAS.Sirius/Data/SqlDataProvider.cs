﻿using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.IO;

using SAS.Config;
using SAS.Data;
using SAS.Entity;
using SAS.Common;

namespace SAS.Sirius.Data
{
    public class DataProvider
    {
        /// <summary>
        /// SQL SERVER SQL语句转义
        /// </summary>
        /// <param name="str">需要转义的关键字符串</param>
        /// <param name="pattern">需要转义的字符数组</param>
        /// <returns>转义后的字符串</returns>
        private string RegEsc(string str)
        {
            string[] pattern = { @"%", @"_", @"'" };
            foreach (string s in pattern)
            {
                switch (s)
                {
                    case "%":
                        str = str.Replace(s, "[%]");
                        break;
                    case "_":
                        str = str.Replace(s, "[_]");
                        break;
                    case "'":
                        str = str.Replace(s, "['']");
                        break;
                }
            }
            return str;
        }

        private DbParameter[] GetDateSpanParms(string startdate, string enddate)
        {
            DbParameter[] parms = new DbParameter[2];
            if (startdate != "")
            {
                parms[0] = DbHelper.MakeInParam("@startdate", (DbType)SqlDbType.DateTime, 8, DateTime.Parse(startdate));
            }
            if (enddate != "")
            {
                parms[1] = DbHelper.MakeInParam("@enddate", (DbType)SqlDbType.DateTime, 8, DateTime.Parse(enddate).AddDays(1));
            }
            return parms;
        }

        public int CreateTeams(TeamInfo teaminfo)
        {
            DbParameter[] parms = 
            {
                DbHelper.MakeInParam("@name",(DbType)SqlDbType.NVarChar,255,teaminfo.Name),
                DbHelper.MakeInParam("@teamdomain",(DbType)SqlDbType.NVarChar,255,teaminfo.Teamdomain),
                DbHelper.MakeInParam("@templateid",(DbType)SqlDbType.Int,4,teaminfo.Templateid),
                DbHelper.MakeInParam("@imgs",(DbType)SqlDbType.NVarChar,255,teaminfo.Imgs),
                DbHelper.MakeInParam("@bio",(DbType)SqlDbType.NVarChar,255,teaminfo.Bio),
                DbHelper.MakeInParam("@content1",(DbType)SqlDbType.NText,0,teaminfo.Content1),
                DbHelper.MakeInParam("@content2",(DbType)SqlDbType.NText,0,teaminfo.Content2),
                DbHelper.MakeInParam("@content3",(DbType)SqlDbType.NText,0,teaminfo.Content3),
                DbHelper.MakeInParam("@content4",(DbType)SqlDbType.NText,0,teaminfo.Content4),
                DbHelper.MakeInParam("@teammember",(DbType)SqlDbType.NText,0,teaminfo.TeamMember),
                DbHelper.MakeInParam("@stutas",(DbType)SqlDbType.Int,4,teaminfo.Stutas),
                DbHelper.MakeInParam("@displayorder",(DbType)SqlDbType.Int,4,teaminfo.Displayorder),
                DbHelper.MakeInParam("@seokeywords",(DbType)SqlDbType.NText,0,teaminfo.Seokeywords),
                DbHelper.MakeInParam("@seodescription",(DbType)SqlDbType.NText,0,teaminfo.Seodescription),
                DbHelper.MakeInParam("@creater",(DbType)SqlDbType.VarChar,50,teaminfo.Creater)
            };
            string commandText = String.Format("INSERT INTO [{0}teamInfo] ([name],[teamdomain],[templateid],[imgs],[bio],[content1],[content2],[content3],[content4],[stutas],[displayorder],[teamMember],[seokeywords],[seodescription],[creater]) VALUES (@name,@teamdomain,@templateid,@imgs,@bio,@content1,@content2,@content3,@content4,@stutas,@displayorder,@teamMember,@seokeywords,@seodescription,@creater);SELECT SCOPE_IDENTITY()", BaseConfigs.GetTablePrefix);
            return TypeConverter.ObjectToInt(DbHelper.ExecuteScalar(CommandType.Text, commandText, parms));
        }

        public int CreateWork(TeamWorkInfo workinfo)
        {
            DbParameter[] parms = {
                                    DbHelper.MakeInParam("@name", (DbType)SqlDbType.NVarChar,50,workinfo.Name),
				                    DbHelper.MakeInParam("@start", (DbType)SqlDbType.SmallDateTime,4,workinfo.Start),
				                    DbHelper.MakeInParam("@end", (DbType)SqlDbType.SmallDateTime,4,workinfo.End),
				                    DbHelper.MakeInParam("@worddesc", (DbType)SqlDbType.Text,0,workinfo.Worddesc),
				                    DbHelper.MakeInParam("@url", (DbType)SqlDbType.VarChar,200,workinfo.Url),
				                    DbHelper.MakeInParam("@img", (DbType)SqlDbType.VarChar,200,workinfo.Img),
				                    DbHelper.MakeInParam("@imgbak", (DbType)SqlDbType.VarChar,200,workinfo.Imgbak),
				                    DbHelper.MakeInParam("@teamid", (DbType)SqlDbType.Int,4,workinfo.Teamid),
				                    DbHelper.MakeInParam("@members", (DbType)SqlDbType.VarChar,500,workinfo.Members)
                                  };
            string commandText = String.Format("INSERT INTO [{0}teamwork] ([name],[start],[end],[worddesc],[url],[img],[imgbak],[teamid],[members])VALUES(@name,@start,@end,@worddesc,@url,@img,@imgbak,@teamid,@members);SELECT SCOPE_IDENTITY()", BaseConfigs.GetTablePrefix);
            return TypeConverter.ObjectToInt(DbHelper.ExecuteScalar(CommandType.Text, commandText, parms));
        }

        public SAS.Common.Generic.List<TeamInfo> GetAllTeamList()
        {
            string commandText = string.Format("SELECT * FROM [{0}teamInfo] ORDER BY [createdate]", BaseConfigs.GetTablePrefix);

            IDataReader reader = DbHelper.ExecuteReader(CommandType.Text, commandText);
            SAS.Common.Generic.List<TeamInfo> tlist = new SAS.Common.Generic.List<TeamInfo>();
            while (reader.Read())
            {
                TeamInfo team = new TeamInfo();
                team.TeamID = TypeConverter.ObjectToInt(reader["teamID"], 0);
                team.Name = reader["name"].ToString();
                team.Teamdomain = reader["teamdomain"].ToString();
                team.Templateid = TypeConverter.ObjectToInt(reader["templateid"].ToString(), 0);
                team.BuildDate = Utils.GetStandardDate(reader["builddate"].ToString());
                team.CreateDate = Utils.GetStandardDate(reader["createdate"].ToString());
                team.UpdateDate = Utils.GetStandardDate(reader["updatedate"].ToString());
                team.Imgs = reader["imgs"].ToString();
                team.Bio = reader["bio"].ToString();
                team.Content1 = reader["content1"].ToString();
                team.Content2 = reader["content2"].ToString();
                team.Content3 = reader["content3"].ToString();
                team.Content4 = reader["content4"].ToString();
                team.Stutas = TypeConverter.ObjectToInt(reader["stutas"], 0);
                team.Pageviews = TypeConverter.ObjectToInt(reader["pageviews"], 0);
                team.Displayorder = TypeConverter.ObjectToInt(reader["displayorder"], 0);
                team.TeamMember = reader["teammember"].ToString();
                team.Seokeywords = reader["seokeywords"].ToString();
                team.Seodescription = reader["seodescription"].ToString();
                team.Creater = reader["creater"].ToString();
                tlist.Add(team);
            }
            reader.Close();
            return tlist;
        }

        public DataTable GetAllTeam()
        {
            string commandText = string.Format("SELECT * FROM [{0}teamInfo] ORDER BY [createdate]", BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];
        }

        public IDataReader GetTeamInfoByID(int tid)
        {
            DbParameter[] parms = { DbHelper.MakeInParam("@teamID", (DbType)SqlDbType.Int, 4, tid) };

            string commandText = String.Format("SELECT * FROM [{0}teamInfo] WHERE [teamID] = @teamID", BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteReader(CommandType.Text, commandText, parms);
        }

        public IDataReader GetTeamInfoByDomain(string domain)
        {
            DbParameter[] parms = { DbHelper.MakeInParam("@domain", (DbType)SqlDbType.NVarChar, 255, domain) };
            string commmandText = String.Format("SELECT TOP 1 * FROM [{0}teamInfo] WHERE [teamdomain] = @domain", BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteReader(CommandType.Text, commmandText, parms);
        }

        public bool UpdateTeamInfo(TeamInfo teaminfo)
        {
            DbParameter[] parms = 
            {
                DbHelper.MakeInParam("@teamID",(DbType)SqlDbType.Int,4,teaminfo.TeamID),
                DbHelper.MakeInParam("@name",(DbType)SqlDbType.NVarChar,255,teaminfo.Name),
                DbHelper.MakeInParam("@teamdomain",(DbType)SqlDbType.NVarChar,255,teaminfo.Teamdomain),
                DbHelper.MakeInParam("@templateid",(DbType)SqlDbType.Int,4,teaminfo.Templateid),
                DbHelper.MakeInParam("@imgs",(DbType)SqlDbType.NVarChar,255,teaminfo.Imgs),
                DbHelper.MakeInParam("@updateDate",(DbType)SqlDbType.DateTime,8,teaminfo.UpdateDate),
                DbHelper.MakeInParam("@bio",(DbType)SqlDbType.NVarChar,255,teaminfo.Bio),
                DbHelper.MakeInParam("@content1",(DbType)SqlDbType.NText,0,teaminfo.Content1),
                DbHelper.MakeInParam("@content2",(DbType)SqlDbType.NText,0,teaminfo.Content2),
                DbHelper.MakeInParam("@content3",(DbType)SqlDbType.NText,0,teaminfo.Content3),
                DbHelper.MakeInParam("@content4",(DbType)SqlDbType.NText,0,teaminfo.Content4),
                DbHelper.MakeInParam("@teammember",(DbType)SqlDbType.NText,0,teaminfo.TeamMember),
                DbHelper.MakeInParam("@stutas",(DbType)SqlDbType.Int,4,teaminfo.Stutas),
                DbHelper.MakeInParam("@displayorder",(DbType)SqlDbType.Int,4,teaminfo.Displayorder),
                DbHelper.MakeInParam("@seokeywords",(DbType)SqlDbType.NText,0,teaminfo.Seokeywords),
                DbHelper.MakeInParam("@seodescription",(DbType)SqlDbType.NText,0,teaminfo.Seodescription)
            };

            string commandText = String.Format("UPDATE [{0}teamInfo] SET [name] = @name,[teamdomain] = @teamdomain,[templateid] = @templateid,[updateDate] = @updateDate,[imgs] = @imgs,[bio] = @bio,[content1] = @content1,[content2] = @content2,[content3] = @content3,[content4] = @content4,[stutas] = @stutas,[displayorder] = @displayorder,[teamMember] = @teamMember,[seokeywords] = @seokeywords,[seodescription] = @seodescription	WHERE [teamID]=@teamID ",BaseConfigs.GetTablePrefix);

            int result = DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);

            if (result == 0)
            {
                return false;
            }

            return true;

        }

        public string SetTeamMemberList(string members, int tid)
        {
            DbParameter[] parms = 
            {
                DbHelper.MakeInParam("@tid",(DbType)SqlDbType.Int,4,tid),
                DbHelper.MakeInParam("@members",(DbType)SqlDbType.VarChar,500,members)
            };
            return DbHelper.ExecuteScalarToStr(CommandType.StoredProcedure, string.Format("[{0}setteammemberlist]", BaseConfigs.GetTablePrefix), parms);
        }

        public string SetWorkMemberList(string members, int tid)
        {
            DbParameter[] parms = 
            {
                DbHelper.MakeInParam("@tid",(DbType)SqlDbType.Int,4,tid),
                DbHelper.MakeInParam("@members",(DbType)SqlDbType.VarChar,500,members)
            };
            return DbHelper.ExecuteScalarToStr(CommandType.StoredProcedure, string.Format("[{0}setteamworkmemberlist]", BaseConfigs.GetTablePrefix), parms);
        }

        public int CreateAct(TeamActInfo tai)
        {
            DbParameter[] parms = {
					                DbHelper.MakeInParam("@name",(DbType)SqlDbType.NVarChar,50,tai.Name),
					                DbHelper.MakeInParam("@start",(DbType)SqlDbType.SmallDateTime,4,tai.Start),
					                DbHelper.MakeInParam("@end",(DbType)SqlDbType.SmallDateTime,4,tai.End),
					                DbHelper.MakeInParam("@shortdesc",(DbType)SqlDbType.VarChar,500,tai.Shortdesc),
					                DbHelper.MakeInParam("@img",(DbType)SqlDbType.VarChar,200,tai.Img),
					                DbHelper.MakeInParam("@imgbak",(DbType)SqlDbType.VarChar,200,tai.Imgbak),
					                DbHelper.MakeInParam("@teamid",(DbType)SqlDbType.Int,4,tai.Teamid),
					                DbHelper.MakeInParam("@atype",(DbType)SqlDbType.Int,4,tai.Atype),
					                DbHelper.MakeInParam("@piccollect",(DbType)SqlDbType.Text,0,tai.Piccollect)
                                  };
            string commandText = String.Format("INSERT INTO [{0}teamactivity] ([name],[start],[end],[shortdesc],[img],[imgbak],[teamid],[atype],[piccollect]) VALUES (@name,@start,@end,@shortdesc,@img,@imgbak,@teamid,@atype,@piccollect);SELECT SCOPE_IDENTITY()", BaseConfigs.GetTablePrefix);
            return TypeConverter.ObjectToInt(DbHelper.ExecuteScalar(CommandType.Text, commandText, parms));
        }

        public IDataReader GetTeamActListByTid(int tid)
        {
            DbParameter[] parms = { DbHelper.MakeInParam("@teamid", (DbType)SqlDbType.Int, 4, tid) };

            string commandText = String.Format("SELECT * FROM [{0}teamactivity] WHERE [teamid] = @teamid", BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteReader(CommandType.Text, commandText, parms);
        }

        public IDataReader GetTeamActInfo(int aid)
        {
            DbParameter[] parms = { DbHelper.MakeInParam("@aid", (DbType)SqlDbType.Int, 4, aid) };
            string commmandText = String.Format("SELECT TOP 1 * FROM [{0}teamactivity] WHERE [id] = @aid", BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteReader(CommandType.Text, commmandText, parms);
        }

        public void UpdateTeamAct(TeamActInfo tinfo)
        {
            DbParameter[] parms = {
                                    DbHelper.MakeInParam("@id",(DbType)SqlDbType.Int,4,tinfo.Id),
					                DbHelper.MakeInParam("@name",(DbType)SqlDbType.NVarChar,50,tinfo.Name),
					                DbHelper.MakeInParam("@start",(DbType)SqlDbType.SmallDateTime,4,tinfo.Start),
					                DbHelper.MakeInParam("@end",(DbType)SqlDbType.SmallDateTime,4,tinfo.End),
					                DbHelper.MakeInParam("@shortdesc",(DbType)SqlDbType.VarChar,500,tinfo.Shortdesc),
					                DbHelper.MakeInParam("@img",(DbType)SqlDbType.VarChar,200,tinfo.Img),
					                DbHelper.MakeInParam("@imgbak",(DbType)SqlDbType.VarChar,200,tinfo.Imgbak),
					                DbHelper.MakeInParam("@teamid",(DbType)SqlDbType.Int,4,tinfo.Teamid),
					                DbHelper.MakeInParam("@atype",(DbType)SqlDbType.Int,4,tinfo.Atype),
					                DbHelper.MakeInParam("@piccollect",(DbType)SqlDbType.Text,0,tinfo.Piccollect)
                                  };
            string commandText = String.Format("UPDATE [{0}teamactivity] SET [name] = @name,[start] = @start,[end] = @end,[shortdesc] = @shortdesc,[img] = @img,[imgbak] = @imgbak,[teamid] = @teamid,[atype] = @atype,[piccollect] = @piccollect WHERE [id] = @id", BaseConfigs.GetTablePrefix);
            DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        public IDataReader GetWorksByTid(int tid)
        {
            DbParameter[] parms = { DbHelper.MakeInParam("@teamid", (DbType)SqlDbType.Int, 4, tid) };
            string commandText = String.Format("SELECT * FROM [{0}teamwork] WHERE [teamid] = @teamid", BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteReader(CommandType.Text, commandText, parms);
        }

        public IDataReader GetWorkInfo(int wid)
        {
            DbParameter[] parms = { DbHelper.MakeInParam("@wid", (DbType)SqlDbType.Int, 4, wid) };
            string commmandText = String.Format("SELECT TOP 1 * FROM [{0}teamwork] WHERE [id] = @wid", BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteReader(CommandType.Text, commmandText, parms);
        }

        public void UpdateWorkInfo(TeamWorkInfo workinfo)
        {
            DbParameter[] parms = {
                                    DbHelper.MakeInParam("@id", (DbType)SqlDbType.Int,4,workinfo.Id),
                                    DbHelper.MakeInParam("@name", (DbType)SqlDbType.NVarChar,50,workinfo.Name),
				                    DbHelper.MakeInParam("@start", (DbType)SqlDbType.SmallDateTime,4,workinfo.Start),
				                    DbHelper.MakeInParam("@end", (DbType)SqlDbType.SmallDateTime,4,workinfo.End),
				                    DbHelper.MakeInParam("@worddesc", (DbType)SqlDbType.Text,0,workinfo.Worddesc),
				                    DbHelper.MakeInParam("@url", (DbType)SqlDbType.VarChar,200,workinfo.Url),
				                    DbHelper.MakeInParam("@img", (DbType)SqlDbType.VarChar,200,workinfo.Img),
				                    DbHelper.MakeInParam("@imgbak", (DbType)SqlDbType.VarChar,200,workinfo.Imgbak),
				                    DbHelper.MakeInParam("@teamid", (DbType)SqlDbType.Int,4,workinfo.Teamid),
				                    DbHelper.MakeInParam("@members", (DbType)SqlDbType.VarChar,500,workinfo.Members)
                                  };
            string commandText = String.Format("UPDATE [sas_teamwork] SET [name] = @name,[start] = @start,[end] = @end,[worddesc] = @worddesc,[url] = @url,[img] = @img,[imgbak] = @imgbak,[teamid] = @teamid,[members] = @members	WHERE id=@id", BaseConfigs.GetTablePrefix);
            DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }
    }
}
