using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRentingSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIsActiveToEngineCategoryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "EngineCategory",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5bb84c59-01e7-4605-90d7-5902751af4c3", "AQAAAAEAACcQAAAAEGJ5NufYxu6iVmgwv4wXqhruWPIDxRB7KAPP83qCpUBqSfwZHf3OqT+CbyY72bqQ+A==", "c618faa1-a706-4e8e-b3d2-0b7f416bd49d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bcb4f072-ecca-43c9-ab26-c060c6f364e4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7a2095cd-785a-4885-8965-3c6e19b97f54", "AQAAAAEAACcQAAAAEL9aKB7h5r4z0rCOeLfTPhxAV2q9pAX5KsSy+snsa6HQsuorNMqZRqHuNtXGZ2YYYw==", "669c752c-b842-4202-9dd4-f6dafa945630" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "11bea0ab-a6d4-49f1-ba89-0007459e2a43", "AQAAAAEAACcQAAAAEOYhqdSEq1VDeKLEp/pmMo7dUMW6w5Sx2r8kOKndsiShvfBi7cpcp4MWDF/rDPna3w==", "0aebcd8d-daa7-4351-92ae-946fd18fa608" });

            migrationBuilder.UpdateData(
                table: "EngineCategory",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "EngineCategory",
                keyColumn: "Id",
                keyValue: 2,
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "EngineCategory",
                keyColumn: "Id",
                keyValue: 3,
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "EngineCategory",
                keyColumn: "Id",
                keyValue: 4,
                column: "IsActive",
                value: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "EngineCategory");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fac621c3-5e6a-4a2b-b9a4-b4a3663ef64f", "AQAAAAEAACcQAAAAEBwTFh3dPsChBvD8gpEPjJIbSBa9sVjxfckb8GwSWwGHj7KpIYPzqgBqU/06qrfmYQ==", "ba6d31cd-9ee2-4ece-8713-831bae060de0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bcb4f072-ecca-43c9-ab26-c060c6f364e4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d48d7c6a-77b4-4bc5-b439-59e16e1e8c33", "AQAAAAEAACcQAAAAEC2VMpETTcAD9gt1/nDvqhWkD2jBhssGDiXswbMw5CYJ62icGTvRCJOAxndDe5xOfw==", "85796f1c-518f-4a12-9cdf-bda0f201d718" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "03427b78-91d8-4bb1-879b-7e49369802c4", "AQAAAAEAACcQAAAAEEBokWSKjT2BYserCm0SBR2Y1BJ2J8Z11Qk2i9rkBW7h+ULWWd6r3+/x1fIP0U8wng==", "f59ea8e4-4766-45e0-bb3f-4d554465bfdc" });
        }
    }
}
