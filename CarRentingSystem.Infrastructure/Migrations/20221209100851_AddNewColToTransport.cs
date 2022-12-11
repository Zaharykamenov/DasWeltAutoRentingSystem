using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRentingSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNewColToTransport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AgentId",
                table: "Transports",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "RenterId",
                table: "Transports",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3ce6228e-a94b-4747-91ce-e168bdaaeec5", "AQAAAAEAACcQAAAAECF7EdCTr7kfvBOE6SIAsrqNjK3gRp0Iz6X8FVEyKuvxo8ZzvOwCxk4WpsnV+Vkh1A==", "762b2809-540c-4d0c-820d-cb39c5e0ed8e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bcb4f072-ecca-43c9-ab26-c060c6f364e4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "439a43b0-e326-46c9-a35f-0e0a8dd3a48d", "AQAAAAEAACcQAAAAEGJIyu8LIlMmXge/ohwmTFnyZJ+ZgHy9MjWMwPF+H0656UFB4NirdSdi+t0oLfoszQ==", "36a1f85a-c0c2-47cb-95df-ecf0268596ec" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6a2434d1-aa93-45da-a778-71d2d742e992", "AQAAAAEAACcQAAAAENGZUwIjrjoLPWTUTSlugy6xBn4qDBqv00w5ISkDPTsXL/pOVirURrhhDlhTIj0uOg==", "e3ef4034-407b-4d24-9c08-875fee9c09cb" });

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AgentId", "RenterId" },
                values: new object[] { 1, null });

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AgentId", "RenterId" },
                values: new object[] { 2, null });

            migrationBuilder.UpdateData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "AgentId", "RenterId" },
                values: new object[] { 1, null });

            migrationBuilder.CreateIndex(
                name: "IX_Transports_AgentId",
                table: "Transports",
                column: "AgentId");

            migrationBuilder.CreateIndex(
                name: "IX_Transports_RenterId",
                table: "Transports",
                column: "RenterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transports_Agents_AgentId",
                table: "Transports",
                column: "AgentId",
                principalTable: "Agents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transports_AspNetUsers_RenterId",
                table: "Transports",
                column: "RenterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transports_Agents_AgentId",
                table: "Transports");

            migrationBuilder.DropForeignKey(
                name: "FK_Transports_AspNetUsers_RenterId",
                table: "Transports");

            migrationBuilder.DropIndex(
                name: "IX_Transports_AgentId",
                table: "Transports");

            migrationBuilder.DropIndex(
                name: "IX_Transports_RenterId",
                table: "Transports");

            migrationBuilder.DropColumn(
                name: "AgentId",
                table: "Transports");

            migrationBuilder.DropColumn(
                name: "RenterId",
                table: "Transports");

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
        }
    }
}
