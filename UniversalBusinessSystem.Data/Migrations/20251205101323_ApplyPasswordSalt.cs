using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversalBusinessSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class ApplyPasswordSalt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 10, 13, 22, 408, DateTimeKind.Utc).AddTicks(1212));

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 10, 13, 22, 408, DateTimeKind.Utc).AddTicks(1239));

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 10, 13, 22, 408, DateTimeKind.Utc).AddTicks(1249));

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 10, 13, 22, 408, DateTimeKind.Utc).AddTicks(1257));

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 10, 13, 22, 408, DateTimeKind.Utc).AddTicks(1264));

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 10, 13, 22, 408, DateTimeKind.Utc).AddTicks(1270));

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777777"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 10, 13, 22, 408, DateTimeKind.Utc).AddTicks(1276));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 10, 13, 22, 408, DateTimeKind.Utc).AddTicks(1348));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaab"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 10, 13, 22, 408, DateTimeKind.Utc).AddTicks(1363));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 10, 13, 22, 408, DateTimeKind.Utc).AddTicks(1376));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbc"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 10, 13, 22, 408, DateTimeKind.Utc).AddTicks(1382));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbd"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 10, 13, 22, 408, DateTimeKind.Utc).AddTicks(1388));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbe"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 10, 13, 22, 408, DateTimeKind.Utc).AddTicks(1394));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbf"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 10, 13, 22, 408, DateTimeKind.Utc).AddTicks(1401));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 10, 13, 22, 408, DateTimeKind.Utc).AddTicks(1406));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("cccccccc-cccc-cccc-cccc-cccccccccccd"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 10, 13, 22, 408, DateTimeKind.Utc).AddTicks(1415));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("cccccccc-cccc-cccc-cccc-ccccccccccce"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 10, 13, 22, 408, DateTimeKind.Utc).AddTicks(1420));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("cccccccc-cccc-cccc-cccc-cccccccccccf"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 10, 13, 22, 408, DateTimeKind.Utc).AddTicks(1429));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-ddddddddddd0"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 10, 13, 22, 408, DateTimeKind.Utc).AddTicks(1435));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-ddddddddddd1"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 10, 13, 22, 408, DateTimeKind.Utc).AddTicks(1440));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-ddddddddddd2"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 10, 13, 22, 408, DateTimeKind.Utc).AddTicks(1445));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-ddddddddddd3"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 10, 13, 22, 408, DateTimeKind.Utc).AddTicks(1450));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 10, 13, 22, 408, DateTimeKind.Utc).AddTicks(1454));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeef"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 10, 13, 22, 408, DateTimeKind.Utc).AddTicks(1459));

            migrationBuilder.UpdateData(
                table: "ShopTypes",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 10, 13, 22, 408, DateTimeKind.Utc).AddTicks(591));

            migrationBuilder.UpdateData(
                table: "ShopTypes",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111112"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 10, 13, 22, 408, DateTimeKind.Utc).AddTicks(626));

            migrationBuilder.UpdateData(
                table: "ShopTypes",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111113"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 10, 13, 22, 408, DateTimeKind.Utc).AddTicks(634));

            migrationBuilder.UpdateData(
                table: "ShopTypes",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111114"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 10, 13, 22, 408, DateTimeKind.Utc).AddTicks(641));

            migrationBuilder.UpdateData(
                table: "ShopTypes",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111115"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 10, 13, 22, 408, DateTimeKind.Utc).AddTicks(647));

            migrationBuilder.UpdateData(
                table: "ShopTypes",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111116"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 10, 13, 22, 408, DateTimeKind.Utc).AddTicks(653));

            migrationBuilder.UpdateData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 10, 13, 22, 408, DateTimeKind.Utc).AddTicks(1049));

            migrationBuilder.UpdateData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 10, 13, 22, 408, DateTimeKind.Utc).AddTicks(1080));

            migrationBuilder.UpdateData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 10, 13, 22, 408, DateTimeKind.Utc).AddTicks(1089));

            migrationBuilder.UpdateData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 10, 13, 22, 408, DateTimeKind.Utc).AddTicks(1095));

            migrationBuilder.UpdateData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 10, 13, 22, 408, DateTimeKind.Utc).AddTicks(1102));

            migrationBuilder.UpdateData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 10, 13, 22, 408, DateTimeKind.Utc).AddTicks(1112));

            migrationBuilder.UpdateData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777777"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 10, 13, 22, 408, DateTimeKind.Utc).AddTicks(1120));

            migrationBuilder.UpdateData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 10, 13, 22, 408, DateTimeKind.Utc).AddTicks(1129));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 10, 8, 49, 470, DateTimeKind.Utc).AddTicks(9712));

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 10, 8, 49, 470, DateTimeKind.Utc).AddTicks(9725));

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 10, 8, 49, 470, DateTimeKind.Utc).AddTicks(9731));

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 10, 8, 49, 470, DateTimeKind.Utc).AddTicks(9735));

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 10, 8, 49, 470, DateTimeKind.Utc).AddTicks(9740));

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 10, 8, 49, 470, DateTimeKind.Utc).AddTicks(9745));

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777777"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 10, 8, 49, 470, DateTimeKind.Utc).AddTicks(9750));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 10, 8, 49, 470, DateTimeKind.Utc).AddTicks(9804));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaab"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 10, 8, 49, 470, DateTimeKind.Utc).AddTicks(9813));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 10, 8, 49, 470, DateTimeKind.Utc).AddTicks(9821));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbc"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 10, 8, 49, 470, DateTimeKind.Utc).AddTicks(9825));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbd"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 10, 8, 49, 470, DateTimeKind.Utc).AddTicks(9828));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbe"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 10, 8, 49, 470, DateTimeKind.Utc).AddTicks(9832));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbf"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 10, 8, 49, 470, DateTimeKind.Utc).AddTicks(9836));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 10, 8, 49, 470, DateTimeKind.Utc).AddTicks(9839));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("cccccccc-cccc-cccc-cccc-cccccccccccd"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 10, 8, 49, 470, DateTimeKind.Utc).AddTicks(9843));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("cccccccc-cccc-cccc-cccc-ccccccccccce"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 10, 8, 49, 470, DateTimeKind.Utc).AddTicks(9846));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("cccccccc-cccc-cccc-cccc-cccccccccccf"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 10, 8, 49, 470, DateTimeKind.Utc).AddTicks(9852));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-ddddddddddd0"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 10, 8, 49, 470, DateTimeKind.Utc).AddTicks(9856));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-ddddddddddd1"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 10, 8, 49, 470, DateTimeKind.Utc).AddTicks(9860));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-ddddddddddd2"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 10, 8, 49, 470, DateTimeKind.Utc).AddTicks(9864));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-ddddddddddd3"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 10, 8, 49, 470, DateTimeKind.Utc).AddTicks(9867));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 10, 8, 49, 470, DateTimeKind.Utc).AddTicks(9873));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeef"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 10, 8, 49, 470, DateTimeKind.Utc).AddTicks(9876));

            migrationBuilder.UpdateData(
                table: "ShopTypes",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 10, 8, 49, 470, DateTimeKind.Utc).AddTicks(9282));

            migrationBuilder.UpdateData(
                table: "ShopTypes",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111112"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 10, 8, 49, 470, DateTimeKind.Utc).AddTicks(9303));

            migrationBuilder.UpdateData(
                table: "ShopTypes",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111113"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 10, 8, 49, 470, DateTimeKind.Utc).AddTicks(9309));

            migrationBuilder.UpdateData(
                table: "ShopTypes",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111114"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 10, 8, 49, 470, DateTimeKind.Utc).AddTicks(9314));

            migrationBuilder.UpdateData(
                table: "ShopTypes",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111115"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 10, 8, 49, 470, DateTimeKind.Utc).AddTicks(9321));

            migrationBuilder.UpdateData(
                table: "ShopTypes",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111116"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 10, 8, 49, 470, DateTimeKind.Utc).AddTicks(9329));

            migrationBuilder.UpdateData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 10, 8, 49, 470, DateTimeKind.Utc).AddTicks(9599));

            migrationBuilder.UpdateData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 10, 8, 49, 470, DateTimeKind.Utc).AddTicks(9623));

            migrationBuilder.UpdateData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 10, 8, 49, 470, DateTimeKind.Utc).AddTicks(9629));

            migrationBuilder.UpdateData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 10, 8, 49, 470, DateTimeKind.Utc).AddTicks(9633));

            migrationBuilder.UpdateData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 10, 8, 49, 470, DateTimeKind.Utc).AddTicks(9637));

            migrationBuilder.UpdateData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 10, 8, 49, 470, DateTimeKind.Utc).AddTicks(9644));

            migrationBuilder.UpdateData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777777"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 10, 8, 49, 470, DateTimeKind.Utc).AddTicks(9651));

            migrationBuilder.UpdateData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 10, 8, 49, 470, DateTimeKind.Utc).AddTicks(9655));
        }
    }
}
