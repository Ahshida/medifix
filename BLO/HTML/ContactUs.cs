using System.Collections.Generic;
using HTMLReportCreator;

namespace HTML.ContactUs
{
public partial class MainItem : ReportObject
{
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
partial void OnNameing(object value);
partial void OnNameed();
private object _name;
public object Name
{
get
{
return _name;
}
set
{
OnNameing(value);
_name = value;
OnNameed();
}
}
partial void OnEmailing(object value);
partial void OnEmailed();
private object _email;
public object Email
{
get
{
return _email;
}
set
{
OnEmailing(value);
_email = value;
OnEmailed();
}
}
partial void OnDescriptionLabeling(object value);
partial void OnDescriptionLabeled();
private object _descriptionlabel;
public object DescriptionLabel
{
get
{
return _descriptionlabel;
}
set
{
OnDescriptionLabeling(value);
_descriptionlabel = value;
OnDescriptionLabeled();
}
}
partial void OnDescriptioning(object value);
partial void OnDescriptioned();
private object _description;
public object Description
{
get
{
return _description;
}
set
{
OnDescriptioning(value);
_description = value;
OnDescriptioned();
}
}
}


}

