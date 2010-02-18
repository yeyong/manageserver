<%@ Control Language="C#" CodeBehind="onlineeditor.ascx.cs" Inherits="SAS.ManageWeb.ManagePage.onlineeditor" %>
<%@ Register TagPrefix="cc1" Namespace="SAS.Control" Assembly="SAS.Control" %>

<textarea name="DataTextarea" id="DataTextarea" runat="server" cols="80" rows="20"></textarea>
	
<script type="text/javascript" src="../../javascript/common.js"></script>
<script type="text/javascript" src="../../javascript/dnteditor.js"></script>
<script type="text/javascript" src="../../javascript/post.js"></script>
<script type="text/javascript">
    var dntEditor;
      
    function CreateEditor()
    {
        dntEditor = new DNTeditor('<% = DataTextarea.ClientID%>', '80%', '200', document.getElementById('<% = DataTextarea.ClientID%>').value);
        dntEditor.BasePath = '<%=SAS.Config.BaseConfigs.GetSitePath %>';
        dntEditor.ReplaceTextarea();
        document.getElementById("<% = DataTextarea.ClientID%>___Frame").height = "150px";
    }

    function PrepareSave()
    {
        document.getElementById('<% = DataTextarea.ClientID%>').value = dntEditor.GetHtml();
    }

    function validate(theform)
    {
       PrepareSave();
       return true;
    }

    CreateEditor();
</script>