﻿/****************************************************************************
*项目名称：SAEA.MQTT.Core.Packets
*CLR 版本：4.0.30319.42000
*机器名称：WENLI-PC
*命名空间：SAEA.MQTT.Core.Packets
*类 名 称：ReceivedMqttPacket
*版 本 号：V1.0.0.0
*创建人： yswenli
*电子邮箱：wenguoli_520@qq.com
*创建时间：2019/1/15 10:14:57
*描述：
*=====================================================================
*修改时间：2019/1/15 10:14:57
*修 改 人： yswenli
*版 本 号： V3.6.2.2
*描    述：
*****************************************************************************/
using SAEA.MQTT.Common.Serializer;
using System;
using System.Collections.Generic;
using System.Text;

namespace SAEA.MQTT.Core.Packets
{
    public class ReceivedMqttPacket
    {
        public ReceivedMqttPacket(byte fixedHeader, MqttPacketBodyReader body)
        {
            FixedHeader = fixedHeader;
            Body = body;
        }

        public byte FixedHeader { get; }

        public MqttPacketBodyReader Body { get; }
    }
}