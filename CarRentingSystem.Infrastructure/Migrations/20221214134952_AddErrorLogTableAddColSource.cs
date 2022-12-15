using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRentingSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddErrorLogTableAddColSource : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Source",
                table: "ErrorLogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "790ba56b-6308-44da-8e6b-2bc4af6f57d3", "AQAAAAEAACcQAAAAEIqlIh7rkQI6Ue6++w675NN+R+KJ4Ar2YgFaW5TgUp9JSNIzZakJWheToFhZXcWxMA==", "2eaf80c5-c428-4c62-a8b2-813df1c67170" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bcb4f072-ecca-43c9-ab26-c060c6f364e4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "26910789-e5ac-4f09-b130-e78cb4dee518", "AQAAAAEAACcQAAAAEJqXgiaz9PfsyTbfhAYYBz1LcB5tT0BAVqJQQjDFnw7Pv8WmiP4TzkN9obgfICeCjg==", "73721b49-d1d3-4bbd-a8c1-f986bb4aac39" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b21db360-358a-4c01-8b4a-08d4e66543bf", "AQAAAAEAACcQAAAAECfpQ8PyCR2pTG4ruPf2JUaxpOkmKRi55tuwH+xTIbOU0Xgf5JktulAGZrgLXnPznQ==", "5a050bd5-f075-4e9b-b45d-f40fd7ce1326" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Source",
                table: "ErrorLogs");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c5ad986c-85e8-4406-8cd5-d959b289584e", "AQAAAAEAACcQAAAAEIjxnh846Xg2AxJ9Wo1VD/6sczQHed6kmyzrLSpSGPFpRvU9KeRtvmhNd1S7VCbUJA==", "5fd7631b-17c0-4625-89b5-9405ddbc5c06" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bcb4f072-ecca-43c9-ab26-c060c6f364e4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "73c2efaa-e90f-4687-9dd1-07e6429002ea", "AQAAAAEAACcQAAAAEP8SKMIr5mRlf6MfBm26hmS3eqhkXPKRRqCZoDfUtUf5qqcDZFTu/LoqincEgvGloA==", "53fd52dd-8ca3-4297-a5d0-a909b6c46546" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7bb1201b-110e-4030-8eec-53d9c052e31c", "AQAAAAEAACcQAAAAEDa6dMk2MzIn3CGzeGoLMwYLgruDuHrf0BkUtUPf9Lq6b+fUqGEnATTVRELvRvANsg==", "2271bcc0-3223-443a-a631-39f94afac9ee" });
        }
    }
}
