using System.Collections.Generic;
using HTMLReportCreator;

namespace HTML.InformStatusChanged
{
public partial class MainItem : ReportObject
{
partial void OnReceivering(object value);
partial void OnReceivered();
private object _receiver;
public object Receiver
{
get
{
return _receiver;
}
set
{
OnReceivering(value);
_receiver = value;
OnReceivered();
}
}
partial void OnTitleing(object value);
partial void OnTitleed();
private object _title;
public object Title
{
get
{
return _title;
}
set
{
OnTitleing(value);
_title = value;
OnTitleed();
}
}
partial void OnRemarksing(Remarks value);
partial void OnRemarksed();
private Remarks _remarks;
public Remarks Remarks
{
get
{
return _remarks;
}
set
{
OnRemarksing(value);
_remarks = value;
OnRemarksed();
}
}
partial void OnFullNameing(object value);
partial void OnFullNameed();
private object _fullname;
public object FullName
{
get
{
return _fullname;
}
set
{
OnFullNameing(value);
_fullname = value;
OnFullNameed();
}
}
partial void OnNRICing(object value);
partial void OnNRICed();
private object _nric;
public object NRIC
{
get
{
return _nric;
}
set
{
OnNRICing(value);
_nric = value;
OnNRICed();
}
}
partial void OnLANoing(object value);
partial void OnLANoed();
private object _lano;
public object LANo
{
get
{
return _lano;
}
set
{
OnLANoing(value);
_lano = value;
OnLANoed();
}
}
partial void OnPolicyNoing(object value);
partial void OnPolicyNoed();
private object _policyno;
public object PolicyNo
{
get
{
return _policyno;
}
set
{
OnPolicyNoing(value);
_policyno = value;
OnPolicyNoed();
}
}
partial void OnTeling(object value);
partial void OnTeled();
private object _tel;
public object Tel
{
get
{
return _tel;
}
set
{
OnTeling(value);
_tel = value;
OnTeled();
}
}
partial void OnBanking(object value);
partial void OnBanked();
private object _bank;
public object Bank
{
get
{
return _bank;
}
set
{
OnBanking(value);
_bank = value;
OnBanked();
}
}
partial void OnCaseStatusing(object value);
partial void OnCaseStatused();
private object _casestatus;
public object CaseStatus
{
get
{
return _casestatus;
}
set
{
OnCaseStatusing(value);
_casestatus = value;
OnCaseStatused();
}
}
}

public partial class Remarks : ReportObject
{
partial void OnMessageing(object value);
partial void OnMessageed();
private object _message;
public object Message
{
get
{
return _message;
}
set
{
OnMessageing(value);
_message = value;
OnMessageed();
}
}
}



}

