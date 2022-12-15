using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRentingSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddErrorLogTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ErrorLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegistedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    User = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErrorLogs", x => x.Id);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ErrorLogs");

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
        }
    }
}
