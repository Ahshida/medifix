using System.Collections.Generic;
using HTMLReportCreator;

namespace HTML.ResetEmail
{
public partial class MainItem : ReportObject
{
partial void OnUserNameing(object value);
partial void OnUserNameed();
private object _username;
public object UserName
{
get
{
return _username;
}
set
{
OnUserNameing(value);
_username = value;
OnUserNameed();
}
}
partial void OnRootUrling(object value);
partial void OnRootUrled();
private object _rooturl;
public object RootUrl
{
get
{
return _rooturl;
}
set
{
OnRootUrling(value);
_rooturl = value;
OnRootUrled();
}
}
partial void OnConfirmUrling(object value);
partial void OnConfirmUrled();
private object _confirmurl;
public object ConfirmUrl
{
get
{
return _confirmurl;
}
set
{
OnConfirmUrling(value);
_confirmurl = value;
OnConfirmUrled();
}
}
}


}

