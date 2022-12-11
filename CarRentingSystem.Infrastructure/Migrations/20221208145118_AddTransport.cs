using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CarRentingSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTransport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Transports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PricePerKm = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    DeliveryDays = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transports", x => x.Id);
                });

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

            migrationBuilder.InsertData(
                table: "Transports",
                columns: new[] { "Id", "CompanyName", "DeliveryDays", "IsActive", "PricePerKm" },
                values: new object[,]
                {
                    { 1, "VidaTrans Ltd", 5, true, 2.0m },
                    { 2, "SofiaTrans Ltd", 3, true, 10.0m },
                    { 3, "Speedy Ltd", 1, true, 20.0m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transports");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "16f8b43c-3513-4540-b683-e161e49d6ff7", "AQAAAAEAACcQAAAAEEMXJPstScWwJoMpPZsA/hCYlAjCD+RncN8fyn7mP5niiR0qZB7fxDYLZWxHaO+Q4Q==", "d8d6738d-feae-4ea6-a2dd-628c6181e824" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bcb4f072-ecca-43c9-ab26-c060c6f364e4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b53b99e9-cdc9-4f25-8f3a-b086ab4f2d71", "AQAAAAEAACcQAAAAEGJUxlmMYJUnNMU9rVfzdNmO1tQNMdvIK+Oyw2+hx/qjSR+L+BJHHZpsrlp2m2nAPg==", "f53a1b6c-6d95-4140-8fec-03391381f568" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7e2da239-16d9-42d3-8388-c8462ece92c6", "AQAAAAEAACcQAAAAEM+34rH3tgUJXY/ksSfuBWE3977ZRAUi3zGhlbuJC/6GKIGHRXOMJ7oh0SN75A5e2A==", "969af320-76f5-411d-82cb-d9ec924e95e0" });
        }
    }
}
