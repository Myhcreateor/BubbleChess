using ProtoBuf;
using System;
using System.IO;
using UnityEngine;

public class MsgBase
{
	public virtual ProtocolEnum ProtoType { get; set; }
	//编码协议名
	public static byte[] EncodeName(MsgBase msgBase)
	{
		byte[] nameBytes = System.Text.Encoding.UTF8.GetBytes(msgBase.ProtoType.ToString());
		Int16 len = (Int16)nameBytes.Length;
		byte[] bytes = new byte[2 + len];
		bytes[0] = (byte)(len % 256);
		bytes[1] = (byte)(len / 256);
		Array.Copy(nameBytes, 0, bytes, 2, len);
		return bytes;
	}
	//协议解码
	public static ProtocolEnum DecodeName(byte[] bytes,int offset,out int count)
	{
		count = 0;
		if (offset + 2 > bytes.Length) return ProtocolEnum.None;
		Int16 len = (Int16)((bytes[offset + 1] << 8) | bytes[offset]);
		if (offset + 2 + len > bytes.Length) return ProtocolEnum.None;
		count = 2 + len;
		try
		{
			string name = System.Text.Encoding.UTF8.GetString(bytes, offset + 2, len);
			return (ProtocolEnum)System.Enum.Parse(typeof(ProtocolEnum), name);
		}
		catch (Exception ex)
		{
			Debug.LogError("不存在的协议:" + ex.ToString());
			return ProtocolEnum.None;
		}
	}
	//协议序列化及加密
	public static byte[] Encode(MsgBase msgBase)
	{
		string secret = string.IsNullOrEmpty(NetManager.Instance.SercetKey) ? NetManager.Instance.PublicKey : NetManager.Instance.SercetKey;
		using (var memory =  new MemoryStream())
		{
			//将协议类进行序列化转换成byte数组
			Serializer.Serialize(memory, msgBase);
			byte[] bytes = memory.ToArray();
			//对数组进行加密
			bytes = AES.AESEncrypt(bytes, secret);
			return bytes;
		}
	}
	//协议反序列化及解密
	public static MsgBase Decode(ProtocolEnum protocol,byte[] bytes,int offset,int count)
	{
		if (count <= 0)
		{
			Debug.LogError("协议解密出错，数据长度为0");
			return null;
		}
		string secret = string.IsNullOrEmpty(NetManager.Instance.SercetKey) ? NetManager.Instance.PublicKey : NetManager.Instance.SercetKey;
		try
		{
			byte[] newBytes = new byte[count];
			Array.Copy(bytes, offset, newBytes, 0, count);
			newBytes = AES.AESDecrypt(newBytes, secret);
			using(var memory = new MemoryStream(newBytes,0,newBytes.Length))
			{
				Type t = System.Type.GetType(protocol.ToString());
				return (MsgBase)Serializer.NonGeneric.Deserialize(t, memory);
			}
		}
		catch (Exception ex)
		{
			Debug.LogError("协议解密出错:"+ex.ToString());
			return null;
		}
	}
}
