The EF Core tools version '3.0.3' is older than that of the runtime '3.1.5'. Update the tools for the latest features and bug fixes.
info: Microsoft.EntityFrameworkCore.Infrastructure[10403]
      Entity Framework Core 3.1.5 initialized 'MyCourseDbContext' using provider 'Microsoft.EntityFrameworkCore.Sqlite' with options: MaxPoolSize=128 
System.NotSupportedException: SQLite does not support this migration operation ('AlterColumnOperation'). For more information, see http://go.microsoft.com/fwlink/?LinkId=723262.
   at Microsoft.EntityFrameworkCore.Migrations.SqliteMigrationsSqlGenerator.Generate(AlterColumnOperation operation, IModel model, MigrationCommandListBuilder builder)
   at Microsoft.EntityFrameworkCore.Migrations.MigrationsSqlGenerator.<>c.<.cctor>b__71_4(MigrationsSqlGenerator g, MigrationOperation o, IModel m, MigrationCommandListBuilder b)
   at Microsoft.EntityFrameworkCore.Migrations.MigrationsSqlGenerator.Generate(MigrationOperation operation, IModel model, MigrationCommandListBuilder builder)
   at Microsoft.EntityFrameworkCore.Migrations.MigrationsSqlGenerator.Generate(IReadOnlyList`1 operations, IModel model)
   at Microsoft.EntityFrameworkCore.Migrations.SqliteMigrationsSqlGenerator.Generate(IReadOnlyList`1 operations, IModel model)
   at Microsoft.EntityFrameworkCore.Migrations.Internal.Migrator.GenerateUpSql(Migration migration)
   at Microsoft.EntityFrameworkCore.Migrations.Internal.Migrator.GenerateScript(String fromMigration, String toMigration, Boolean idempotent)
   at Microsoft.EntityFrameworkCore.Design.Internal.MigrationsOperations.ScriptMigration(String fromMigration, String toMigration, Boolean idempotent, String contextType)
   at Microsoft.EntityFrameworkCore.Design.OperationExecutor.ScriptMigrationImpl(String fromMigration, String toMigration, Boolean idempotent, String contextType)
   at Microsoft.EntityFrameworkCore.Design.OperationExecutor.ScriptMigration.<>c__DisplayClass0_0.<.ctor>b__0()
   at Microsoft.EntityFrameworkCore.Design.OperationExecutor.OperationBase.<>c__DisplayClass3_0`1.<Execute>b__0()
   at Microsoft.EntityFrameworkCore.Design.OperationExecutor.OperationBase.Execute(Action action)
SQLite does not support this migration operation ('AlterColumnOperation'). For more information, see http://go.microsoft.com/fwlink/?LinkId=723262.
