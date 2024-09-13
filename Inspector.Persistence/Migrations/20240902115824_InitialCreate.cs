using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inspector.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Инвентари",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ИнвНомер = table.Column<string>(name: "Инв. Номер", type: "nvarchar(450)", nullable: false),
                    Наименование = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Науничтожение = table.Column<bool>(name: "На уничтожение", type: "bit", nullable: false),
                    Отметкаобуничтожении = table.Column<bool>(name: "Отметка об уничтожении", type: "bit", nullable: false),
                    Примечание = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Инвентари", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Наим. ТС",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Наименование = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Наим. ТС", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Тома",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ИнвентНомер = table.Column<string>(name: "Инвент. Номер", type: "nvarchar(max)", nullable: true),
                    Номердела = table.Column<string>(name: "Номер дела", type: "nvarchar(max)", nullable: false),
                    НомерТома = table.Column<int>(name: "Номер Тома", type: "int", nullable: false),
                    ГодТомаДела = table.Column<string>(name: "Год Тома/Дела", type: "nvarchar(max)", nullable: false),
                    Науничтожение = table.Column<bool>(name: "На уничтожение", type: "bit", nullable: false),
                    Отметкаобуничтожении = table.Column<bool>(name: "Отметка об уничтожении", type: "bit", nullable: false),
                    Примечание = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Тома", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Акт обслед.",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Инвномер = table.Column<string>(name: "Инв. номер", type: "nvarchar(450)", nullable: false),
                    Дата = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Название = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Idтома = table.Column<int>(name: "Id тома", type: "int", nullable: true),
                    Idинвентаря = table.Column<int>(name: "Id инвентаря", type: "int", nullable: true),
                    Страница = table.Column<int>(type: "int", nullable: true),
                    Науничтожение = table.Column<bool>(name: "На уничтожение", type: "bit", nullable: false),
                    Отметкаобуничтожении = table.Column<bool>(name: "Отметка об уничтожении", type: "bit", nullable: false),
                    Примечание = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Акт обслед.", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Акт обслед._Инвентари_Id инвентаря",
                        column: x => x.Idинвентаря,
                        principalTable: "Инвентари",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Акт обслед._Тома_Id тома",
                        column: x => x.Idтома,
                        principalTable: "Тома",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Аттестаты",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Наименование = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Номер = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ДатаВыдачи = table.Column<DateTime>(name: "Дата Выдачи", type: "datetime2", nullable: false),
                    Срокдействия = table.Column<DateTime>(name: "Срок действия", type: "datetime2", nullable: false),
                    Кемвыдан = table.Column<string>(name: "Кем выдан", type: "nvarchar(max)", nullable: false),
                    Idтома = table.Column<int>(name: "Id тома", type: "int", nullable: true),
                    Idинвентаря = table.Column<int>(name: "Id инвентаря", type: "int", nullable: true),
                    Страница = table.Column<int>(type: "int", nullable: true),
                    Науничтожение = table.Column<bool>(name: "На уничтожение", type: "bit", nullable: false),
                    Отметкаобуничтожении = table.Column<bool>(name: "Отметка об уничтожении", type: "bit", nullable: false),
                    Примечание = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Аттестаты", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Аттестаты_Инвентари_Id инвентаря",
                        column: x => x.Idинвентаря,
                        principalTable: "Инвентари",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Аттестаты_Тома_Id тома",
                        column: x => x.Idтома,
                        principalTable: "Тома",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Док. Второй",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Инвномер = table.Column<string>(name: "Инв. номер", type: "nvarchar(450)", nullable: false),
                    Дата = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Название = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Idтома = table.Column<int>(name: "Id тома", type: "int", nullable: true),
                    Idинвентаря = table.Column<int>(name: "Id инвентаря", type: "int", nullable: true),
                    Страница = table.Column<int>(type: "int", nullable: true),
                    Науничтожение = table.Column<bool>(name: "На уничтожение", type: "bit", nullable: false),
                    Отметкаобуничтожении = table.Column<bool>(name: "Отметка об уничтожении", type: "bit", nullable: false),
                    Примечание = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Док. Второй", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Док. Второй_Инвентари_Id инвентаря",
                        column: x => x.Idинвентаря,
                        principalTable: "Инвентари",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Док. Второй_Тома_Id тома",
                        column: x => x.Idтома,
                        principalTable: "Тома",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Док. Первый",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Инвномер = table.Column<string>(name: "Инв. номер", type: "nvarchar(450)", nullable: false),
                    Дата = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Название = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Idтома = table.Column<int>(name: "Id тома", type: "int", nullable: true),
                    Idинвентаря = table.Column<int>(name: "Id инвентаря", type: "int", nullable: true),
                    Страница = table.Column<int>(type: "int", nullable: true),
                    Науничтожение = table.Column<bool>(name: "На уничтожение", type: "bit", nullable: false),
                    Отметкаобуничтожении = table.Column<bool>(name: "Отметка об уничтожении", type: "bit", nullable: false),
                    Примечание = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Док. Первый", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Док. Первый_Инвентари_Id инвентаря",
                        column: x => x.Idинвентаря,
                        principalTable: "Инвентари",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Док. Первый_Тома_Id тома",
                        column: x => x.Idтома,
                        principalTable: "Тома",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Док. Прочие",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Инвномер = table.Column<string>(name: "Инв. номер", type: "nvarchar(450)", nullable: false),
                    Дата = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Название = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Idтома = table.Column<int>(name: "Id тома", type: "int", nullable: true),
                    Idинвентаря = table.Column<int>(name: "Id инвентаря", type: "int", nullable: true),
                    Страница = table.Column<int>(type: "int", nullable: true),
                    Науничтожение = table.Column<bool>(name: "На уничтожение", type: "bit", nullable: false),
                    Отметкаобуничтожении = table.Column<bool>(name: "Отметка об уничтожении", type: "bit", nullable: false),
                    Примечание = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Док. Прочие", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Док. Прочие_Инвентари_Id инвентаря",
                        column: x => x.Idинвентаря,
                        principalTable: "Инвентари",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Док. Прочие_Тома_Id тома",
                        column: x => x.Idтома,
                        principalTable: "Тома",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Док. Третий",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Инвномер = table.Column<string>(name: "Инв. номер", type: "nvarchar(450)", nullable: false),
                    Дата = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Название = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Idтома = table.Column<int>(name: "Id тома", type: "int", nullable: true),
                    Idинвентаря = table.Column<int>(name: "Id инвентаря", type: "int", nullable: true),
                    Страница = table.Column<int>(type: "int", nullable: true),
                    Науничтожение = table.Column<bool>(name: "На уничтожение", type: "bit", nullable: false),
                    Отметкаобуничтожении = table.Column<bool>(name: "Отметка об уничтожении", type: "bit", nullable: false),
                    Примечание = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Док. Третий", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Док. Третий_Инвентари_Id инвентаря",
                        column: x => x.Idинвентаря,
                        principalTable: "Инвентари",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Док. Третий_Тома_Id тома",
                        column: x => x.Idтома,
                        principalTable: "Тома",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Расп. о вводе",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Инвномер = table.Column<string>(name: "Инв. номер", type: "nvarchar(450)", nullable: false),
                    Дата = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Название = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Idтома = table.Column<int>(name: "Id тома", type: "int", nullable: true),
                    Idинвентаря = table.Column<int>(name: "Id инвентаря", type: "int", nullable: true),
                    Страница = table.Column<int>(type: "int", nullable: true),
                    Науничтожение = table.Column<bool>(name: "На уничтожение", type: "bit", nullable: false),
                    Отметкаобуничтожении = table.Column<bool>(name: "Отметка об уничтожении", type: "bit", nullable: false),
                    Примечание = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Расп. о вводе", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Расп. о вводе_Инвентари_Id инвентаря",
                        column: x => x.Idинвентаря,
                        principalTable: "Инвентари",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Расп. о вводе_Тома_Id тома",
                        column: x => x.Idтома,
                        principalTable: "Тома",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Кабинеты",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Номер = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Адресздания = table.Column<string>(name: "Адрес здания", type: "nvarchar(max)", nullable: false),
                    Типпомещения = table.Column<string>(name: "Тип помещения", type: "nvarchar(max)", nullable: false),
                    Отвзаэкспл = table.Column<string>(name: "Отв. за экспл.", type: "nvarchar(max)", nullable: true),
                    ОтвзаТЗИ = table.Column<string>(name: "Отв. за ТЗИ", type: "nvarchar(max)", nullable: true),
                    Списокдопущенныхвкабинет = table.Column<string>(name: "Список допущенных в кабинет.", type: "nvarchar(max)", nullable: true),
                    Idаттестата = table.Column<int>(name: "Id аттестата", type: "int", nullable: true),
                    IdРасповводевэкспл = table.Column<int>(name: "Id Расп. о вводе в экспл.", type: "int", nullable: true),
                    Idактаобслед = table.Column<int>(name: "Id акта обслед.", type: "int", nullable: true),
                    Примечание = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Кабинеты", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Кабинеты_Акт обслед._Id акта обслед.",
                        column: x => x.Idактаобслед,
                        principalTable: "Акт обслед.",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Кабинеты_Аттестаты_Id аттестата",
                        column: x => x.Idаттестата,
                        principalTable: "Аттестаты",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Кабинеты_Расп. о вводе_Id Расп. о вводе в экспл.",
                        column: x => x.IdРасповводевэкспл,
                        principalTable: "Расп. о вводе",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ОВТ",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Наименование = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Idаттестата = table.Column<int>(name: "Id аттестата", type: "int", nullable: true),
                    Отвзаэксплуатацию = table.Column<string>(name: "Отв. за эксплуатацию", type: "nvarchar(max)", nullable: true),
                    ОтвзаТЗИ = table.Column<string>(name: "Отв. за ТЗИ", type: "nvarchar(max)", nullable: true),
                    АдминБезопИнф = table.Column<string>(name: "Админ.Безоп.Инф.", type: "nvarchar(max)", nullable: true),
                    СисАдмин = table.Column<string>(name: "Сис.Админ.", type: "nvarchar(max)", nullable: true),
                    IdРасповводевэкспл = table.Column<int>(name: "Id Расп. о вводе в экспл.", type: "int", nullable: true),
                    Примечание = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ОВТ", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ОВТ_Аттестаты_Id аттестата",
                        column: x => x.Idаттестата,
                        principalTable: "Аттестаты",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ОВТ_Расп. о вводе_Id Расп. о вводе в экспл.",
                        column: x => x.IdРасповводевэкспл,
                        principalTable: "Расп. о вводе",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Тех.средства",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Серийныйномер = table.Column<string>(name: "Серийный номер", type: "nvarchar(450)", nullable: false),
                    IdНаимТС = table.Column<int>(name: "Id Наим. ТС", type: "int", nullable: false),
                    Модель = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdКабинета = table.Column<int>(name: "Id Кабинета", type: "int", nullable: true),
                    IdОВТ = table.Column<int>(name: "Id ОВТ", type: "int", nullable: true),
                    Актуальность = table.Column<bool>(type: "bit", nullable: false),
                    IdДокументаПервый = table.Column<int>(name: "Id Документа Первый", type: "int", nullable: true),
                    IdДокументаВторой = table.Column<int>(name: "Id Документа Второй", type: "int", nullable: true),
                    IdДокументаТретий = table.Column<int>(name: "Id Документа Третий.", type: "int", nullable: true),
                    Назначение = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Примечание = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Тех.средства", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Тех.средства_Док. Второй_Id Документа Второй",
                        column: x => x.IdДокументаВторой,
                        principalTable: "Док. Второй",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Тех.средства_Док. Первый_Id Документа Первый",
                        column: x => x.IdДокументаПервый,
                        principalTable: "Док. Первый",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Тех.средства_Док. Третий_Id Документа Третий.",
                        column: x => x.IdДокументаТретий,
                        principalTable: "Док. Третий",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Тех.средства_Кабинеты_Id Кабинета",
                        column: x => x.IdКабинета,
                        principalTable: "Кабинеты",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Тех.средства_Наим. ТС_Id Наим. ТС",
                        column: x => x.IdНаимТС,
                        principalTable: "Наим. ТС",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Тех.средства_ОВТ_Id ОВТ",
                        column: x => x.IdОВТ,
                        principalTable: "ОВТ",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Акт обслед._Инв. номер",
                table: "Акт обслед.",
                column: "Инв. номер",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Акт обслед._Id инвентаря",
                table: "Акт обслед.",
                column: "Id инвентаря");

            migrationBuilder.CreateIndex(
                name: "IX_Акт обслед._Id тома",
                table: "Акт обслед.",
                column: "Id тома");

            migrationBuilder.CreateIndex(
                name: "IX_Аттестаты_Номер",
                table: "Аттестаты",
                column: "Номер",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Аттестаты_Id инвентаря",
                table: "Аттестаты",
                column: "Id инвентаря");

            migrationBuilder.CreateIndex(
                name: "IX_Аттестаты_Id тома",
                table: "Аттестаты",
                column: "Id тома");

            migrationBuilder.CreateIndex(
                name: "IX_Док. Второй_Инв. номер",
                table: "Док. Второй",
                column: "Инв. номер",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Док. Второй_Id инвентаря",
                table: "Док. Второй",
                column: "Id инвентаря");

            migrationBuilder.CreateIndex(
                name: "IX_Док. Второй_Id тома",
                table: "Док. Второй",
                column: "Id тома");

            migrationBuilder.CreateIndex(
                name: "IX_Док. Первый_Инв. номер",
                table: "Док. Первый",
                column: "Инв. номер",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Док. Первый_Id инвентаря",
                table: "Док. Первый",
                column: "Id инвентаря");

            migrationBuilder.CreateIndex(
                name: "IX_Док. Первый_Id тома",
                table: "Док. Первый",
                column: "Id тома");

            migrationBuilder.CreateIndex(
                name: "IX_Док. Прочие_Инв. номер",
                table: "Док. Прочие",
                column: "Инв. номер",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Док. Прочие_Id инвентаря",
                table: "Док. Прочие",
                column: "Id инвентаря");

            migrationBuilder.CreateIndex(
                name: "IX_Док. Прочие_Id тома",
                table: "Док. Прочие",
                column: "Id тома");

            migrationBuilder.CreateIndex(
                name: "IX_Док. Третий_Инв. номер",
                table: "Док. Третий",
                column: "Инв. номер",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Док. Третий_Id инвентаря",
                table: "Док. Третий",
                column: "Id инвентаря");

            migrationBuilder.CreateIndex(
                name: "IX_Док. Третий_Id тома",
                table: "Док. Третий",
                column: "Id тома");

            migrationBuilder.CreateIndex(
                name: "IX_Инвентари_Инв. Номер",
                table: "Инвентари",
                column: "Инв. Номер",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Кабинеты_Id акта обслед.",
                table: "Кабинеты",
                column: "Id акта обслед.");

            migrationBuilder.CreateIndex(
                name: "IX_Кабинеты_Id аттестата",
                table: "Кабинеты",
                column: "Id аттестата");

            migrationBuilder.CreateIndex(
                name: "IX_Кабинеты_Id Расп. о вводе в экспл.",
                table: "Кабинеты",
                column: "Id Расп. о вводе в экспл.");

            migrationBuilder.CreateIndex(
                name: "IX_Наим. ТС_Наименование",
                table: "Наим. ТС",
                column: "Наименование",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ОВТ_Id аттестата",
                table: "ОВТ",
                column: "Id аттестата",
                unique: true,
                filter: "[Id аттестата] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ОВТ_Id Расп. о вводе в экспл.",
                table: "ОВТ",
                column: "Id Расп. о вводе в экспл.");

            migrationBuilder.CreateIndex(
                name: "IX_Расп. о вводе_Инв. номер",
                table: "Расп. о вводе",
                column: "Инв. номер",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Расп. о вводе_Id инвентаря",
                table: "Расп. о вводе",
                column: "Id инвентаря");

            migrationBuilder.CreateIndex(
                name: "IX_Расп. о вводе_Id тома",
                table: "Расп. о вводе",
                column: "Id тома");

            migrationBuilder.CreateIndex(
                name: "IX_Тех.средства_Серийный номер",
                table: "Тех.средства",
                column: "Серийный номер",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Тех.средства_Id Документа Второй",
                table: "Тех.средства",
                column: "Id Документа Второй");

            migrationBuilder.CreateIndex(
                name: "IX_Тех.средства_Id Документа Первый",
                table: "Тех.средства",
                column: "Id Документа Первый");

            migrationBuilder.CreateIndex(
                name: "IX_Тех.средства_Id Документа Третий.",
                table: "Тех.средства",
                column: "Id Документа Третий.");

            migrationBuilder.CreateIndex(
                name: "IX_Тех.средства_Id Кабинета",
                table: "Тех.средства",
                column: "Id Кабинета");

            migrationBuilder.CreateIndex(
                name: "IX_Тех.средства_Id Наим. ТС",
                table: "Тех.средства",
                column: "Id Наим. ТС");

            migrationBuilder.CreateIndex(
                name: "IX_Тех.средства_Id ОВТ",
                table: "Тех.средства",
                column: "Id ОВТ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Док. Прочие");

            migrationBuilder.DropTable(
                name: "Тех.средства");

            migrationBuilder.DropTable(
                name: "Док. Второй");

            migrationBuilder.DropTable(
                name: "Док. Первый");

            migrationBuilder.DropTable(
                name: "Док. Третий");

            migrationBuilder.DropTable(
                name: "Кабинеты");

            migrationBuilder.DropTable(
                name: "Наим. ТС");

            migrationBuilder.DropTable(
                name: "ОВТ");

            migrationBuilder.DropTable(
                name: "Акт обслед.");

            migrationBuilder.DropTable(
                name: "Аттестаты");

            migrationBuilder.DropTable(
                name: "Расп. о вводе");

            migrationBuilder.DropTable(
                name: "Инвентари");

            migrationBuilder.DropTable(
                name: "Тома");
        }
    }
}
