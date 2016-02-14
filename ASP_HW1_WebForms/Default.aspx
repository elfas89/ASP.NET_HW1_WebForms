<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ASP_HW1_WebForms.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Дом</title>
    <style>
        .component-div {
            border: 2px solid gray;
            float: left;
            margin: 3px;
            padding: 3px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
                <asp:Panel ID="listPanel" runat="server">Доступные компоненты:
                    <asp:DropDownList ID="dropDownComponentList" runat="server">
                        <asp:ListItem>Телевизор</asp:ListItem>
                        <asp:ListItem>Холодильник</asp:ListItem>
                        <asp:ListItem>Печь</asp:ListItem>
                        <asp:ListItem>Духовка</asp:ListItem>
                        <asp:ListItem>Музыкальный центр</asp:ListItem>
                    </asp:DropDownList>

                    <asp:Image ID="image" runat="server" />
                </asp:Panel>
            <br />

                <asp:Panel ID="namePanel" runat="server">
                    Имя компонента:
                    <asp:TextBox ID="nameComponentBox" runat="server"></asp:TextBox>
                    <asp:Label ID="infoLabel" runat="server" Text=""></asp:Label>
                </asp:Panel>
            <br />
                <asp:Panel ID="buttonPanel" runat="server">
                    <asp:Button ID="addComponentButton" runat="server" Text="Добавить компонент умного дома" />
                </asp:Panel>
            <br />

            <asp:Panel ID="сomponentPanel" runat="server" ></asp:Panel>

        </div>
    </form>
</body>
</html>
