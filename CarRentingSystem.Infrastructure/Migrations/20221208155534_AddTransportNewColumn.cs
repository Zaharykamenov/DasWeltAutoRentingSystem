using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRentingSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTransportNewColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Transports",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Transports",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9c903175-3ca0-4ef8-a06c-3f8b0a5e9045", "AQAAAAEAACcQAAAAEG4T5F5bT7MzAgUPHNMSQpG6QmYjjFdYiUgAU9nhcO14cJ21auVur9bDuVP9vj1jjA==", "d9dbc37f-5872-4d5f-9246-8f57720748f9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bcb4f072-ecca-43c9-ab26-c060c6f364e4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cad86728-1e00-4384-b492-15d6d506357c", "AQAAAAEAACcQAAAAEH80dhOrvtuJost99zMm26xYM5k6xo9sxBf3C/hMMSTKDU3Xr8JvAxKTJZWqbLKNoA==", "a2c9a55f-a4da-4a52-838b-e2bc3db8298b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2dc73998-c53a-4735-8c76-fa2889994e99", "AQAAAAEAACcQAAAAECCD2clrLe5XvPOUMGnTm5lrRbWt2PsTdVVnaKlhH7t+59iQh9szlg1fzOaXkrs9AA==", "8eed1be3-7f31-44bd-bd2b-a6b9f71a2a5a" });

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "ImageUrl" },
                values: new object[] { "Best transport company in the west Bulgaria!", "https://www.vida.se/wp-content/uploads/2018/12/vida_rgb.jpg" });

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "ImageUrl" },
                values: new object[] { "The best transport company in central Bulgaria!", "https://d1yjjnpx0p53s8.cloudfront.net/styles/logo-thumbnail/s3/102017/untitled-3_77.png?Q07xLRtpQwmjEjwxEl7zei_mYpcEEbdE&itok=y1qvbWxC" });

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "ImageUrl" },
                values: new object[] { "The biggest and the fastest transport company in Bulgaria!", "https://www.speedy.bg/uploads/file_manager_uploads/Pics/speedy-logo-4c--cmyk.jpg" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Transports");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Transports");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7966001e-20d1-4f30-9a68-126c707c45a5", "AQAAAAEAACcQAAAAECMakWy/ssHz0Kuv4znvJnm/XcWHFADWdHIWphLuJYiNzcFHgyuvSf3+Uz7A3uknww==", "686e23dc-f5e6-47d3-bcca-49823e4e3779" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bcb4f072-ecca-43c9-ab26-c060c6f364e4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "84223e47-a96b-41d9-88ef-8d25b70846c8", "AQAAAAEAACcQAAAAEF/VgEyR47gXz6EU29YDXVVafyBkJYUmn7SKDjYruAAyVAb4PhSTeNX8qCFHrHNdvw==", "ebc6aa56-08ec-4b82-85d3-132f689919a1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "58f2e511-63e3-4c21-9292-9479a2a85296", "AQAAAAEAACcQAAAAEBZ5+eDPlJOtnp03x26IJNmFd2ZUBtdytaDD9NlEkWfT81bpWIzTgdeZd9capnhASA==", "0f4f3147-1c73-4698-abbe-a8545781ecaa" });
        }
    }
}
