using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public enum RegisterResult
{
	Success,
	Failed,
	AlreadlyExist,
	WrongCode,
	Forbidden
}
public enum LoginResult
{
	Success,
	Failed,
	UserNotExist,
	WrongPwd,
	TimeOutToken
}
public enum LoginType
{
	Phone, Mail, QQ, WX, Token
}
public enum RegisterType
{
	Phone, Mail
}

