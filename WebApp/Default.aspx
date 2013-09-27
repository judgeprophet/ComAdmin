<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="GZM.COMAdmin.WebApp._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script type="text/javascript">
        function SelectAllCheckboxesSpecific(spanChk)
        {
            var IsChecked = spanChk.checked;
            var Chk = spanChk;

            Parent = document.getElementById('gvComApp');
            var items = Parent.getElementsByTagName('input');
            for (i = 0; i < items.length; i++)
            {
                if (items[i].id != Chk && items[i].type == "checkbox")
                {
                    if (items[i].checked != IsChecked)
                    {
                        items[i].click();
                    }
                }
            }
        }

        function HighlightRow(chkB)
        {
            var IsChecked = chkB.checked;

            if (IsChecked)
            {
                chkB.parentElement.parentElement.style.backgroundColor = '#228b22';
                chkB.parentElement.parentElement.style.color = 'white';
            }
            else
            {
                chkB.parentElement.parentElement.style.backgroundColor = 'white';
                chkB.parentElement.parentElement.style.color = 'black';
            }
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table width="100%">
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Font-Bold="True" Text="Application(s) chargée(s) :"></asp:Label>
                    </td>
                    <td valign="middle">
                        <asp:Label ID="lblMinHangTime" runat="server" Text="Temps (ms) min. pour App Inactive (en rouge)"></asp:Label>
                        <asp:TextBox ID="txtMinHangTime" runat="server" />
                        <asp:Button ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" Text="Refresh" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:GridView ID="gvComApp" runat="server" CellPadding="4" AutoGenerateColumns="False" DataKeyNames="ApplicationName" Width="100%" BackColor="White" BorderColor="#3366CC" BorderStyle="Solid" BorderWidth="1px" OnRowCreated="gvComApp_RowCreated">
                            <RowStyle BackColor="White" />
                            <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                            <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                            <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                            <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                            <Columns>
                                <asp:TemplateField HeaderText="Roles" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkAll" onclick="javascript:SelectAllCheckboxesSpecific(this);" runat="server" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelect" onclick="javascript:HighlightRow(this);" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="ApplicationName" HeaderText="Nom Application" SortExpression="ApplicationName" ItemStyle-HorizontalAlign="Left"></asp:BoundField>
                                <asp:BoundField DataField="NbClass" HeaderText="Classes par App" SortExpression="NbClass" ItemStyle-HorizontalAlign="Right"></asp:BoundField>
                                <asp:BoundField DataField="TotalResponseTime" HeaderText="Total Temps reponse (ms)" SortExpression="TotalResponseTime" ItemStyle-HorizontalAlign="Right"></asp:BoundField>
                                <asp:BoundField DataField="TotalInCall" HeaderText="En Utilisation (nb appel)" SortExpression="TotalInCall" ItemStyle-HorizontalAlign="Right"></asp:BoundField>
                                <asp:BoundField DataField="isHang" HeaderText="Gelé" SortExpression="isHang" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnResetSelected" runat="server" OnClick="btnResetSelected_Click" Text="Arrêter" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lblResetNote" runat="server" Text="NOTE: L'arrêt de composantes peut prendre plusieurs minutes.  Veuillez attendre le message de confirmation."></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
