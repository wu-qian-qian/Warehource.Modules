#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Identity.Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class InitialCreate : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            "Identity.Role",
            table => new
            {
                Id = table.Column<Guid>("uniqueidentifier", nullable: false),
                RoleName = table.Column<string>("nvarchar(50)", maxLength: 50, nullable: false),
                Description = table.Column<string>("nvarchar(max)", nullable: true),
                CreationTime = table.Column<DateTime>("datetime2", nullable: false),
                LastModifierUser = table.Column<string>("nvarchar(max)", nullable: true),
                LastModificationTime = table.Column<DateTime>("datetime2", nullable: true),
                IsDeleted = table.Column<bool>("bit", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Identity.Role", x => x.Id)
                    .Annotation("SqlServer:Clustered", false);
            });

        migrationBuilder.CreateTable(
            "Identity.User",
            table => new
            {
                Id = table.Column<Guid>("uniqueidentifier", nullable: false),
                Name = table.Column<string>("nvarchar(max)", nullable: false),
                Description = table.Column<string>("nvarchar(max)", nullable: true),
                Email = table.Column<string>("nvarchar(max)", nullable: true),
                Phone = table.Column<string>("nvarchar(max)", nullable: true),
                Username = table.Column<string>("nvarchar(50)", maxLength: 50, nullable: false),
                Password = table.Column<string>("nvarchar(20)", maxLength: 20, nullable: false),
                LockoutEnd = table.Column<DateTimeOffset>("datetimeoffset", nullable: false),
                RoleId = table.Column<Guid>("uniqueidentifier", nullable: false),
                CreationTime = table.Column<DateTime>("datetime2", nullable: false),
                LastModifierUser = table.Column<string>("nvarchar(max)", nullable: true),
                LastModificationTime = table.Column<DateTime>("datetime2", nullable: true),
                IsDeleted = table.Column<bool>("bit", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Identity.User", x => x.Id)
                    .Annotation("SqlServer:Clustered", false);
                table.ForeignKey(
                    "FK_Identity.User_Identity.Role_RoleId",
                    x => x.RoleId,
                    "Identity.Role",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
                "IX_Identity.Role_RoleName",
                "Identity.Role",
                "RoleName")
            .Annotation("SqlServer:Clustered", false);

        migrationBuilder.CreateIndex(
            "IX_Identity.User_RoleId",
            "Identity.User",
            "RoleId");

        migrationBuilder.CreateIndex(
                "IX_Identity.User_Username",
                "Identity.User",
                "Username")
            .Annotation("SqlServer:Clustered", false);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            "Identity.User");

        migrationBuilder.DropTable(
            "Identity.Role");
    }
}