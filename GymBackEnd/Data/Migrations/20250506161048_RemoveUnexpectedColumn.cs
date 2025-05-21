using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymBackEnd.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUnexpectedColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
                  migrationBuilder.DropColumn(
                            name: "PaymentMethodId",
                            table: "Payments");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
