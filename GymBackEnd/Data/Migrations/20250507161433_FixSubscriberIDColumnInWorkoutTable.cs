using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymBackEnd.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixSubscriberIDColumnInWorkoutTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                        UPDATE WorkoutPlans
                        SET SubscriberId = 2
                        WHERE SubscriberId IS NULL
                        OR SubscriberId NOT IN (SELECT Id FROM Subscribers);
                    ");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutPlans_Subscribers_SubscriberId",
                table: "WorkoutPlans");

            migrationBuilder.DropColumn(
                name: "SubscirberID",
                table: "WorkoutPlans");

            migrationBuilder.RenameColumn(
                name: "SubscriberId",
                table: "WorkoutPlans",
                newName: "SubscriberID");

            migrationBuilder.RenameIndex(
                name: "IX_WorkoutPlans_SubscriberId",
                table: "WorkoutPlans",
                newName: "IX_WorkoutPlans_SubscriberID");

            migrationBuilder.AlterColumn<int>(
                name: "SubscriberID",
                table: "WorkoutPlans",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutPlans_Subscribers_SubscriberID",
                table: "WorkoutPlans",
                column: "SubscriberID",
                principalTable: "Subscribers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutPlans_Subscribers_SubscriberID",
                table: "WorkoutPlans");

            migrationBuilder.RenameColumn(
                name: "SubscriberID",
                table: "WorkoutPlans",
                newName: "SubscriberId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkoutPlans_SubscriberID",
                table: "WorkoutPlans",
                newName: "IX_WorkoutPlans_SubscriberId");

            migrationBuilder.AlterColumn<int>(
                name: "SubscriberId",
                table: "WorkoutPlans",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<int>(
                name: "SubscirberID",
                table: "WorkoutPlans",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutPlans_Subscribers_SubscriberId",
                table: "WorkoutPlans",
                column: "SubscriberId",
                principalTable: "Subscribers",
                principalColumn: "Id");
        }
    }
}
