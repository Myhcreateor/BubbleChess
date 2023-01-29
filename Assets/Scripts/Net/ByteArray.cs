using System;


public class ByteArray
{
	//默认大小
	public const int DEFAULT_SIZE = 1024;
	//初始大小
	private int initSize = 0;
	//缓冲区
	public byte[] bytes;
	//读写位置
	public int readIndex = 0;
	public int writeIndex = 0;
	//容量
	private int capacity = 0;
	//剩余空间
	public int Remain { get { return capacity - writeIndex; } }
	//数据长度
	public int Length { get { return writeIndex - readIndex; } }

	public ByteArray()
	{
		bytes = new byte[DEFAULT_SIZE];
		capacity = DEFAULT_SIZE;
		initSize = DEFAULT_SIZE;
		readIndex = 0;
		writeIndex = 0;
	}
	public ByteArray(Byte[] deflautBytes)
	{
		bytes = deflautBytes;
		capacity = deflautBytes.Length;
		initSize = deflautBytes.Length;
		readIndex = 0;
		writeIndex = deflautBytes.Length;
	}
	public void CheckAndMoveBytes()
	{
		if (Length < 8)
		{
			MoveBytes();
		}
	}
	public void MoveBytes()
	{
		if (readIndex < 0) return;
		Array.Copy(bytes, readIndex, bytes, 0, Length);
		writeIndex = Length;
		readIndex = 0;
	}
	public void ReSize(int size)
	{
		if (readIndex < 0) return;
		if (size < Length) return;
		if (size < initSize) return;
		int n = 1024;
		while (n < size) n *= 2;
		capacity = n;
		byte[] newByte = new byte[capacity];
		Array.Copy(bytes, readIndex, newByte, 0, Length);
		bytes = newByte;
		writeIndex = Length;
		readIndex = 0;
	}
}

