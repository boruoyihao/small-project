<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="display.aspx.cs" Inherits="TestServer.display" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
    <%List<TestServer.User> list = getList();
      Response.Write("第二种：页面response打印输出<br/>");
      Response.Write("<table border='1'>");
      for (int i = 0; i < list.Count; i++)
      {
          Response.Write("<tr>");
          TestServer.User user = list[i];
          Response.Write("<td>"+user.Account + "</td><td>" + user.Email + "</td><td>" + user.Time + "</td>");
          Response.Write("</tr>");
      }
      Response.Write("</table>");
      Response.Write("<hr/>");
       %>
    <p>第三种：页面控件打印输出（最好的方式）<br/></p>
    <asp:Table ID="Table1" runat="server" border="1" >

        <asp:TableRow >

            <asp:TableCell >账户名</asp:TableCell>
            <asp:TableCell >邮箱</asp:TableCell>
            <asp:TableCell >注册时间</asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    
</body>
</html>
