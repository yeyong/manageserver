using System;
using System.IO;

using SAS.Common;
using SAS.Config;

namespace SAS.Logic
{
    public enum AvatarSize { Large, Medium, Small }
    public class Avatars
    {
        const string AVATAR_URL = "upload/{0}/{1}/{2}/{3}_avatar_{4}.jpg";
        /// <summary>
        /// 获取头像地址
        /// </summary>
        /// <param name="uid">用户Id</param>
        /// <param name="avatarSize">头像大小，1：大，2：中，3：小</param>
        /// <returns></returns>
        public static string GetAvatarUrl(string uid, AvatarSize avatarSize)
        {
            uid = FormatUid(uid);
            string size = "";
            switch (avatarSize)
            {
                case AvatarSize.Large:
                    size = "large";
                    break;
                case AvatarSize.Medium:
                    size = "medium";
                    break;
                case AvatarSize.Small:
                    size = "small";
                    break;
            }
            string physicsAvatarPath = string.Format(AVATAR_URL, uid.Substring(0, 3), uid.Substring(3, 2), uid.Substring(5, 2), uid.Substring(7, 2), size);
            if (File.Exists(Utils.GetMapPath(BaseConfigs.GetSitePath + "avatars/" + physicsAvatarPath)))
            {
                return Utils.GetRootUrl(BaseConfigs.GetSitePath) + "avatars/" + physicsAvatarPath;
            }
            else
                return GetDefaultAvatarUrl(avatarSize);
        }

        /// <summary>
        /// 获取头像url
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public static string GetAvatarUrl(string uid)
        {
            return GetAvatarUrl(uid, AvatarSize.Medium);
        }

        /// <summary>
        /// 获取默认头像
        /// </summary>
        /// <param name="avatarSize"></param>
        /// <returns></returns>
        public static string GetDefaultAvatarUrl(AvatarSize avatarSize)
        {
            return Utils.GetRootUrl(BaseConfigs.GetSitePath) + "images/common/noavatar_" + avatarSize.ToString().ToLower() + ".gif";
        }

        /// <summary>
        /// 格式化Uid为9位标准格式
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public static string FormatUid(string uid)
        {
            int uidLength = uid.Length;
            if (uidLength < 9)
            {
                for (int i = 0; i < 9 - uidLength; i++)
                    uid = "0" + uid;
            }
            return uid;
        }

        /// <summary>
        /// 是否存在上传头像
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public static bool ExistAvatar(string uid)
        {
            uid = FormatUid(uid);
            string largeAvatar = GetPhysicsAvatarPath(uid, AvatarSize.Large);
            string mediumAvatar = GetPhysicsAvatarPath(uid, AvatarSize.Medium);
            string smallAvatar = GetPhysicsAvatarPath(uid, AvatarSize.Small);
            return File.Exists(largeAvatar) && File.Exists(mediumAvatar) && File.Exists(smallAvatar);
        }

        /// <summary>
        /// 获取头像物理路径
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static string GetPhysicsAvatarPath(string uid, AvatarSize size)
        {
            return Utils.GetMapPath(BaseConfigs.GetSitePath + "avatars/" +
                string.Format(AVATAR_URL, uid.Substring(0, 3), uid.Substring(3, 2), uid.Substring(5, 2), uid.Substring(7, 2), size.ToString().ToLower()));
        }

        /// <summary>
        /// 删除头像
        /// </summary>
        /// <param name="uid"></param>
        public static void DeleteAvatar(string uid)
        {
            uid = FormatUid(uid);
            if (File.Exists(Avatars.GetPhysicsAvatarPath(uid, AvatarSize.Large)))
            {
                File.Delete(Avatars.GetPhysicsAvatarPath(uid, AvatarSize.Large));
                File.Delete(Avatars.GetPhysicsAvatarPath(uid, AvatarSize.Medium));
                File.Delete(Avatars.GetPhysicsAvatarPath(uid, AvatarSize.Small));
            }
        }
    }
}
