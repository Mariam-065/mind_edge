using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MindEdge_1.Migrations
{
    /// <inheritdoc />
    public partial class UpdateChatTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatbotRooms_Users_UserId",
                table: "ChatbotRooms");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_ChatbotRooms_ChatbotRoomId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_ChatbotRoomId",
                table: "Messages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChatbotRooms",
                table: "ChatbotRooms");

            migrationBuilder.DropIndex(
                name: "IX_ChatbotRooms_UserId",
                table: "ChatbotRooms");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ChatbotRooms");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "ChatbotRooms");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ChatbotRooms");

            migrationBuilder.AddColumn<string>(
                name: "ChatbotRoomSessionId",
                table: "Messages",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SessionId",
                table: "ChatbotRooms",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "ChatbotRooms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChatbotRooms",
                table: "ChatbotRooms",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ChatbotRoomSessionId",
                table: "Messages",
                column: "ChatbotRoomSessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_ChatbotRooms_ChatbotRoomSessionId",
                table: "Messages",
                column: "ChatbotRoomSessionId",
                principalTable: "ChatbotRooms",
                principalColumn: "SessionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_ChatbotRooms_ChatbotRoomSessionId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_ChatbotRoomSessionId",
                table: "Messages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChatbotRooms",
                table: "ChatbotRooms");

            migrationBuilder.DropColumn(
                name: "ChatbotRoomSessionId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "SessionId",
                table: "ChatbotRooms");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "ChatbotRooms");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "ChatbotRooms",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "ChatbotRooms",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "ChatbotRooms",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChatbotRooms",
                table: "ChatbotRooms",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ChatbotRoomId",
                table: "Messages",
                column: "ChatbotRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatbotRooms_UserId",
                table: "ChatbotRooms",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatbotRooms_Users_UserId",
                table: "ChatbotRooms",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_ChatbotRooms_ChatbotRoomId",
                table: "Messages",
                column: "ChatbotRoomId",
                principalTable: "ChatbotRooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
