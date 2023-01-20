using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LocalBusiness.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Businesses",
                columns: table => new
                {
                    BusinessId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Review = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Businesses", x => x.BusinessId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Businesses",
                columns: new[] { "BusinessId", "Description", "Name", "Review" },
                values: new object[,]
                {
                    { 1, "Chinese Restaurant", "Panda Fast", 3 },
                    { 2, "Supermarket", "Walmarket", 4 },
                    { 3, "Mexican Restaurant", "Tortilla Bell", 3 },
                    { 4, "Fast food chicken restaurant", "Girl-Fil-A", 4 },
                    { 5, "Coffee shop", "Sunbucks", 2 },
                    { 6, "Coffee/Donut shop", "Dippin Donuts", 2 },
                    { 7, "Coffee shop", "Moonbucks", 5 },
                    { 8, "Wine store", "Crushed Grapes", 1 },
                    { 9, "American restaurant", "Apple A Plus", 5 },
                    { 10, "American diner", "IJump", 3 },
                    { 11, "Home Improvement Company", "House Depot", 1 },
                    { 12, "Furniture Company", "Bobithy Furniture", 2 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Businesses");
        }
    }
}
