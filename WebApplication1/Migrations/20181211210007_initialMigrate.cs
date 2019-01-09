using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Migrations
{
    public partial class initialMigrate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cuisin",
                columns: table => new
                {
                    cui_name = table.Column<string>(unicode: false, maxLength: 40, nullable: false),
                    descript = table.Column<string>(unicode: false, maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cuisin", x => x.cui_name);
                });

            migrationBuilder.CreateTable(
                name: "resturant",
                columns: table => new
                {
                    rest_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    rest_name = table.Column<string>(unicode: false, maxLength: 40, nullable: true),
                    rest_address = table.Column<string>(unicode: false, maxLength: 40, nullable: true),
                    phone_number = table.Column<string>(unicode: false, maxLength: 40, nullable: true),
                    email = table.Column<string>(unicode: false, maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_resturant", x => x.rest_id);
                });

            migrationBuilder.CreateTable(
                name: "resturant_cuisin",
                columns: table => new
                {
                    rest_id = table.Column<int>(nullable: false),
                    cui_name = table.Column<string>(unicode: false, maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_resturant_cuisin", x => new { x.rest_id, x.cui_name });
                    table.ForeignKey(
                        name: "FK__resturant__cui_n__534D60F1",
                        column: x => x.cui_name,
                        principalTable: "cuisin",
                        principalColumn: "cui_name",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__resturant__rest___52593CB8",
                        column: x => x.rest_id,
                        principalTable: "resturant",
                        principalColumn: "rest_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_resturant_cuisin_cui_name",
                table: "resturant_cuisin",
                column: "cui_name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "resturant_cuisin");

            migrationBuilder.DropTable(
                name: "cuisin");

            migrationBuilder.DropTable(
                name: "resturant");
        }
    }
}
