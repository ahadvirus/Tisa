using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Tisa.Store.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class ConnectValidatorToAttribute : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AttributeEntityValidators",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    AttributeEntityId = table.Column<int>(type: "int", nullable: false),
                    ValidatorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeEntityValidators", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttributeEntityValidators_AttributeEntities_AttributeEntityId",
                        column: x => x.AttributeEntityId,
                        principalTable: "AttributeEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttributeEntityValidators_Validators_ValidatorId",
                        column: x => x.ValidatorId,
                        principalTable: "Validators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AttributeEntityValidatorClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Key = table.Column<string>(type: "varchar(255)", nullable: false),
                    Value = table.Column<string>(type: "longtext", nullable: false),
                    AttributeEntityValidationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeEntityValidatorClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttributeEntityValidatorClaims_AttributeEntityValidators_Att~",
                        column: x => x.AttributeEntityValidationId,
                        principalTable: "AttributeEntityValidators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_AttributeEntityValidatorClaims_AttributeEntityValidationId_K~",
                table: "AttributeEntityValidatorClaims",
                columns: new[] { "AttributeEntityValidationId", "Key" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AttributeEntityValidatorClaims_Id",
                table: "AttributeEntityValidatorClaims",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_AttributeEntityValidators_AttributeEntityId",
                table: "AttributeEntityValidators",
                column: "AttributeEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_AttributeEntityValidators_ValidatorId",
                table: "AttributeEntityValidators",
                column: "ValidatorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttributeEntityValidatorClaims");

            migrationBuilder.DropTable(
                name: "AttributeEntityValidators");
        }
    }
}
