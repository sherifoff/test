@model EstateItemListViewModel

@{
    ViewBag.Title = "EstateItems";
    Layout = "_Layout";
}

@section Body{

<div class="container" asp-action="Search" method="post" enctype="multipart/form-data">

    @using (Html.BeginForm("Search", "Home", FormMethod.Get))
    {
        <b>Search for</b>

        @*<br />
            @Html.RadioButton("searchBy", "Location", true) <text> Location </text>*@

        <br />
        <label> Location</label>
        @Html.TextBox("search", null, new { @class = "btnselect" })

        <br />
        <label> Price</label>
        @Html.TextBox("price", null, new { @class = "btnselect" })

        <br />
        <label> HouseType</label>
        @Html.TextBox("housetype", null, new { @class = "btnselect" })

        <br />
        <label> Property</label>
        @Html.TextBox("property", null, new { @class = "btnselect" })

        <br />
        <label> Currency</label>
        @Html.TextBox("currency", null, new { @class = "btnselect" })


        <br />
        <br />
        <input type="submit" class="button" value="Search" />
    }
</div>


<div class="container">

    @foreach (var estate in Model.EstateItems)
    {

        @await Component.InvokeAsync("RealEstate", estate)

        @*<a asp-action="Details" asp-route-id="@estate.Id">Details</a>*@

        @*<br />
            <a asp-action="Edit" asp-route-id="@estate.Id">Edit</a>*@

        @*<form asp-action="Delete" method="post">
                <input type="hidden" name="Id" value="@estate.Id" />
                <button type="submit">Delete</button>
            </form>
        *@

    }
</div>
}

<br />
<div page-model="@Model.PagingInfo" page-action="Index" page-classes-enabled="true"
     page-class="button" page-class-normal="btn-default"
     page-class-selected="btn-primary"
     class="btn-group pull-right m-1 container">
</div>


