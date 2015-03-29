<%@ Page Title="Booking" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="BookingTransaction.aspx.cs" Inherits="TravelAgency.BookingTransaction" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True" />
    <h2>
        Booking
    </h2>
    <table>
        <tr>
            <td>
                Booking Type
            </td>
            <td>
                <asp:DropDownList ID="ddlBookingType" runat="server" OnSelectedIndexChanged="ddlBookingType_OnSelectedIndexChanged"
                    AutoPostBack="true">
                    <asp:ListItem Text="Select..." Value="Select" Selected="True"></asp:ListItem>
                    <%--<asp:ListItem Text="All" Value="All"></asp:ListItem>--%>
                    <asp:ListItem Text="Airways" Value="Airways"></asp:ListItem>
                    <asp:ListItem Text="Hotel" Value="Hotel"></asp:ListItem>
                    <asp:ListItem Text="Car" Value="Car"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <div id="divAirlines" runat="server" visible="false">
        <h2>
            Airline
        </h2>
        <table>
            <tr>
                <td>
                    Airlines
                </td>
                <td>
                    <asp:DropDownList ID="ddlAirlines" runat="server" OnSelectedIndexChanged="ddlAirlines_OnSelectedIndexChanged"
                        AutoPostBack="true">
                    </asp:DropDownList>
                </td>
                <td colspan="6">
                </td>
            </tr>
            <tr>
                <td>
                    Airbus
                </td>
                <td>
                    <asp:DropDownList ID="ddlAirbus" runat="server" OnSelectedIndexChanged="ddlAirbus_OnSelectedIndexChanged"
                        AutoPostBack="true">
                    </asp:DropDownList>
                </td>
                <td colspan="6">
                </td>
            </tr>
            <tr>
                <td>
                    From
                </td>
                <td>
                    <asp:DropDownList ID="ddlAirbusFrom" runat="server">
                        <asp:ListItem Text="California" Value="California" Selected="True"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    To
                </td>
                <td>
                    <asp:DropDownList ID="ddlAirbusTo" runat="server">
                        <asp:ListItem Text="Texas" Value="Texas" Selected="True"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td colspan="4">
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
                <td>
                    Travel Date
                </td>
                <td>
                    <asp:TextBox ID="txtTravelDate" runat="server"></asp:TextBox>
                    <cc1:CalendarExtender ID="txtTravelDate_CalendarExtender" runat="server" Enabled="True"
                        TargetControlID="txtTravelDate">
                    </cc1:CalendarExtender>
                    <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                    <asp:RequiredFieldValidator ID="reqvTravelDate" runat="server" ControlToValidate="txtTravelDate"
                        Display="Dynamic" ErrorMessage="Travel Date is required." SetFocusOnError="True"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="validatorEndDate" runat="server" ErrorMessage="Invalid date format."
                        Operator="DataTypeCheck" Type="Date" ControlToValidate="txtTravelDate" Display="Dynamic"
                        meta:resourcekey="validatorEndDateResource1"></asp:CompareValidator>
                    <asp:RangeValidator ID="valTravelDate" runat="server" ControlToValidate="txtTravelDate"
                        Type="Date" ErrorMessage="Date value cannot be less than today" Display="Dynamic"
                        SetFocusOnError="True" MaximumValue="1/1/3999" MinimumValue="1/1/1900"></asp:RangeValidator>
                </td>
                <td colspan="4">
                    <asp:Button ID="btnAirlineBookedSearch" runat="server" Text="Search Availability"
                        OnClick="btnAirlineBookedSearch_OnClick" />
                </td>
            </tr>
            <tr>
                <td>
                    Available Seat Numbers
                </td>
                <td>
                    <asp:DropDownList ID="ddlAvailableSeatNumbers" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    SSN
                </td>
                <td>
                    <asp:TextBox ID="txtSSN" runat="server"></asp:TextBox>
                </td>
                <td>
                </td>
                <td>
                    <asp:Button ID="btnAirbusSeatBook" runat="server" Text="Book" OnClick="btnAirbusSeatBook_OnClick" />
                </td>
                <td colspan="2">
                    <asp:Label ID="lblBError" runat="server" ForeColor="Red"></asp:Label>
                    <asp:Label ID="lblTicketNumber" runat="server" ForeColor="Green"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Amount
                </td>
                <td colspan="7">
                    <asp:TextBox ID="txtPaidAmount" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="8">
                    <asp:Button ID="btnAlreadyBookedTickets" runat="server" Text="Booked Tickets" OnClick="btnAlreadyBookedTickets_OnClick" />
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <asp:GridView ID="gvAirlineBookedSeats" CssClass="Grid" Width="100%" AutoGenerateColumns="False"
                        AllowPaging="True" runat="server" OnPageIndexChanging="gvAirlineBookedSeats_PageIndexChanging"
                        OnRowDataBound="gvAirlineBookedSeats_RowDataBound" OnRowCommand="gvAirlineBookedSeats_RowCommand">
                        <AlternatingRowStyle CssClass="GridAltRow" />
                        <Columns>
                            <asp:TemplateField HeaderText="Ticket Number">
                                <ItemTemplate>
                                    <asp:Label ID="lblTicketNumber" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.id")%>'></asp:Label>
                                    <asp:HiddenField ID="hdnIsEditable" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.is_locked")%>' />
                                </ItemTemplate>
                                <HeaderStyle Width="10%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Left" Width="10%" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Airbus Code">
                                <ItemTemplate>
                                    <asp:Label ID="lblAirbusCode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.airbus_code")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="11%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Left" Width="11%" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Seat Number">
                                <ItemTemplate>
                                    <asp:Label ID="lblSeatNumber" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.seat_number")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="8%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Left" Width="8%" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Travel Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblTravelDate" runat="server" Text='<%# Bind("travel_date", " {0:MM-dd-yyyy hh:mm tt}")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="15%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Center" Width="15%" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Passenger SSN">
                                <ItemTemplate>
                                    <asp:Label ID="lblPassengerSSN" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.passenger_ssn")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="26%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Left" Width="26%" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Booked By">
                                <ItemTemplate>
                                    <asp:Label ID="lblBookedBy" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.booked_by")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="26%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Left" Width="26%" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Confirmed By">
                                <ItemTemplate>
                                    <asp:Label ID="lblConfirmedBy" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.confirmed_by")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="26%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Left" Width="26%" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Paid Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lblPaidAmount" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.paid_amount")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="26%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Left" Width="26%" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Update">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgBtnEdit" ToolTip="Update Ticket" CommandName="Update Ticket"
                                        CommandArgument='<%# Bind("id")%>' runat="server" src="../../App_Themes/Images/iconEdit.gif">
                                    </asp:ImageButton>
                                </ItemTemplate>
                                <HeaderStyle Width="5%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Center" Width="4%" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Confirm">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgBtnConfirm" ToolTip="Confirm" CommandName="Confirm" CommandArgument='<%# Bind("id")%>'
                                        runat="server" src="../../App_Themes/Images/audioSave.gif"></asp:ImageButton>
                                </ItemTemplate>
                                <HeaderStyle Width="5%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Center" Width="4%" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Delete">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgBtnDelete" ToolTip="Delete" CommandName="Delete Ticket" CommandArgument='<%# Bind("id")%>'
                                        runat="server" src="../../App_Themes/Images/iconRemove.gif"></asp:ImageButton>
                                </ItemTemplate>
                                <HeaderStyle Width="5%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Center" Width="4%" VerticalAlign="Middle" />
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="GridHeader" />
                        <PagerStyle CssClass="GridPager" />
                        <RowStyle CssClass="GridRow" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
    <div id="divHotel" runat="server" visible="false">
        <h2>
            Hotels
        </h2>
        <table>
            <tr>
                <td>
                    Hotels
                </td>
                <td>
                    <asp:DropDownList ID="ddlHotels" runat="server" OnSelectedIndexChanged="ddlHotels_OnSelectedIndexChanged"
                        AutoPostBack="true">
                    </asp:DropDownList>
                </td>
                <td colspan="6">
                </td>
            </tr>
            <tr>
                <td>
                    Rooms
                </td>
                <td>
                    <asp:DropDownList ID="ddlRooms" runat="server" OnSelectedIndexChanged="ddlRooms_OnSelectedIndexChanged"
                        AutoPostBack="true">
                    </asp:DropDownList>
                </td>
                <td colspan="6">
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
                <td>
                    Booking Date
                </td>
                <td>
                    <asp:TextBox ID="txtRoomBookDate" runat="server"></asp:TextBox>
                    <cc1:CalendarExtender ID="txtRoomBookDate_CalendarExtender" runat="server" Enabled="True"
                        TargetControlID="txtRoomBookDate">
                    </cc1:CalendarExtender>
                    <asp:Label ID="Label2" runat="server" ForeColor="Red"></asp:Label>
                    <asp:RequiredFieldValidator ID="reqvRoomTravelDate" runat="server" ControlToValidate="txtRoomBookDate"
                        Display="Dynamic" ErrorMessage="Travel Date is required." SetFocusOnError="True"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="validatorRoomEndDate" runat="server" ErrorMessage="Invalid date format."
                        Operator="DataTypeCheck" Type="Date" ControlToValidate="txtRoomBookDate" Display="Dynamic"
                        meta:resourcekey="validatorEndDateResource1"></asp:CompareValidator>
                    <asp:RangeValidator ID="valRoomTravelDate" runat="server" ControlToValidate="txtRoomBookDate"
                        Type="Date" ErrorMessage="Date value cannot be less than today" Display="Dynamic"
                        SetFocusOnError="True" MaximumValue="1/1/3999" MinimumValue="1/1/1900"></asp:RangeValidator>
                </td>
                <td colspan="4">
                    <asp:Button ID="btnRoomBookedSearch" runat="server" Text="Search Availability" OnClick="btnRoomBookedSearch_OnClick" />
                </td>
            </tr>
            <tr>
                <td>
                    Available Rooms
                </td>
                <td>
                    <asp:DropDownList ID="ddlAvailableRooms" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    SSN
                </td>
                <td>
                    <asp:TextBox ID="txtPassSSNRoom" runat="server"></asp:TextBox>
                </td>
                <td>
                </td>
                <td>
                    <asp:Button ID="btnRoomBook" runat="server" Text="Book" OnClick="btnRoomBook_OnClick" />
                </td>
                <td colspan="2">
                    <asp:Label ID="lblRoomError" runat="server" ForeColor="Red"></asp:Label>
                    <asp:Label ID="lblRoomBookRefNumber" runat="server" ForeColor="Green"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Amount
                </td>
                <td colspan="7">
                    <asp:TextBox ID="txtRoomAmountPaid" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="8">
                    <asp:Button ID="btnAlreadyBookedRooms" runat="server" Text="Booked Rooms" OnClick="btnAlreadyBookedRooms_OnClick" />
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <asp:GridView ID="gvBookedRooms" CssClass="Grid" Width="100%" AutoGenerateColumns="False"
                        AllowPaging="True" runat="server" OnPageIndexChanging="gvBookedRooms_PageIndexChanging"
                        OnRowDataBound="gvBookedRooms_RowDataBound" OnRowCommand="gvBookedRooms_RowCommand">
                        <AlternatingRowStyle CssClass="GridAltRow" />
                        <Columns>
                            <asp:TemplateField HeaderText="Booking Id">
                                <ItemTemplate>
                                    <asp:Label ID="lblTicketNumber" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.room_booking_id")%>'></asp:Label>
                                    <asp:HiddenField ID="hdnIsEditable" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.is_locked")%>' />
                                </ItemTemplate>
                                <HeaderStyle Width="10%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Left" Width="10%" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Room Number">
                                <ItemTemplate>
                                    <asp:Label ID="lblAirbusCode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.room_number")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="11%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Left" Width="11%" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Booking Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblTravelDate" runat="server" Text='<%# Bind("book_date", " {0:MM-dd-yyyy hh:mm tt}")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="15%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Center" Width="15%" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Passenger SSN">
                                <ItemTemplate>
                                    <asp:Label ID="lblPassengerSSN" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.passenger_ssn")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="26%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Left" Width="26%" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Booked By">
                                <ItemTemplate>
                                    <asp:Label ID="lblBookedBy" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.booked_by")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="26%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Left" Width="26%" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Confirmed By">
                                <ItemTemplate>
                                    <asp:Label ID="lblConfirmedBy" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.confirmed_by")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="26%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Left" Width="26%" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Paid Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lblPaidAmount" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.paid_amount")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="26%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Left" Width="26%" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Update">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgBtnEdit" ToolTip="Update" CommandName="Update RoomBId" CommandArgument='<%# Bind("room_booking_id")%>'
                                        runat="server" src="../../App_Themes/Images/iconEdit.gif"></asp:ImageButton>
                                </ItemTemplate>
                                <HeaderStyle Width="5%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Center" Width="4%" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Confirm">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgBtnConfirm" ToolTip="Confirm" CommandName="Confirm" CommandArgument='<%# Bind("room_booking_id")%>'
                                        runat="server" src="../../App_Themes/Images/audioSave.gif"></asp:ImageButton>
                                </ItemTemplate>
                                <HeaderStyle Width="5%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Center" Width="4%" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Delete">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgBtnDelete" ToolTip="Delete" CommandName="Delete RoomBId"
                                        CommandArgument='<%# Bind("room_booking_id")%>' runat="server" src="../../App_Themes/Images/iconRemove.gif">
                                    </asp:ImageButton>
                                </ItemTemplate>
                                <HeaderStyle Width="5%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Center" Width="4%" VerticalAlign="Middle" />
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="GridHeader" />
                        <PagerStyle CssClass="GridPager" />
                        <RowStyle CssClass="GridRow" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
    <div id="divCar" runat="server" visible="false">
        <h2>
            Car Rentals
        </h2>
        <table>
            <tr>
                <td>
                    Car Rentals
                </td>
                <td>
                    <asp:DropDownList ID="ddlCarRentals" runat="server" OnSelectedIndexChanged="ddlCarRentals_OnSelectedIndexChanged"
                        AutoPostBack="true">
                    </asp:DropDownList>
                </td>
                <td colspan="6">
                </td>
            </tr>
            <tr>
                <td>
                    Taxi
                </td>
                <td>
                    <asp:DropDownList ID="ddlCars" runat="server" OnSelectedIndexChanged="ddlCars_OnSelectedIndexChanged"
                        AutoPostBack="true">
                    </asp:DropDownList>
                </td>
                <td colspan="6">
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
                <td>
                    Booking Date
                </td>
                <td>
                    <asp:TextBox ID="txtCarBookDate" runat="server"></asp:TextBox>
                    <cc1:CalendarExtender ID="txtCarBookDate_CalendarExtender" runat="server" Enabled="True"
                        TargetControlID="txtCarBookDate">
                    </cc1:CalendarExtender>
                    <asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label>
                    <asp:RequiredFieldValidator ID="reqvCarTravelDate" runat="server" ControlToValidate="txtCarBookDate"
                        Display="Dynamic" ErrorMessage="Travel Date is required." SetFocusOnError="True"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="validatorCarEndDate" runat="server" ErrorMessage="Invalid date format."
                        Operator="DataTypeCheck" Type="Date" ControlToValidate="txtCarBookDate" Display="Dynamic"
                        meta:resourcekey="validatorEndDateResource1"></asp:CompareValidator>
                    <asp:RangeValidator ID="valCarTravelDate" runat="server" ControlToValidate="txtCarBookDate"
                        Type="Date" ErrorMessage="Date value cannot be less than today" Display="Dynamic"
                        SetFocusOnError="True" MaximumValue="1/1/3999" MinimumValue="1/1/1900"></asp:RangeValidator>
                </td>
                <td colspan="4">
                    <asp:Button ID="btnCarBookedSearch" runat="server" Text="Search Availability" OnClick="btnCarBookedSearch_OnClick" />
                </td>
            </tr>
            <tr>
                <td>
                    Available Cars
                </td>
                <td>
                    <asp:DropDownList ID="ddlAvailableCars" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    SSN
                </td>
                <td>
                    <asp:TextBox ID="txtPassSSNCar" runat="server"></asp:TextBox>
                </td>
                <td>
                </td>
                <td>
                    <asp:Button ID="btnCarBook" runat="server" Text="Book" OnClick="btnCarBook_OnClick" />
                </td>
                <td colspan="2">
                    <asp:Label ID="lblCarError" runat="server" ForeColor="Red"></asp:Label>
                    <asp:Label ID="lblCarBookRefNumber" runat="server" ForeColor="Green"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Amount
                </td>
                <td colspan="7">
                    <asp:TextBox ID="txtCarAmountPaid" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="8">
                    <asp:Button ID="btnAlreadyBookedCars" runat="server" Text="Booked Cars" OnClick="btnAlreadyBookedCars_OnClick" />
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <asp:GridView ID="gvBookedCars" CssClass="Grid" Width="100%" AutoGenerateColumns="False"
                        AllowPaging="True" runat="server" OnPageIndexChanging="gvBookedCars_PageIndexChanging"
                        OnRowDataBound="gvBookedCars_RowDataBound" OnRowCommand="gvBookedCars_RowCommand">
                        <AlternatingRowStyle CssClass="GridAltRow" />
                        <Columns>
                            <asp:TemplateField HeaderText="Booking Id">
                                <ItemTemplate>
                                    <asp:Label ID="lblTicketNumber" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.car_booking_id")%>'></asp:Label>
                                    <asp:HiddenField ID="hdnIsEditable" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.is_locked")%>' />
                                </ItemTemplate>
                                <HeaderStyle Width="10%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Left" Width="10%" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Car Number">
                                <ItemTemplate>
                                    <asp:Label ID="lblAirbusCode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.car_number")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="11%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Left" Width="11%" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Booking Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblTravelDate" runat="server" Text='<%# Bind("book_date", " {0:MM-dd-yyyy hh:mm tt}")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="15%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Center" Width="15%" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Passenger SSN">
                                <ItemTemplate>
                                    <asp:Label ID="lblPassengerSSN" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.passenger_ssn")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="26%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Left" Width="26%" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Booked By">
                                <ItemTemplate>
                                    <asp:Label ID="lblBookedBy" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.booked_by")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="26%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Left" Width="26%" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Confirmed By">
                                <ItemTemplate>
                                    <asp:Label ID="lblConfirmedBy" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.confirmed_by")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="26%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Left" Width="26%" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Paid Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lblPaidAmount" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.paid_amount")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="26%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Left" Width="26%" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Update">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgBtnEdit" ToolTip="Update" CommandName="Update CarBId" CommandArgument='<%# Bind("car_booking_id")%>'
                                        runat="server" src="../../App_Themes/Images/iconEdit.gif"></asp:ImageButton>
                                </ItemTemplate>
                                <HeaderStyle Width="5%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Center" Width="4%" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Confirm">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgBtnConfirm" ToolTip="Confirm" CommandName="Confirm" CommandArgument='<%# Bind("car_booking_id")%>'
                                        runat="server" src="../../App_Themes/Images/audioSave.gif"></asp:ImageButton>
                                </ItemTemplate>
                                <HeaderStyle Width="5%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Center" Width="4%" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Delete">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgBtnDelete" ToolTip="Delete" CommandName="Delete CarBId" CommandArgument='<%# Bind("car_booking_id")%>'
                                        runat="server" src="../../App_Themes/Images/iconRemove.gif"></asp:ImageButton>
                                </ItemTemplate>
                                <HeaderStyle Width="5%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Center" Width="4%" VerticalAlign="Middle" />
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="GridHeader" />
                        <PagerStyle CssClass="GridPager" />
                        <RowStyle CssClass="GridRow" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
