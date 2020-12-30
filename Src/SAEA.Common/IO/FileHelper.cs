﻿/****************************************************************************
*Copyright (c) 2018-2020 yswenli All Rights Reserved.
*CLR版本： 4.0.30319.42000
*机器名称：WENLI-PC
*公司名称：yswenli
*命名空间：SAEA.Commom.IO
*文件名： FileHelper
*版本号： v6.0.0.1
*唯一标识：bf3043aa-a84d-42ab-a6b6-b3adf2ab8925
*当前的用户域：WENLI-PC
*创建人： yswenli
*电子邮箱：wenguoli_520@qq.com
*创建时间：2018/4/10 16:53:26
*描述：
*
*=====================================================================
*修改标记
*修改时间：2018/4/10 16:53:26
*修改人： yswenli
*版本号： v6.0.0.1
*描述：
*
*****************************************************************************/
using System;
using System.IO;
using System.Text;

namespace SAEA.Common.IO
{
    /// <summary>
    /// 文件操作类
    /// </summary>
    public static class FileHelper
    {

        public static bool Exists(string filePath)
        {
            return File.Exists(filePath);
        }

        public static string GetDirecotry(string filePath)
        {
            var dir = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            return dir;
        }


        public static void Write(string filePath, byte[] data)
        {
            GetDirecotry(filePath);
            using (FileStream fs = File.Create(filePath))
            {
                fs.Write(data, 0, data.Length);
            }
        }


        public static void WriteString(string filePath, string txt)
        {
            var data = Encoding.UTF8.GetBytes(txt);
            Write(filePath, data);
        }

        /// <summary>
        /// 读取文件内容
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static byte[] Read(string filePath)
        {
            byte[] data = null;
            if(!File.Exists(filePath))
            {
                return data;
            }
            using (var fs = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var buffer = new byte[fs.Length];
                fs.Position = 0;
                var offset = 0;

                while ((offset = fs.Read(buffer, offset, buffer.Length)) > 0)
                {
                    if (offset == fs.Length) break;

                    if (offset == 0) throw new System.Exception($"读取{filePath}出现异常！");

                }
                data = buffer;
            }
            return data;
        }

        public static string ReadString(string filePath)
        {
            var data = Read(filePath);

            return Encoding.UTF8.GetString(data);
        }

        /// <summary>
        /// 读取文件内容
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="read"></param>
        /// <param name="bufferSize"></param>
        public static void Read(string filePath, Action<byte[]> read, int bufferSize = 10240)
        {
            using (var fs = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                fs.Position = 0;

                var data = new byte[bufferSize];

                while (true)
                {
                    var len = fs.Read(data, 0, data.Length);

                    if (len == 0) break;

                    var buffer = data.AsSpan().Slice(0, len).ToArray();

                    read?.Invoke(buffer);
                }
            }
        }
        /// <summary>
        /// 获取文件信息
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static FileInfo GetFileInfo(string filePath)
        {
            if (File.Exists(filePath))
            {
                return new FileInfo(filePath);
            }
            return null;
        }
        
        /// <summary>
        /// 移除文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool Remove(string filePath)
        {
            try
            {
                File.Delete(filePath);
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Error("FileHelper.Remove", ex, filePath);
            }
            return false;
        }
    }
}
