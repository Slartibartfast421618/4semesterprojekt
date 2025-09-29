using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BackendAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTableAndDummy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Lat",
                table: "Hairdressers",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Lng",
                table: "Hairdressers",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "SalonName",
                table: "Hairdressers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Website",
                table: "Hairdressers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Hairdressers",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "Lat", "Lng", "SalonName", "Website" },
                values: new object[] { 55.710031999999998, 9.5347290000000005, "Hairværk", "https://hairvaerkvejle.dk/" });

            migrationBuilder.UpdateData(
                table: "Hairdressers",
                keyColumn: "ID",
                keyValue: 2,
                columns: new[] { "Lat", "Lng", "SalonName", "Website" },
                values: new object[] { 55.612772999999997, 9.5668050000000004, "Salon No. 24", "https://salonno24.dk/" });

            migrationBuilder.InsertData(
                table: "Hairdressers",
                columns: new[] { "ID", "Lat", "Lng", "SalonName", "Website" },
                values: new object[,]
                {
                    { 3, 55.711891000000001, 9.5365300000000008, "Frisør PARK Styling Vejle", "https://parkstyling.dk/pages/salon/vejle" },
                    { 4, 55.710152000000001, 9.5352700000000006, "City Frisørerne", "https://parkstyling.dk/pages/salon/vejle-city" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Hairdressers",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Hairdressers",
                keyColumn: "ID",
                keyValue: 4);

            migrationBuilder.DropColumn(
                name: "Lat",
                table: "Hairdressers");

            migrationBuilder.DropColumn(
                name: "Lng",
                table: "Hairdressers");

            migrationBuilder.DropColumn(
                name: "SalonName",
                table: "Hairdressers");

            migrationBuilder.DropColumn(
                name: "Website",
                table: "Hairdressers");
        }
    }
}
