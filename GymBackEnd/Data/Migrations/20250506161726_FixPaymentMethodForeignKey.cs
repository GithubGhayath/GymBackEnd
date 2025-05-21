using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymBackEnd.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixPaymentMethodForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

             // 👇 Fix any invalid PaymentMethodId values BEFORE changing schema
                    migrationBuilder.Sql(@"
                        UPDATE Payments
                        SET PaymentMethodId = 1
                        WHERE PaymentMethodId IS NULL
                        OR PaymentMethodId NOT IN (SELECT Id FROM PaymentMethods);
                    ");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_PaymentMethods_PaymentMethodId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "PaymentMehtodID",
                table: "Payments");

            migrationBuilder.RenameColumn(
                name: "PaymentMethodId",
                table: "Payments",
                newName: "PaymentMethodID");

            migrationBuilder.RenameIndex(
                name: "IX_Payments_PaymentMethodId",
                table: "Payments",
                newName: "IX_Payments_PaymentMethodID");

            migrationBuilder.AlterColumn<int>(
                name: "PaymentMethodID",
                table: "Payments",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_PaymentMethods_PaymentMethodID",
                table: "Payments",
                column: "PaymentMethodID",
                principalTable: "PaymentMethods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_PaymentMethods_PaymentMethodID",
                table: "Payments");

            migrationBuilder.RenameColumn(
                name: "PaymentMethodID",
                table: "Payments",
                newName: "PaymentMethodId");

            migrationBuilder.RenameIndex(
                name: "IX_Payments_PaymentMethodID",
                table: "Payments",
                newName: "IX_Payments_PaymentMethodId");

            migrationBuilder.AlterColumn<int>(
                name: "PaymentMethodId",
                table: "Payments",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<int>(
                name: "PaymentMehtodID",
                table: "Payments",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_PaymentMethods_PaymentMethodId",
                table: "Payments",
                column: "PaymentMethodId",
                principalTable: "PaymentMethods",
                principalColumn: "Id");
        }
    }
}
