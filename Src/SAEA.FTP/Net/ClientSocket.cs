﻿/****************************************************************************
*项目名称：SAEA.FTP.Net
*CLR 版本：4.0.30319.42000
*机器名称：WALLE-PC
*命名空间：SAEA.FTP.Net
*类 名 称：ClientSocket
*版 本 号：V1.0.0.0
*创建人： yswenli
*电子邮箱：yswenli@outlook.com
*创建时间：2019/9/27 17:02:27
*描述：
*=====================================================================
*修改时间：2019/9/27 17:02:27
*修 改 人： yswenli
*版 本 号： V1.0.0.0
*描    述：
*****************************************************************************/
using SAEA.Sockets;
using System;
using System.Collections.Generic;
using System.Text;

namespace SAEA.FTP.Net
{
    class ClientSocket
    {
        public ClientSocket(ClientConfig config)
        {
            SocketOptionBuilder.Instance
                .SetSocket()
                .UseIocp()
                .SetIP(config.IP)
                .SetPort(config.Port)
                
        }
    }
}
