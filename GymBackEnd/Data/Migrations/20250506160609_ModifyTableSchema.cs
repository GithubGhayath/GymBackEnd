using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymBackEnd.Data.Migrations
{
    /// <inheritdoc />
    public partial class ModifyTableSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subscribers_WorkoutPlans_WorkoutPlanId",
                table: "Subscribers");

            migrationBuilder.DropIndex(
                name: "IX_Subscribers_WorkoutPlanId",
                table: "Subscribers");

            migrationBuilder.DropColumn(
                name: "WorkPlanID",
                table: "Subscribers");

            migrationBuilder.DropColumn(
                name: "WorkoutPlanId",
                table: "Subscribers");

            migrationBuilder.AddColumn<int>(
                name: "SubscirberID",
                table: "WorkoutPlans",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SubscriberId",
                table: "WorkoutPlans",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutPlans_SubscriberId",
                table: "WorkoutPlans",
                column: "SubscriberId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutPlans_Subscribers_SubscriberId",
                table: "WorkoutPlans",
                column: "SubscriberId",
                principalTable: "Subscribers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutPlans_Subscribers_SubscriberId",
                table: "WorkoutPlans");

            migrationBuilder.DropIndex(
                name: "IX_WorkoutPlans_SubscriberId",
                table: "WorkoutPlans");

            migrationBuilder.DropColumn(
                name: "SubscirberID",
                table: "WorkoutPlans");

            migrationBuilder.DropColumn(
                name: "SubscriberId",
                table: "WorkoutPlans");

            migrationBuilder.AddColumn<int>(
                name: "WorkPlanID",
                table: "Subscribers",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WorkoutPlanId",
                table: "Subscribers",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subscribers_WorkoutPlanId",
                table: "Subscribers",
                column: "WorkoutPlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subscribers_WorkoutPlans_WorkoutPlanId",
                table: "Subscribers",
                column: "WorkoutPlanId",
                principalTable: "WorkoutPlans",
                principalColumn: "Id");
        }
    }
}
